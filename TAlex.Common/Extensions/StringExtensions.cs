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
        private static readonly Regex CamelTextRegex = new Regex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])");
        private static readonly Regex HtmlTagRegex = new Regex("(<.*?>\\s*)+");


        /// <summary>
        /// Splits the string by new line separators.
        /// </summary>
        /// <param name="source">The source string to spliting.</param>
        /// <returns>array of line strings.</returns>
        public static string[] SplitByLines(this String source)
        {
            return NewLineRegex.Split(source);
        }

        /// <summary>
        /// Returns the reduced string given the structure of words.
        /// </summary>
        /// <param name="source">The source string to cutting.</param>
        /// <param name="length">The maximum length of result string.</param>
        /// <param name="addEllipsis">Flag that indicate adding three dots at the end.</param>
        /// <returns>reduced string.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">length is less than zero.</exception>
        public static string Cut(this String source, int length, bool addEllipsis = false)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", "Length must be equal or greater than zero.");

            if (String.IsNullOrEmpty(source) || length >= source.Length)
                return source;

            if (Char.IsPunctuation(source[length]))
                return source.Substring(0, length);

            int removeIndex;
            for (removeIndex = length; removeIndex >= 0; removeIndex--)
            {
                if (Char.IsWhiteSpace(source[removeIndex])) break;
            }

            string result = source.Substring(0, removeIndex + 1).Trim().TrimEnd(',');
            return (result.Length < length && addEllipsis) ? result + " ..." : result;
        }

        public static string ExtractTextFromHtml(this String source)
        {
            return HtmlTagRegex.Replace(source, " ").Trim();
        }

        /// <summary>
        /// Returns camel string in regular format.
        /// </summary>
        /// <param name="text">The source string for converting.</param>
        /// <returns>string in regular format.</returns>
        public static string CamelToRegular(String text)
        {
            if (String.IsNullOrEmpty(text))
                return text;

            return CamelTextRegex.Replace(text, (m) => " " + m.Value);
        }

        /// <summary>
        /// Returns encoded html string. Can be used as anti-spam email encoder.
        /// </summary>
        /// <param name="source">The source string for encode</param>
        /// <returns>encoded html string.</returns>
        public static string EncodeHtml(this String source)
        {
            return String.Join(String.Empty, source.Cast<char>().Select(x => String.Format("&#{0:D3};", (int)x)));
        }
    }
}
