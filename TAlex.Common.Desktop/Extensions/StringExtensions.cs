using System;
using System.Text;
using System.Text.RegularExpressions;


namespace TAlex.Common.Extensions
{
    /// <summary>
    /// Provides a set of static helpers methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Regex NewLineRegex = new Regex("\r\n|\r|\n", RegexOptions.Compiled);
        private static readonly Regex CamelTextRegex = new Regex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled);
        private static readonly Regex HtmlTagRegex = new Regex("(<.*?>\\s*)+", RegexOptions.Compiled);


        /// <summary>
        /// Returns the reduced string given the structure of words.
        /// </summary>
        /// <param name="source">The source string to cutting.</param>
        /// <param name="length">The maximum length of result string.</param>
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
    }
}
