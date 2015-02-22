using System;
using System.Text;


namespace TAlex.Common
{
    /// <summary>
    /// Represents the extended methods for converting data types, such as bytes to string.
    /// </summary>
    public static class ConvertEx
    {
        #region Fields

        private const long BytesInKilobyte = 1024L;
        private const long BytesInMegabyte = 1048576L;
        private const long BytesInGigabyte = 1073741824L;
        private const long BytesInTerabyte = 1099511627776L;

        #endregion

        #region Methods

        /// <summary>
        /// Converts the number of bytes to display string.
        /// </summary>
        /// <param name="bytes">A <see cref="System.Int64"/> represents the number of bytes for converting.</param>
        /// <returns>string converting from bytes.</returns>
        public static string BytesToDisplayString(long bytes)
        {
            if (bytes < BytesInKilobyte)
                return String.Format("{0} bytes", bytes);
            else if (bytes < BytesInMegabyte)
                return String.Format("{0} KB", Round2Digits((double)bytes / BytesInKilobyte));
            else if (bytes < BytesInGigabyte)
                return String.Format("{0} MB", Round2Digits((double)bytes / BytesInMegabyte));
            else if (bytes < BytesInTerabyte)
                return String.Format("{0} GB", Round2Digits((double)bytes / BytesInGigabyte));
            else
                return String.Format("{0} TB", Round2Digits((double)bytes / BytesInTerabyte));
        }

        private static double Round2Digits(double value)
        {
            return Math.Round(value, 2);
        }

        #endregion
    }
}
