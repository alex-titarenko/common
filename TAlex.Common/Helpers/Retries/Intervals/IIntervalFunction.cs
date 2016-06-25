using System;


namespace TAlex.Common.Helpers.Retries.Intervals
{
    public interface IIntervalFunction
    {
        TimeSpan GetNewInterval(int currentAttempt, TimeSpan currentInterval, RetryPolicy policy);
    }
}
