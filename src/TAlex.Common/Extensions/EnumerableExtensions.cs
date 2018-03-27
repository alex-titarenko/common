using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace TAlex.Common.Extensions
{
    /// <summary>
    /// Provides a set of static methods for querying objects that implement <see cref="System.Collections.Generic.IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the randomized source enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">An <see cref="System.Collections.Generic.IEnumerable{T}" /> to return the randomized enumerable.</param>
        /// <returns>randomized source enumerable.</returns>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }


        /// <summary>
        /// Filters a sequence of values based on a search request.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">An <see cref="System.Collections.Generic.IEnumerable{T}" /> to return the search result.</param>
        /// <param name="query">A string representing the search request.</param>
        /// <param name="selectors">Sets of selectors by that needed for search executing.</param>
        /// <returns>An <see cref="System.Collections.Generic.IEnumerable{T}" /> that contains elements from the input sequence that satisfy the search request.</returns>
        /// <exception cref="System.NullReferenceException">query or selectors is null.</exception>
        public static IEnumerable<TSource> Search<TSource>(this IEnumerable<TSource> source, string query,
            IEnumerable<Func<TSource, object>> selectors)
        {
            return Search<TSource>(source, query, selectors, DefaultComplianceType.Strict);
        }

        /// <summary>
        /// Filters a sequence of values based on a search request.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">An <see cref="System.Collections.Generic.IEnumerable{T}" /> to return the search result.</param>
        /// <param name="query">A string representing the search request.</param>
        /// <param name="selectors">Sets of selectors by that needed for search executing.</param>
        /// <param name="complianceType"></param>
        /// <returns>An <see cref="System.Collections.Generic.IEnumerable{T}" /> that contains elements from the input sequence that satisfy the search request.</returns>
        /// <exception cref="System.NullReferenceException">query or selectors is null.</exception>
        public static IEnumerable<TSource> Search<TSource>(this IEnumerable<TSource> source, string query,
            IEnumerable<Func<TSource, object>> selectors, DefaultComplianceType complianceType)
        {
            return Search<TSource>(source, query, selectors, DefaultOperator.Or, complianceType);
        }

        /// <summary>
        /// Filters a sequence of values based on a search request.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">An <see cref="System.Collections.Generic.IEnumerable{T}" /> to return the search result.</param>
        /// <param name="query">A string representing the search request.</param>
        /// <param name="selectors">Sets of selectors by that needed for search executing.</param>
        /// <param name="op">A default operator for searching.</param>
        /// <param name="complianceType">A default compliance type for searching.</param>
        /// <returns>An <see cref="System.Collections.Generic.IEnumerable{T}" /> that contains elements from the input sequence that satisfy the search request.</returns>
        /// <exception cref="System.NullReferenceException">query or selectors is null.</exception>
        public static IEnumerable<TSource> Search<TSource>(this IEnumerable<TSource> source, string query,
            IEnumerable<Func<TSource, object>> selectors, DefaultOperator op, DefaultComplianceType complianceType)
        {
            return EnumerableSearcher<TSource>.Search(source, query, selectors, op, complianceType);
        }
    }

    public class EnumerableSearcher<T>
    {
        private const string WordBoundary = @"\b";
        private const string Wildcard = ".*";
        private const string OrSeparator = "|";

        private static char[] _delimeters = new char[]
            {
                ' ', '|', '\n', '\r', '\t', '=',
            };

        private static char[] _trimChars = new char[]
            {
                '.', ',', ';', '!', '-', '\\', '/'
            };

        private static string[] _escapeChars = new string[]
            {
                "\\", "[", "]", "(", ")", "{", "}", "^", "$", "#", ".", "+", "|"
            };


        public DefaultOperator Operator { get; set; }

        public DefaultComplianceType ComplianceType { get; set; }


        public IEnumerable<T> Search(IEnumerable<T> source, string query, IEnumerable<Func<T, object>> selectors)
        {
            return Search(source, query, selectors, Operator, ComplianceType);
        }

        public static IEnumerable<T> Search(IEnumerable<T> source, string query,
            IEnumerable<Func<T, object>> selectors,
            DefaultOperator op, DefaultComplianceType complianceType)
        {
            return source.Where(BuildSearchPredicate(query, selectors, op, complianceType));
        }

        public static Func<T, bool> BuildSearchPredicate(string query, IEnumerable<Func<T, object>> selectors, DefaultOperator op, DefaultComplianceType complianceType)
        {
            string[] patterns = BuildPatterns(query, complianceType);

            switch (op)
            {
                case DefaultOperator.Or:
                    Regex regex = GetSearchPatternRegex(String.Join(OrSeparator, patterns));
                    return x => x != null && IsOrMatch(x, regex, selectors);

                case DefaultOperator.And:
                    Regex[] regexes = patterns.Select(x => GetSearchPatternRegex(x)).ToArray();
                    return x => x != null && IsAndMatch(x, regexes, selectors);

                default:
                    throw new NotImplementedException();
            }
        }

        private static string[] BuildPatterns(string query, DefaultComplianceType complianceType)
        {
            string[] words = SplitStringIntoWords(NormalizeString(query));

            string prefix = String.Empty;
            string suffix = String.Empty;

            switch (complianceType)
            {
                case DefaultComplianceType.Strict:
                    prefix = WordBoundary;
                    suffix = WordBoundary;
                    break;

                case DefaultComplianceType.Beginning:
                    prefix = WordBoundary;
                    suffix = Wildcard;
                    break;

                case DefaultComplianceType.Ending:
                    prefix = Wildcard;
                    suffix = WordBoundary;
                    break;

                case DefaultComplianceType.Occurrence:
                    prefix = Wildcard;
                    suffix = Wildcard;
                    break;
            }

            return words.Select(x => String.Format("({0}{1}{2})", prefix, Escape(x), suffix)).ToArray();
        }

        private static Regex GetSearchPatternRegex(string pattern)
        {
            return new Regex(pattern, RegexOptions.IgnoreCase);
        }

        private static bool IsOrMatch(T item, Regex searchRegex, IEnumerable<Func<T, object>> selectors)
        {
            foreach (Func<T, object> selector in selectors)
            {
                object value = selector(item);

                if (value != null && searchRegex.IsMatch(value.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsAndMatch(T item, Regex[] searchRegexes, IEnumerable<Func<T, object>> selectors)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Func<T, object> selector in selectors)
            {
                sb.Append(" " + selector(item));
            }
            string fullText = sb.ToString();

            foreach (Regex regex in searchRegexes)
            {
                if (!regex.IsMatch(fullText))
                    return false;
            }

            return true;
        }

        private static string NormalizeString(string str)
        {
            return str.Trim().ToLowerInvariant();
        }

        private static string[] SplitStringIntoWords(string str)
        {
            return str
                .Split(_delimeters, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim(_trimChars))
                .ToArray();
        }

        private static string Escape(string str)
        {
            foreach (string ch in _escapeChars)
            {
                str = str.Replace(ch, @"\" + ch);
            }

            str = str.Replace("*", ".*");
            str = str.Replace("?", ".");

            return str;
        }
    }

    /// <summary>
    /// Represents the default operator for parsing queries.
    /// </summary>
    public enum DefaultOperator
    {
        /// <summary>
        /// Requires that all terms in the search results.
        /// </summary>
        And,

        /// <summary>
        /// Requires at least one of the terms in the search results.
        /// </summary>
        Or
    }

    /// <summary>
    /// Represents the default type of search compliance.
    /// </summary>
    public enum DefaultComplianceType
    {
        /// <summary>
        /// Compliance type, which requires full compliance by comparing the words.
        /// </summary>
        Strict,

        /// <summary>
        /// Compliance type, in which variation is permitted endings by comparison.
        /// </summary>
        Beginning,

        /// <summary>
        /// Compliance type, in which variation is permitted beginnings by comparison.
        /// </summary>
        Ending,

        /// <summary>
        /// Compliance type, in which variation is permitted beginnings
        /// and endings by comparison.
        /// </summary>
        Occurrence
    }
}
