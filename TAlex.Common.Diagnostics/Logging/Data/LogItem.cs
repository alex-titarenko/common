using System;
using System.Collections.Generic;
using System.Text;
using TAlex.Common.Diagnostics;


namespace TAlex.Common.Diagnostics.Logging.Data
{
    public class LogItem
    {
        public virtual string Description { get; set; }
        
        public virtual LogItemType Type { get; set; }

        public virtual DateTime TimeCreated { get; set; }


        public virtual IEnumerable<NameValuePair> BasicInformation { get; set; }

        public virtual IEnumerable<NameValuePair> GeneralProperties { get; set; }

        public virtual ExceptionInfo Exception { get; set; }


        public LogItem()
        {
        }
    }
}
