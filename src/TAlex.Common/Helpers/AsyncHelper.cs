using System;
using System.Threading.Tasks;


namespace TAlex.Common.Helpers
{
    public static class AsyncHelper
    {
        public static T GetSyncResult<T>(this Func<Task<T>> func)
        {
            var syncTask = Task.Run(async () => { return await func(); });
            syncTask.Wait();
            return syncTask.Result;
        }
    }
}
