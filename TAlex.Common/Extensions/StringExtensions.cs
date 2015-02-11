using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace TAlex.Common.Extensions
{
    /// <summary>
    /// Provides a set of static helpers methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Regex NewLineRegex = new Regex("\r\n|\r|\n");


        /// <summary>
        /// Splits the string by new line separators.
        /// </summary>
        /// <param name="source">The source string to spliting.</param>
        /// <returns>array of line strings.</returns>
        public static string[] SplitByLines(this String source)
        {
            return NewLineRegex.Split(source);
        }
    }
}
