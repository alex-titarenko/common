using System;
using System.Collections;
using System.Collections.Generic;


namespace TAlex.Common
{
    public static class Argument
    {
        public static void Requires(bool condition, string paramName, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, paramName);
            }
        }

        public static void RequiresNotNull(object obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName, $"{paramName} is null.");
            }
        }

        public static void RequiresNotNullOrEmpty(string str, string paramName)
        {
            Requires(!string.IsNullOrEmpty(str), paramName, $"{paramName} is null or empty.");
        }

        public static void RequiresNotNullOrWhiteSpace(string str, string paramName)
        {
            Requires(!string.IsNullOrWhiteSpace(str), paramName, $"{paramName} is null or whitespace.");
        }

        public static void RequiresNonNegative(long number, string paramName)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} is negative.");
            }
        }

        public static void RequiresNotEmpty(ICollection list, string paramName)
        {
            Requires(list.Count > 0, paramName, $"{paramName} is empty.");
        }
    }
}
