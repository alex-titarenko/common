using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TAlex.Common.Diagnostics;


namespace TAlex.Common.Diagnostics.Logging.Data.Providers
{
    public class SvcLogDataProvider : FileLogDataProvider
    {
        #region Fields

        private static readonly List<string> _excludedGeneralProperties = new List<string>
        {
            "Exception",
            "TraceIdentifier"
        };

        #endregion

        #region Constructors

        public SvcLogDataProvider(string logsDirectory)
            : base(logsDirectory, String.Empty)
        {

        }

        #endregion

        #region Methods

        protected override IEnumerable<LogItem> GetRecords(Stream stream)
        {
            XmlReaderSettings settings = new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Fragment };
            XmlReader reader = XmlReader.Create(stream, settings);
            XmlDocument doc = new XmlDocument();

            while (reader.ReadToNextSibling("E2ETraceEvent"))
            {
                LogItem item = new LogItem();
                doc.Load(reader.ReadSubtree());
                XmlNode root = doc.FirstChild;
                XmlNode systemNode = root["System"];
                XmlNode appDataNode = root["ApplicationData"];
                XmlNode traceRecordNode = appDataNode.FirstChild.FirstChild.FirstChild;

                item.Type = ResolveType(systemNode["SubType"]);
                item.TimeCreated = ResolveTime(systemNode);
                item.Description = traceRecordNode["Description"].InnerText;

                item.BasicInformation = new List<NameValuePair>
                {
                    new NameValuePair("Activity ID", systemNode["Correlation"].Attributes["ActivityID"].Value),
                    new NameValuePair("Time", ResolveTime(systemNode).ToString()),
                    new NameValuePair("Level", systemNode["SubType"].Attributes["Name"].Value),
                    new NameValuePair("Source", systemNode["Source"].Attributes["Name"].Value),
                    new NameValuePair("Process", systemNode["Execution"].Attributes["ProcessName"].Value),
                    new NameValuePair("Thread", systemNode["Execution"].Attributes["ThreadID"].Value),
                    new NameValuePair("Computer", systemNode["Computer"].InnerText),
                    new NameValuePair("Trace Identifier/Code", traceRecordNode["TraceIdentifier"].InnerText)
                };

                item.GeneralProperties = traceRecordNode.ChildNodes.OfType<XmlNode>()
                    .Where(x => !String.IsNullOrEmpty(x.InnerText) && !_excludedGeneralProperties.Contains(x.Name))
                    .Select(x => new NameValuePair(x.Name, x.InnerText));

                item.Exception = ResolveException(traceRecordNode["Exception"]);

                
                yield return item;
            }
        }

        private LogItemType ResolveType(XmlNode typeNode)
        {
            string name = typeNode.Attributes["Name"].Value;

            switch (name)
            {
                case "Error": return LogItemType.Error;
                case "Information": return LogItemType.Info;
                case "Warning": return LogItemType.Warning;
            }
            return LogItemType.Info;
        }

        private DateTime ResolveTime(XmlNode systemNode)
        {
            return DateTime.Parse(systemNode["TimeCreated"].Attributes["SystemTime"].Value, CultureInfo.InvariantCulture);
        }

        private ExceptionInfo ResolveException(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            XmlNode stackTrace = node["StackTrace"];
            return new ExceptionInfo
            {
                ExceptionType = node["ExceptionType"].InnerText,
                Message = node["Message"].InnerText,
                StackTrace = stackTrace != null ? stackTrace.InnerText : null,
                ExceptionString = node["ExceptionString"].InnerText,
                InnerException = ResolveException(node["InnerException"])
            };
        }

        #endregion
    }
}
