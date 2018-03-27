using System;
using System.Collections;


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

        public static void RequiresNonNegative(double number, string paramName)
        {
            if (number < 0.0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} is negative.");
            }
        }

        public static void RequiresNotEmpty(ICollection list, string paramName)
        {
            Requires(list.Count > 0, paramName, $"{paramName} is empty.");
        }

        public static void RequiresGreaterThan(long number, long lowerBound, string paramName)
        {
            if (number <= lowerBound)
            {
                throw new ArgumentOutOfRangeException(paramName, number, $"{paramName} is lower or equal then {lowerBound}");
            }
        }

        public static void RequiresGreaterThan(double number, double lowerBound, string paramName)
        {
            if (number <= lowerBound)
            {
                throw new ArgumentOutOfRangeException(paramName, number, $"{paramName} is lower or equal then {lowerBound}");
            }
        }

        public static void RequiresGreaterThanOrEqual(long number, long lowerBound, string paramName)
        {
            if (number < lowerBound)
            {
                throw new ArgumentOutOfRangeException(paramName, number, $"{paramName} is lower then {lowerBound}");
            }
        }

        public static void RequiresGreaterThanOrEqual(double number, double lowerBound, string paramName)
        {
            if (number < lowerBound)
            {
                throw new ArgumentOutOfRangeException(paramName, number, $"{paramName} is lower then {lowerBound}");
            }
        }

        public static void RequiresLessThan(long number, long upperBound, string paramName)
        {
            if (number >= upperBound)
            {
                throw new ArgumentOutOfRangeException(paramName, number, $"{paramName} is greater or equal then {upperBound}");
            }
        }

        public static void RequiresLessThan(double number, double upperBound, string paramName)
        {
            if (number >= upperBound)
            {
                throw new ArgumentOutOfRangeException(paramName, number, $"{paramName} is greater or equal then {upperBound}");
            }
        }

        public static void RequiresLessThanOrEqual(long number, long upperBound, string paramName)
        {
            if (number > upperBound)
            {
                throw new ArgumentOutOfRangeException(paramName, number, $"{paramName} is greater then {upperBound}");
            }
        }

        public static void RequiresLessThanOrEqual(double number, double upperBound, string paramName)
        {
            if (number > upperBound)
            {
                throw new ArgumentOutOfRangeException(paramName, number, $"{paramName} is greater then {upperBound}");
            }
        }

        public static void RequiresInRange(long number, long lowerBound, long upperBound, string paramName)
        {
            if (number < lowerBound || number > upperBound)
            {
                throw new ArgumentOutOfRangeException(paramName, number, $"{paramName} is out of the range [{lowerBound}; {upperBound}]");
            }
        }

        public static void RequiresInRange(double number, double lowerBound, double upperBound, string paramName)
        {
            if (number < lowerBound || number > upperBound)
            {
                throw new ArgumentOutOfRangeException(paramName, number, $"{paramName} is out of the range [{lowerBound}; {upperBound}]");
            }
        }
    }
}
