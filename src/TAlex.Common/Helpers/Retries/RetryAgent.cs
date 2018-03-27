using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;


namespace TAlex.Common.Helpers.Retries
{
    public static class RetryAgent
    {
        public static RetryPolicy DefaultPolicy { get; private set; }


        static RetryAgent()
        {
            DefaultPolicy = new RetryPolicy();
        }


        public static void SetDefaultPolicy(RetryPolicy defaultPolicy)
        {
            DefaultPolicy = defaultPolicy;
        }


        public static void Retry(Action code, RetryPolicy policy = null)
        {
            Retry(code, new List<Type> { typeof(Exception) }, policy);
        }

        public static void Retry(Action code, IEnumerable<Type> retryableExceptions, RetryPolicy policy = null)
        {
            Argument.RequiresNotNull(code, nameof(code));
            Argument.RequiresNotNull(retryableExceptions, nameof(retryableExceptions));

            var retryPolicy = (policy ?? DefaultPolicy).Clone();
            var retryInterval = retryPolicy.InitialRetryInterval;
            Exception lastException = null;
            var runTime = Stopwatch.StartNew();

            for (var attempt = 1; attempt <= retryPolicy.RetriesCount; attempt++)
            {
                try
                {
                    code();
                    return;
                }
                catch (Exception exc)
                {
                    var isHandledException = false;

                    foreach (var retryableException in retryableExceptions)
                    {
                        if (exc.GetType() == retryableException ||
                            exc.GetType().GetTypeInfo().IsSubclassOf(retryableException))
                        {
                            isHandledException = true;
                            lastException = exc;
                            retryPolicy.RetryHandler?.Invoke(exc, attempt, runTime.Elapsed, retryPolicy);
                            Task.Delay(retryInterval).Wait();
                            retryInterval = retryPolicy.IntervalFunction.GetNewInterval(attempt, retryInterval, retryPolicy);

                            break;
                        }
                    }

                    if (!isHandledException)
                    {
                        throw;
                    }
                }
            }

            retryPolicy.RetriesExceededHandler?.Invoke(lastException, runTime.Elapsed, retryPolicy);
            var errorMessage = $"Operation continued to fail after {retryPolicy.RetriesCount} retries and {runTime.Elapsed.TotalMinutes} minutes. {lastException.Message}";
            throw new RetriesExceededException(errorMessage, retryPolicy, lastException);
        }
    }
}
