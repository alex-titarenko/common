using System;


namespace TAlex.Common.Helpers.Retries.Intervals
{
    public class ExponentialBackoffIntervalFunction : IIntervalFunction
    {
        public TimeSpan GetNewInterval(int currentAttempt, TimeSpan currentInterval, RetryPolicy policy)
        {
            var newInterval = new TimeSpan(currentInterval.Ticks * 2);
            return newInterval > policy.MaxRetryInterval ? policy.MaxRetryInterval : newInterval;
        }
    }
}
