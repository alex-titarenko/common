using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TAlex.Common.Diagnostics.Models;


namespace TAlex.Common.Diagnostics.Logging.Data.Providers
{
    public class AzureTableLogDataProvider : ILogDataProvider
    {
        #region Fields

        protected readonly CloudStorageAccount StorageAccount;

        #endregion

        #region Properties

        public string TableName { get; set; }

        #endregion

        #region Constructors

        public AzureTableLogDataProvider(string connectionString, string tableName = "logs")
        {
            TableName = tableName;
            StorageAccount = CloudStorageAccount.Parse(connectionString);
        }

        #endregion

        #region ILogDataProvider Members

        public DateTimeRange GetDateRange()
        {
            return new DateTimeRange { StartDate = DateTime.Today, EndDate = DateTime.Today };
        }

        public IEnumerable<LogItem> GetRecords(DateTime startDate, DateTime endDate)
        {
            return ReadEntities(startDate, endDate);
        }

        public IEnumerable<LogItem> GetRecords(DateTime date)
        {
            return ReadEntities(date, date);
        }

        #endregion

        public IEnumerable<LogItem> ReadEntities(DateTime startDate, DateTime endDate)
        {
            var tableClient = StorageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(TableName);

            var query = new TableQuery<TraceRecordTableEntity>().Where(TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.GreaterThanOrEqual, GetDateString(startDate)),
                Microsoft.WindowsAzure.Storage.Table.TableOperators.And,
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.LessThanOrEqual, GetDateString(endDate))));

            return table.ExecuteQuery(query).Select(x => new LogItem
            {
                Type = ConvertEventType(x.EventType),
                TimeCreated = x.Timestamp.DateTime,
                Description = x.Description,
                Exception = x.Exception,

                BasicInformation = new List<NameValuePair>
                {
                    new NameValuePair("Time", x.Timestamp.DateTime.ToString()),
                    new NameValuePair("Level", x.EventType),
                    new NameValuePair("Trace Identifier/Code", x.TraceIdentifier)
                }.Where(i => !String.IsNullOrEmpty(i.Value)).ToList(),
                GeneralProperties = new List<NameValuePair>
                {
                    new NameValuePair("Description", x.Description),
                    new NameValuePair("RequestUrl", x.RequestUrl),
                    new NameValuePair("UserAgent", x.UserAgent),
                    new NameValuePair("Handler", x.Handler),
                    new NameValuePair("PostData", x.PostData),
                    new NameValuePair("HttpMethod", x.HttpMethod),
                    new NameValuePair("Status", x.Status),
                    new NameValuePair("UserHostAddress", x.UserHostAddress)
                }.Where(i => !String.IsNullOrEmpty(i.Value)).ToList()
            });
        }

        private string GetDateString(DateTime date)
        {
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        private LogItemType ConvertEventType(string s)
        {
            switch (s)
            {
                case "Critical":
                case "Error":
                    return LogItemType.Error;

                case "Warning":
                    return LogItemType.Warning;

                case "Information":
                case "Verbose":
                case "Start":
                case "Stop":
                case "Suspend":
                case "Resume":
                case "Transfer":
                    return LogItemType.Info;

                default:
                    throw new ArgumentException();
            }
        }
    }
}
