using System;
using System.Diagnostics;


namespace TAlex.Common.Diagnostics
{
    /// <summary>
    /// Provides the basic information of the currently executing process.
    /// </summary>
    public class ProcessInfo
    {
        #region Fields

        private static ProcessInfo _instance;
        private static readonly PerformanceCounter _privateWorkingSet;

        #endregion

        #region Constructors

        static ProcessInfo()
        {
            _privateWorkingSet = new PerformanceCounter("Process", "Working Set - Private");
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="TAlex.Common.Diagnostics.ProcessInfo"/> class.
        /// </summary>
        protected ProcessInfo()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the current process info.
        /// </summary>
        public static ProcessInfo Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProcessInfo();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Gets the size of the physical memory that uses of current process.
        /// </summary>
        public virtual long PrivateWorkingSet
        {
            get
            {
                string processName = Process.GetCurrentProcess().ProcessName;
                _privateWorkingSet.InstanceName = processName;

                return _privateWorkingSet.RawValue;
            }
        }

        #endregion
    }
}
