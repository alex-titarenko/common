using System;


namespace TAlex.Common.Helpers.Retries
{
    public class RetriesExceededException : Exception
    {
        public RetryPolicy Policy { get; private set; }


        public RetriesExceededException(string message, RetryPolicy policy, Exception innerException)
            : base(message, innerException)
        {
            Policy = policy;
        }
    }
}
