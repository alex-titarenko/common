namespace TAlex.Common.Diagnostics.Reporting
{
    public class ErrorReportModel
    {
        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Subject { get; set; }

        public string Report { get; set; }

        public string Сomments { get; set; }


        public ErrorReportModel()
        {
            UserName = "Unknown User";
        }
    }
}
