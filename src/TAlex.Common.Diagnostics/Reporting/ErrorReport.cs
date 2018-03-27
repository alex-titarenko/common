using System;
using System.Reflection;
using TAlex.Common.Extensions;


namespace TAlex.Common.Diagnostics.Reporting
{
    /// <summary>
    /// Represents the model of the error report.
    /// </summary>
    public class ErrorReport
    {
        #region Properties

        /// <summary>
        /// Gets the target exception.
        /// </summary>
        public Exception TargetException
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the subject of the current error report.
        /// </summary>
        public string Subject
        {
            get
            {
                var assemblyInfo = Assembly.GetEntryAssembly().GetAssemblyInfo();
                return String.Format("#APPLICATION_ERROR: {0} {1}", assemblyInfo.Title, assemblyInfo.Version);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TAlex.Common.Diagnostics.Reporting.ErrorReport"/> class.
        /// </summary>
        /// <param name="exception">The target exception for which to get a report.</param>
        public ErrorReport(Exception exception)
        {
            TargetException = exception;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generate the full html error report, that including additional
        /// information about environment, system and currently executing process.
        /// </summary>
        /// <returns>A <see cref="System.String"/> represents the full html error report.</returns>
        public string GenerateFullHtmlReport()
        {
            ErrorReportHtmlTemplate template = new ErrorReportHtmlTemplate() { Model = this };
            return template.TransformText();
        }

        /// <summary>
        /// Generate the full plain text error report, that including additional
        /// information about environment, system and currently executing process.
        /// </summary>
        /// <returns>A <see cref="System.String"/> represents the full plain text error report.</returns>
        public string GenerateFullPlainTextReport()
        {
            ErrorReportPlainTextTemplate template = new ErrorReportPlainTextTemplate() { Model = this };
            return template.TransformText();
        }

        #endregion
    }
}
