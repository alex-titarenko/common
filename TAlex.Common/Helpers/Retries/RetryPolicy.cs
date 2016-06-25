using System;
using TAlex.Common.Helpers.Retries.Intervals;

namespace TAlex.Common.Helpers.Retries
{
    public delegate void RetryHandler(Exception exception, int attempt, TimeSpan runTime, RetryPolicy policy);

    public delegate void RetriesExceededHandler(Exception exception, TimeSpan runTime, RetryPolicy policy);


    public class RetryPolicy
    {
        public int RetriesCount { get; set; }

        public TimeSpan InitialRetryInterval { get; set; }

        public TimeSpan MaxRetryInterval { get; set; }

        public RetryHandler RetryHandler { get; set; }

        public RetriesExceededHandler RetriesExceededHandler { get; set; }

        public IIntervalFunction IntervalFunction { get; set; }


        public RetryPolicy()
        {
            RetriesCount = 10;
            InitialRetryInterval = TimeSpan.FromSeconds(30);
            MaxRetryInterval = TimeSpan.FromMinutes(20);
            IntervalFunction = new ExponentialBackoffIntervalFunction();
            
        }


        public RetryPolicy Clone()
        {
            return new RetryPolicy
            {
                RetriesCount = RetriesCount,
                InitialRetryInterval = InitialRetryInterval,
                MaxRetryInterval = MaxRetryInterval,
                RetryHandler = RetryHandler,
                RetriesExceededHandler = RetriesExceededHandler,
                IntervalFunction = IntervalFunction
            };
        }
    }
}
