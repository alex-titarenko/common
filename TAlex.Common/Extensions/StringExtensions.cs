using System;
using System.Collections.Generic;
using System.Linq;
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

            string result = null;
            if (Char.IsPunctuation(source[length]))
            {
                result = source.Substring(0, length);
            }
            else
            {
                int removeIndex;
                for (removeIndex = length; removeIndex >= 0; removeIndex--)
                {
                    if (Char.IsWhiteSpace(source[removeIndex])) break;
                }
                result = source.Substring(0, removeIndex + 1).Trim().TrimEnd(',');
            }

            return (result.Length < source.Length && addEllipsis) ? result + " ..." : result;
        }

        public static string ExtractTextFromHtml(this String source)
        {
            return (source != null) ? HtmlTagRegex.Replace(source, " ").Trim() : null;
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

        /// <summary>
        /// Returns plural form of singular noun.
        /// </summary>
        /// <param name="source">The singular noun.</param>
        /// <returns>plural string.</returns>
        /// <remarks>
        /// https://www.grammarly.com/blog/plural-nouns
        /// </remarks>
        public static string Pluralize(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            var irregularNouns = new Dictionary<string, string>
            {
                { "child", "children" },
                { "goose", "geese" },
                { "man", "men" },
                { "woman", "women" },
                { "tooth", "teeth" },
                { "foot", "feet" },
                { "mouse", "mice" },
                { "person", "people" }
            };

            if (source.EndsWithIgnoreCase(irregularNouns.Keys.ToArray(), out string match))
            {
                return source.Substring(0, source.Length - match.Length) + irregularNouns[match];
            }

            if (source.EndsWithIgnoreCase("news", "sheep", "series", "species", "deer"))
            {
                return source;
            }
            else if (source.EndsWithIgnoreCase("us") && !source.EndsWithIgnoreCase("bus"))
            {
                return source.Substring(0, source.Length - 2) + "i";
            }
            if (source.EndsWithIgnoreCase("is"))
            {
                return source.Substring(0, source.Length - 2) + "es";
            }
            else if (source.EndsWithIgnoreCase("s", "ss", "sh", "ch", "x", "z"))
            {
                return source + "es";
            }
            else if (source.EndsWithIgnoreCase("f") && !source.EndsWithIgnoreCase("roof", "belief", "chef", "chief"))
            {
                return source.Substring(0, source.Length - 1) + "ves";
            }
            else if (source.EndsWithIgnoreCase("fe"))
            {
                return source.Substring(0, source.Length - 2) + "ves";
            }
            else if (source.EndsWithIgnoreCase("y") && !source.EndsWithIgnoreCase("ay", "ey", "iy", "oy", "uy"))
            {
                return source.Substring(0, source.Length - 1) + "ies";
            }
            else if (source.EndsWithIgnoreCase("o") && !source.EndsWithIgnoreCase("photo", "piano", "halo"))
            {
                return source + "es";
            }
            else if (source.EndsWithIgnoreCase("on"))
            {
                return source.Substring(0, source.Length - 2) + "a";
            }

            return source + 's';
        }

        /// <summary>
        /// Determines whether the end of this string instance matches the specified strings
        /// ignoring case.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="values">The string array to compare to the substring at the end of source string.</param>
        /// <returns>true if the values parameter matches the end of source string; otherwise, false.</returns>
        public static bool EndsWithIgnoreCase(this string source, params string[] values)
        {
            return EndsWith(source, StringComparison.OrdinalIgnoreCase, values);
        }

        /// <summary>
        /// Determines whether the end of this string instance matches the specified strings
        /// ignoring case.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="values">The string array to compare to the substring at the end of source string.</param>
        /// <param name="match">Output parameter with comparison match string.</param>
        /// <returns>true if the values parameter matches the end of source string; otherwise, false.</returns>
        public static bool EndsWithIgnoreCase(this string source, string[] values, out string match)
        {
            foreach (var value in values)
            {
                if (source.EndsWith(value, StringComparison.OrdinalIgnoreCase))
                {
                    match = value;
                    return true;
                }
            }

            match = null;
            return false;
        }

        /// <summary>
        /// Determines whether the end of this string instance matches the specified strings
        /// when compared using the specified comparison option.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="comparisonType">One of the enumeration values that determines how this string and value are compared.</param>
        /// <param name="values">The string array to compare to the substring at the end of source string.</param>
        /// <returns>true if the values parameter matches the end of source string; otherwise, false.</returns>
        public static bool EndsWith(this string source, StringComparison comparisonType, params string[] values)
        {
            foreach (var value in values)
            {
                if (source.EndsWith(value, comparisonType))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
