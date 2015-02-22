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
        private const int ShuffleModifier = 10;

        private static Random _rand = new Random();


        /// <summary>
        /// Shuffles the elements of list.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">An <see cref="System.Collections.Generic.IList{T}" /> for shuffling.</param>
        public static void Shuffle<T>(this IList<T> source)
        {
            int itemsCount = source.Count;
            int shuffleIters = itemsCount * ShuffleModifier;

            for (int i = 0; i < shuffleIters; i++)
            {
                int n1 = _rand.Next(0, itemsCount);
                int n2 = _rand.Next(0, itemsCount);

                if (n1 != n2)
                {
                    T temp = source[n1];
                    source[n1] = source[n2];
                    source[n2] = temp;
                }
            }
        }
    }
}
