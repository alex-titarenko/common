using System;
using System.Collections;
using System.Collections.Generic;


namespace TAlex.Common.Extensions
{
    /// <summary>
    /// Provides a set of static methods for querying objects that implement <see cref="System.Collections.Generic.IList{T}" />.
    /// </summary>
    public static class ListExtensions
    {
        private static Random _rand = new Random();


        /// <summary>
        /// Shuffles the elements of list.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">An <see cref="System.Collections.Generic.IList{T}" /> for shuffling.</param>
        public static void Shuffle<T>(this IList<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "Input array is null");
            }

            for (var i = source.Count - 1; i > 0; i--)
            {
                var j = _rand.Next(0, i);

                var temp = source[i];
                source[i] = source[j];
                source[j] = temp;
            }
        }
    }
}
