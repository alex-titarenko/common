namespace TAlex.Common.Diagnostics.Reporting
{
    public interface IErrorReportSender
    {
        void Send(ErrorReportModel report, string url);
    }
}
