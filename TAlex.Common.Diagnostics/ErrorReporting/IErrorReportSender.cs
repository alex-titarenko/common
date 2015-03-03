namespace TAlex.Common.Diagnostics.ErrorReporting
{
    public interface IErrorReportSender
    {
        void Send(ErrorReportModel report);
    }
}
