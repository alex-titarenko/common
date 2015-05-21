using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TAlex.Common.Extensions;


namespace TAlex.Common.Diagnostics.Models
{
    public class TraceRecordTableEntity : TraceRecord, ITableEntity
    {
        #region ITableEntity Members

        public string ETag { get; set; }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset Timestamp { get; set; }


        public string EventType { get; set; }


        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            EventType = ReadProperty(x => x.EventType, properties);
            Description = ReadProperty(x => x.Description, properties);
            Exception = DeserializeException(ReadProperty(x => x.Exception, properties));
            Handler = ReadProperty(x => x.Handler, properties);
            HttpMethod = ReadProperty(x => x.HttpMethod, properties);
            PostData = ReadProperty(x => x.PostData, properties);
            RequestUrl = ReadProperty(x => x.RequestUrl, properties);
            Status = ReadProperty(x => x.Status, properties);
            TraceIdentifier = ReadProperty(x => x.TraceIdentifier, properties);
            UrlReferrer = ReadProperty(x => x.UrlReferrer, properties);
            UserAgent = ReadProperty(x => x.UserAgent, properties);
            UserHostAddress = ReadProperty(x => x.UserHostAddress, properties);
            UserName = ReadProperty(x => x.UserName, properties);
        }

        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            return new Dictionary<string, EntityProperty>
            {
                {PropertyName.Get<TraceRecordTableEntity>(x => x.EventType), new EntityProperty(EventType)},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.Description), new EntityProperty(Description)},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.Exception), new EntityProperty(SerializeException(Exception))},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.Handler), new EntityProperty(Handler)},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.HttpMethod), new EntityProperty(HttpMethod)},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.PostData), new EntityProperty(PostData)},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.RequestUrl), new EntityProperty(RequestUrl)},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.Status), new EntityProperty(Status)},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.TraceIdentifier), new EntityProperty(TraceIdentifier)},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.UrlReferrer), new EntityProperty(UrlReferrer)},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.UserAgent), new EntityProperty(UserAgent)},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.UserHostAddress), new EntityProperty(UserHostAddress)},
                {PropertyName.Get<TraceRecordTableEntity>(x => x.UserName), new EntityProperty(UserName)}
            };
        }

        #endregion

        protected string ReadProperty(Expression<Func<TraceRecordTableEntity, object>> propSelector, IDictionary<string, EntityProperty> properties)
        {
            EntityProperty entityProperty = null;
            if (properties.TryGetValue(PropertyName.Get<TraceRecordTableEntity>(propSelector), out entityProperty))
            {
                return entityProperty.StringValue;
            }
            return null;
        }

        protected string SerializeException(ExceptionInfo exception)
        {
            if (exception == null) return null;

            XmlSerializer ser = new XmlSerializer(typeof(ExceptionInfo));

            using (var writer = new StringWriter())
            {
                ser.Serialize(writer, exception);
                return writer.GetStringBuilder().ToString();
            }
        }

        protected ExceptionInfo DeserializeException(string raw)
        {
            if (String.IsNullOrEmpty(raw)) return null;

            XmlSerializer ser = new XmlSerializer(typeof(ExceptionInfo));

            using (var reader = new StringReader(raw))
            {
                return (ExceptionInfo)ser.Deserialize(reader);
            }
        }
    }
}
