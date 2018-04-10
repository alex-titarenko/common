using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;


namespace TAlex.Common.Diagnostics.Reporting
{
    public class ErrorReportSender : IErrorReportSender
    {
        #region IErrorReportSender Members

        public void Send(ErrorReportModel report, string url)
        {
            SendReport(CreateRequest(url), SerializeReport(report));
        }

        #endregion

        #region Methods

        private HttpWebRequest CreateRequest(string url)
        {
            if (!String.IsNullOrEmpty(url))
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";

                return request;
            }
            throw new Exception("Url for reporting is not specified.");
        }

        private void SendReport(HttpWebRequest request, byte[] bytes)
        {
            request.ContentLength = bytes.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            CheckResponse(request);
        }

        private byte[] SerializeReport(ErrorReportModel report)
        {
            var serializedReport = JsonConvert.SerializeObject(report);
            return Encoding.UTF8.GetBytes(serializedReport);
        }

        private void CheckResponse(HttpWebRequest request)
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new WebException(response.StatusDescription);
                }
            }
        }

        #endregion
    }
}
