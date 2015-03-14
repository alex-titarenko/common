using System;
using System.Runtime.InteropServices;
using TAlex.Common.Helpers;


namespace TAlex.Common.Diagnostics
{
    /// <summary>
    /// Provides the information about system, such as total physical memory,
    /// processor architecture, etc.
    /// </summary>
    public class SystemInfo
    {
        #region Fields

        private static SystemInfo _instance;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the current system info.
        /// </summary>
        public static SystemInfo Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SystemInfo();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Gets the string representing the current processor architecture, such as x86, x64, etc.
        /// </summary>
        public virtual string ProcessorArchitecture
        {
            get
            {
                return System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            }
        }

        /// <summary>
        /// Gets the size of physical memory, in bytes.
        /// </summary>
        public virtual ulong TotalPhysicalMemory
        {
            get
            {
                MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
                if (GlobalMemoryStatusEx(memStatus))
                {
                    return memStatus.ullTotalPhys;
                }

                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Gets the size of physical memory, string representation.
        /// </summary>
        public virtual string TotalPhysicalMemoryText
        {
            get
            {
                return ConvertEx.BytesToDisplayString((long)TotalPhysicalMemory);
            }
        }

        /// <summary>
        /// Gets the size of physical memory available, in bytes.
        /// </summary>
        public virtual ulong AvailablePhysicalMemory
        {
            get
            {
                MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
                if (GlobalMemoryStatusEx(memStatus))
                {
                    return memStatus.ullAvailPhys;
                }

                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Gets the size of physical memory available, string representation.
        /// </summary>
        public virtual string AvailablePhysicalMemoryText
        {
            get
            {
                return ConvertEx.BytesToDisplayString((long)AvailablePhysicalMemory);
            }
        }

        /// <summary>
        /// Gets the size of used physical memory, in bytes.
        /// </summary>
        public virtual ulong UsedPhysicalMemory
        {
            get
            {
                return (TotalPhysicalMemory - AvailablePhysicalMemory);
            }
        }

        /// <summary>
        /// Gets the size of used physical memory, string representation.
        /// </summary>
        public virtual string UsedPhysicalMemoryText
        {
            get
            {
                return ConvertEx.BytesToDisplayString((long)UsedPhysicalMemory);
            }
        }

        /// <summary>
        /// Gets the rate of usage of physical memory.
        /// </summary>
        public virtual double PhysicalMemoryUsage
        {
            get
            {
                return UsedPhysicalMemory / (double)TotalPhysicalMemory;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a <see cref="TAlex.Common.Environment.SystemInfo"/> class.
        /// </summary>
        protected SystemInfo()
        {
        }

        #endregion

        #region Methods

        #region Native

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        #endregion

        #endregion

        #region Nested Types

        /// <summary>
        /// Contains information about the current state of both physical and virtual memory, including extended memory
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private class MEMORYSTATUSEX
        {
            /// <summary>
            /// Size of the structure, in bytes. You must set this member before calling GlobalMemoryStatusEx.
            /// </summary>
            public uint dwLength;

            /// <summary>
            /// Number between 0 and 100 that specifies the approximate percentage of physical memory that is in use (0 indicates no memory use and 100 indicates full memory use).
            /// </summary>
            public uint dwMemoryLoad;

            /// <summary>
            /// Total size of physical memory, in bytes.
            /// </summary>
            public ulong ullTotalPhys;

            /// <summary>
            /// Size of physical memory available, in bytes.
            /// </summary>
            public ulong ullAvailPhys;

            /// <summary>
            /// Size of the committed memory limit, in bytes. This is physical memory plus the size of the page file, minus a small overhead.
            /// </summary>
            public ulong ullTotalPageFile;

            /// <summary>
            /// Size of available memory to commit, in bytes. The limit is ullTotalPageFile.
            /// </summary>
            public ulong ullAvailPageFile;

            /// <summary>
            /// Total size of the user mode portion of the virtual address space of the calling process, in bytes.
            /// </summary>
            public ulong ullTotalVirtual;

            /// <summary>
            /// Size of unreserved and uncommitted memory in the user mode portion of the virtual address space of the calling process, in bytes.
            /// </summary>
            public ulong ullAvailVirtual;

            /// <summary>
            /// Size of unreserved and uncommitted memory in the extended portion of the virtual address space of the calling process, in bytes.
            /// </summary>
            public ulong ullAvailExtendedVirtual;

            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }
        }

        #endregion
    }
}
