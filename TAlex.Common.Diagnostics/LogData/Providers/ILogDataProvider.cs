using System;
using System.Collections.Generic;


namespace TAlex.Common.Diagnostics.LogData.Providers
{
    public interface ILogDataProvider
    {
        DateTimeRange GetDateRange();

        IEnumerable<LogItem> GetRecords(DateTime date);

        IEnumerable<LogItem> GetRecords(DateTime startDate, DateTime endDate);
    }
}
