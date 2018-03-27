using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TAlex.Common.Diagnostics.Models;


namespace TAlex.Common.Diagnostics.Logging.Listeners
{
    public class AzureTableTraceListener : TraceListener
    {
        #region Fields

        public static readonly string ConnectionStringKeyAttrName = "connectionStringKey";
        public static readonly string TableNameAttrName = "tableName";

        protected readonly Lazy<CloudStorageAccount> StorageAccount;

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings[Attributes[ConnectionStringKeyAttrName]];
            }
        }

        public string TableName
        {
            get
            {
                return Attributes[TableNameAttrName];
            }
        }

        #endregion

        #region Constructors

        public AzureTableTraceListener()
        {
            StorageAccount = new Lazy<CloudStorageAccount>(() => CloudStorageAccount.Parse(ConnectionString));
        }

        #endregion

        #region Methods

        protected override string[] GetSupportedAttributes()
        {
            return new string[]
            {
                ConnectionStringKeyAttrName,
                TableNameAttrName
            };
        }


        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            TraceData(data, source, eventType);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            TraceData(String.Join(",", data), source, eventType);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            TraceData(eventCache, source, eventType, id);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            TraceData(eventCache, source, eventType, id, message);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            TraceData(eventCache, source, eventType, id, String.Format(format, args));
        }


        private void TraceData(object data, string source, TraceEventType eventType)
        {
            var record = new TraceRecordTableEntity
            {
                PartitionKey = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                RowKey = Guid.NewGuid().ToString()
            };

            if (HttpContext.Current != null)
            {
                FillRecord(record, HttpContext.Current);
            }

            if (data is Exception)
            {
                record.Exception = new ExceptionInfo((Exception)data);
                record.Description = record.Exception.Message;
            }
            else
            {
                record.Description = data + String.Empty;
            }
            if (String.IsNullOrWhiteSpace(record.Description))
            {
                record.Description = source;
            }
            record.EventType = eventType.ToString();

            // Trace
            var tableClient = StorageAccount.Value.CreateCloudTableClient();
            var table = tableClient.GetTableReference(TableName);
            table.CreateIfNotExists();

            table.Execute(TableOperation.Insert(record));
        }


        private void FillRecord(TraceRecordTableEntity record, HttpContext context)
        {
            try
            {
                var request = context.Request;

                record.RequestUrl = request.Url + String.Empty;
                record.UserAgent = request.UserAgent;
                record.UrlReferrer = request.UrlReferrer + String.Empty;
                record.HttpMethod = request.HttpMethod;
                record.PostData = CreateQueryString(request.Form);
                record.Status = GetStatus(context);
                record.Handler = context.Handler + String.Empty;
                record.UserName = context.User != null ? context.User.Identity.Name : String.Empty;
                record.UserHostAddress = request.UserHostAddress;
            }
            catch (HttpException)
            {
            }
        }

        private string CreateQueryString(NameValueCollection vals)
        {
            return String.Join("&", vals.Keys.Cast<string>().Select(x => String.Format("{0}={1}", x, vals[x])));
        }

        private string GetStatus(HttpContext context)
        {
            HttpException exc = context.Error as HttpException;

            if (exc != null)
                return exc.GetHttpCode().ToString();
            else if (context.Error != null)
                return "500 Server Error";

            return context.Response.Status;
        }

        #endregion

        public override void Write(string message)
        {
            TraceData(message, String.Empty, TraceEventType.Information);
        }

        public override void WriteLine(string message)
        {
            TraceData(message, String.Empty, TraceEventType.Information);
        }
    }
}
