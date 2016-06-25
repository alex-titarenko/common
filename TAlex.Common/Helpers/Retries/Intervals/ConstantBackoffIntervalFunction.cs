using System;


namespace TAlex.Common.Helpers.Retries.Intervals
{
    public class ConstantBackoffIntervalFunction : IIntervalFunction
    {
        public TimeSpan GetNewInterval(int currentAttempt, TimeSpan currentInterval, RetryPolicy policy)
        {
            return currentInterval;
        }
    }
}
