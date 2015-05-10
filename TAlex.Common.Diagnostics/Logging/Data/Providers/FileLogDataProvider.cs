using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace TAlex.Common.Diagnostics.Logging.Data.Providers
{
    public abstract class FileLogDataProvider : ILogDataProvider
    {
        #region Properties

        public string LogsDirectory { get; set; }

        public string LogFileNamePattern { get; set; }

        #endregion

        #region Constructors

        public FileLogDataProvider(string logsDirectory, string logFileNamePattern)
        {
            LogsDirectory = logsDirectory;
            LogFileNamePattern = logFileNamePattern;
        }

        #endregion

        #region ILogDataProvider Members

        public DateTimeRange GetDateRange()
        {
            string[] files = Directory.GetFiles(LogsDirectory);
            return new DateTimeRange { StartDate = DateTime.Today, EndDate = DateTime.Today };
        }

        public IEnumerable<LogItem> GetRecords(DateTime date)
        {
            string fileName = String.Format("Trace_{0}-{1:D2}-{2:D2}.svclog", date.Year, date.Month, date.Day);
            string filePath = Path.Combine(LogsDirectory, fileName);

            if (!File.Exists(filePath)) yield break;
            using (Stream stream = new FileStream(Path.Combine(LogsDirectory, fileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var records = GetRecords(stream);
                foreach (var record in records)
                {
                    yield return record;
                }
            }
        }

        public IEnumerable<LogItem> GetRecords(DateTime startDate, DateTime endDate)
        {
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                IEnumerable<LogItem> records = GetRecords(date);
                foreach (LogItem record in records)
                {
                    yield return record;
                }
            }
        }

        #endregion

        protected abstract IEnumerable<LogItem> GetRecords(Stream stream);
    }
}
