using System;
using System.Xml.Serialization;


namespace TAlex.Common.Diagnostics
{
    /// <summary>
    /// Represents the plain object of exception for serialization.
    /// </summary>
    [XmlRoot(ElementName = "Exception")]
    public class ExceptionInfo
    {
        /// <summary>
        /// Gets or sets the type of exception.
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets a message that describes the current exception.
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Gets or sets a string representation of the frames on the call stack at the time
        /// the current exception was thrown.
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets the exception string.
        /// </summary>
        public string ExceptionString { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TAlex.Common.Diagnostics.ExceptionInfo"/> instance that caused the current exception.
        /// </summary>
        public ExceptionInfo InnerException { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TAlex.Common.Diagnostics.ExceptionInfo"/> class.
        /// </summary>
        public ExceptionInfo()
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="TAlex.Common.Diagnostics.ExceptionInfo"/> class
        /// using the specified exception object.
        /// </summary>
        /// <param name="exc">An <see cref="System.Exception"/> object for converting.</param>
        public ExceptionInfo(Exception exc)
		{
			ExceptionType = exc.GetType().ToString();
			Message = exc.Message;
			StackTrace = exc.StackTrace;
			ExceptionString = exc.ToString();

			if (exc.InnerException != null)
			{
                InnerException = new ExceptionInfo(exc.InnerException);
			}
		}
    }
}
