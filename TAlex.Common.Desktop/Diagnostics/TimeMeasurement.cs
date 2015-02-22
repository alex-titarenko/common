using System;
using System.Diagnostics;


namespace TAlex.Common.Diagnostics
{
    /// <summary>
    /// Represents a convenient mechanism for measuring the time perform the operation.
    /// </summary>
    public class TimeMeasurement : IDisposable
    {
        #region Fields

        private Stopwatch _stopwatch;

        private string _actionName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a time interval that indicates execution time of the action.
        /// </summary>
        public TimeSpan Elapsed
        {
            get
            {
                return _stopwatch.Elapsed;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TAlex.Common.Diagnostics.TimeMeasurement"/> class
        /// and start time measurement.
        /// </summary>
        /// <param name="actionName">A string represents the action name for executing.</param>
        public TimeMeasurement(string actionName)
        {
            _actionName = actionName;

            Trace.TraceInformation("'{0}' operation started in {1}.", actionName, DateTime.Now);

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executing the specified action and trace information about elapsed time.
        /// </summary>
        /// <param name="actionName">A string represents the action name for executing.</param>
        /// <param name="action">A delegate represents the action that needed run-time measurements.</param>
        public static void TraceAction(string actionName, Action action)
        {
            using (new TimeMeasurement(actionName))
            {
                action();
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Stop time measurement and trace elapsed time for action executing.
        /// </summary>
        public void Dispose()
        {
            _stopwatch.Stop();
            Trace.TraceInformation("'{0}' operation completed for {1}", _actionName, Elapsed);
        }

        #endregion
    }
}
