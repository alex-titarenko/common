using System;
using System.Collections.Generic;
using System.Text;


namespace TAlex.Common.Diagnostics.Models
{
    public class TraceRecord
    {
        public string Description { get; set; }
        public ExceptionInfo Exception { get; set; }
        public string Handler { get; set; }
        public string HttpMethod { get; set; }
        public string PostData { get; set; }
        public string RequestUrl { get; set; }
        public string Status { get; set; }
        public string TraceIdentifier { get; set; }
        public string UrlReferrer { get; set; }
        public string UserAgent { get; set; }
        public string UserHostAddress { get; set; }
        public string UserName { get; set; }
    }
}
