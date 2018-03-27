using NUnit.Framework;
using System;
using System.Collections.Generic;


namespace TAlex.Common.Tests
{
    [TestFixture]
    class ArgumentTests
    {
        #region Requires

        [Test]
        public void Requires_False_ThrowArgumentException()
        {
            //arrange
            var errorMessage = "Param error!";

            //action
            TestDelegate code = () => Argument.Requires(false, "paramName", errorMessage);

            //assert
            Assert.Throws<ArgumentException>(code, errorMessage);
        }

        [Test]
        public void Requires_True_DoNothing()
        {
            //arrange
            var errorMessage = "Param error!";

            //action
            TestDelegate code = () => Argument.Requires(true, "paramName", errorMessage);

            //assert
            Assert.DoesNotThrow(code);
        }

        #endregion

        #region RequiresNotNull

        [TestCase(null)]
        public void RequiresNotNull_Null_ThrowNullArgumentException(object target)
        {
            //action
            TestDelegate code = () => Argument.RequiresNotNull(target, nameof(target));

            //assert
            Assert.Throws<ArgumentNullException>(code);
        }

        [TestCase("Test")]
        [TestCase(3)]
        public void RequiresNotNull_NotNull_DoNothing(object target)
        {
            //action
            TestDelegate code = () => Argument.RequiresNotNull(target, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        #endregion

        #region RequiresNotNullOrEmpty

        [TestCase(null)]
        [TestCase("")]
        public void RequiresNotNullOrEmpty_NullOrEmpty_ThrowArgumentException(string target)
        {
            //action
            TestDelegate code = () => Argument.RequiresNotNullOrEmpty(target, nameof(target));

            //assert
            Assert.Throws<ArgumentException>(code);
        }

        [TestCase("World")]
        [TestCase("Hello World")]
        [TestCase("   ")]
        public void RequiresNotNullOrEmpty_NotEmptyString_DoNothing(string target)
        {
            //action
            TestDelegate code = () => Argument.RequiresNotNullOrEmpty(target, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        #endregion

        #region RequiresNotNullOrWhiteSpace

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void RequiresNotNullOrWhiteSpace_NullOrWhiteSpace_ThrowArgumentException(string target)
        {
            //action
            TestDelegate code = () => Argument.RequiresNotNullOrWhiteSpace(target, nameof(target));

            //assert
            Assert.Throws<ArgumentException>(code);
        }

        [TestCase("World")]
        [TestCase("Hello World")]
        public void RequiresNotNullOrWhiteSpace_NotEmptyString_DoNothing(string target)
        {
            //action
            TestDelegate code = () => Argument.RequiresNotNullOrWhiteSpace(target, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        #endregion

        #region RequiresNonNegative [Long]

        [TestCase(-1)]
        [TestCase(-5)]
        public void RequiresNonNegative_Integer_NegativeNumber_ThrowArgumentOutOfRangeException(long target)
        {
            //action
            TestDelegate code = () => Argument.RequiresNonNegative(target, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(153)]
        public void RequiresNonNegative_Integer_PositiveNumber_DoNoting(long target)
        {
            //action
            TestDelegate code = () => Argument.RequiresNonNegative(target, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        #endregion

        #region RequiresNonNegative [Double]

        [TestCase(-1.5)]
        [TestCase(-5.0)]
        public void RequiresNonNegative_Real_NegativeNumber_ThrowArgumentOutOfRangeException(double target)
        {
            //action
            TestDelegate code = () => Argument.RequiresNonNegative(target, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        [TestCase(0.0)]
        [TestCase(1.2)]
        [TestCase(153.0)]
        public void RequiresNonNegative_Real_PositiveNumber_DoNoting(double target)
        {
            //action
            TestDelegate code = () => Argument.RequiresNonNegative(target, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        #endregion

        #region RequiresNotEmpty

        [Test]
        public void RequiresNotEmpty_EmptyArray_ThrowArgumentException()
        {
            //arrange
            var target = new int[0];

            //action
            TestDelegate code = () => Argument.RequiresNotEmpty(target, nameof(target));

            //assert
            Assert.Throws<ArgumentException>(code);
        }

        [Test]
        public void RequiresNotEmpty_EmptyList_ThrowArgumentException()
        {
            //arrange
            var target = new List<string>();

            //action
            TestDelegate code = () => Argument.RequiresNotEmpty(target, nameof(target));

            //assert
            Assert.Throws<ArgumentException>(code);
        }

        [Test]
        public void RequiresNotEmpty_EmptyDictionary_ThrowArgumentException()
        {
            //arrange
            var target = new Dictionary<string, object>();

            //action
            TestDelegate code = () => Argument.RequiresNotEmpty(target, nameof(target));

            //assert
            Assert.Throws<ArgumentException>(code);
        }

        [Test]
        public void RequiresNotEmpty_NotEmptyArray_DoNothing()
        {
            //arrange
            var target = new[] { 2, 3 };

            //action
            TestDelegate code = () => Argument.RequiresNotEmpty(target, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        #endregion

        #region RequiresGreaterThan [Long]

        [TestCase(5, 2)]
        [TestCase(1, -12)]
        public void RequiresGreaterThan_Integer_GreaterNumber_DoNothing(long target, long lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresGreaterThan(target, lowerBound, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        [TestCase(2, 5)]
        [TestCase(-12, 1)]
        [TestCase(3, 3)]
        public void RequiresGreaterThan_Integer_LessOrEqualNumber_ThrowArgumentOutOfRangeException(long target, long lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresGreaterThan(target, lowerBound, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        #endregion

        #region RequiresGreaterThan [Double]

        [TestCase(5.2, 2.1)]
        [TestCase(1.3, -12.0)]
        public void RequiresGreaterThan_Real_GreaterNumber_DoNothing(double target, double lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresGreaterThan(target, lowerBound, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        [TestCase(2.4, 5.5)]
        [TestCase(-12.6, 1.1)]
        [TestCase(3.2, 3.2)]
        public void RequiresGreaterThan_Real_LessOrEqualNumber_ThrowArgumentOutOfRangeException(double target, double lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresGreaterThan(target, lowerBound, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        #endregion

        #region RequiresGreaterThanOrEqual [Long]

        [TestCase(5, 2)]
        [TestCase(1, -12)]
        [TestCase(3, 3)]
        public void RequiresGreaterThanOrEqual_Integer_GreaterOrEqualNumber_DoNothing(long target, long lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresGreaterThanOrEqual(target, lowerBound, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        [TestCase(2, 5)]
        [TestCase(-12, 1)]
        public void RequiresGreaterThanOrEqual_Integer_LessNumber_ThrowArgumentOutOfRangeException(long target, long lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresGreaterThanOrEqual(target, lowerBound, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        #endregion

        #region RequiresGreaterThanOrEqual [Double]

        [TestCase(5.2, 2.1)]
        [TestCase(1.3, -12.0)]
        [TestCase(3.2, 3.2)]
        public void RequiresGreaterThanOrEqual_Real_GreaterOrEqualNumber_DoNothing(double target, double lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresGreaterThanOrEqual(target, lowerBound, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        [TestCase(2.4, 5.5)]
        [TestCase(-12.6, 1.1)]
        public void RequiresGreaterThanOrEqual_Real_LessNumber_ThrowArgumentOutOfRangeException(double target, double lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresGreaterThanOrEqual(target, lowerBound, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        #endregion

        #region RequiresLessThan [Long]

        [TestCase(2, 5)]
        [TestCase(-12, 1)]
        public void RequiresLessThan_Integer_LessNumber_DoNothing(long target, long lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresLessThan(target, lowerBound, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        [TestCase(5, 2)]
        [TestCase(1, -12)]
        [TestCase(3, 3)]
        public void RequiresLessThan_Integer_GreaterOrEqualNumber_ThrowArgumentOutOfRangeException(long target, long lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresLessThan(target, lowerBound, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        #endregion

        #region RequiresLessThan [Double]

        [TestCase(2.4, 5.5)]
        [TestCase(-12.6, 1.1)]
        public void RequiresLessThan_Real_LessNumber_DoNothing(double target, double lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresLessThan(target, lowerBound, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        [TestCase(5.2, 2.1)]
        [TestCase(1.3, -12.0)]
        [TestCase(3.2, 3.2)]
        public void RequiresLessThan_Real_GreaterOrEqualNumber_ThrowArgumentOutOfRangeException(double target, double lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresLessThan(target, lowerBound, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        #endregion

        #region RequiresLessThanOrEqual [Long]

        [TestCase(2, 5)]
        [TestCase(-12, 1)]
        [TestCase(3, 3)]
        public void RequiresLessThanOrEqual_Integer_LessOrEqualNumber_DoNothing(long target, long lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresLessThanOrEqual(target, lowerBound, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        [TestCase(5, 2)]
        [TestCase(1, -12)]
        public void RequiresLessThanOrEqual_Integer_GreaterNumber_ThrowArgumentOutOfRangeException(long target, long lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresLessThanOrEqual(target, lowerBound, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        #endregion

        #region RequiresLessThanOrEqual [Double]

        [TestCase(2.1, 5.2)]
        [TestCase(-12.0, 1.1)]
        [TestCase(3.2, 3.2)]
        public void RequiresLessThanOrEqual_Real_LessOrEqualNumber_DoNothing(double target, double lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresLessThanOrEqual(target, lowerBound, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        [TestCase(5.5, 2.4)]
        [TestCase(1.1, -12.6)]
        public void RequiresLessThanOrEqual_Real_GreaterNumber_ThrowArgumentOutOfRangeException(double target, double lowerBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresLessThanOrEqual(target, lowerBound, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        #endregion

        #region RequiresInRange [Long]

        [TestCase(0, 0, 0)]
        [TestCase(2, 2, 2)]
        [TestCase(2, -1, 18)]
        [TestCase(2, 2, 18)]
        [TestCase(2, -1, 2)]
        public void RequiresInRange_Integer_InRange_DoNothing(long target, long lowerBound, long upperBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresInRange(target, lowerBound, upperBound, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        [TestCase(-2, -1, 18)]
        [TestCase(19, 2, 18)]
        [TestCase(333, -1, 2)]
        public void RequiresInRange_Integer_OutOfRange_ThrowArgumentOutOfRangeException(long target, long lowerBound, long upperBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresInRange(target, lowerBound, upperBound, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        #endregion

        #region RequiresInRange [Double]

        [TestCase(0, 0, 0)]
        [TestCase(2.1, 2.1, 2.1)]
        [TestCase(2.3, -1, 18.6)]
        [TestCase(2.3, 2.3, 18.6)]
        [TestCase(2.3, -1.8, 2.3)]
        public void RequiresInRange_Real_InRange_DoNothing(double target, double lowerBound, double upperBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresInRange(target, lowerBound, upperBound, nameof(target));

            //assert
            Assert.DoesNotThrow(code);
        }

        [TestCase(-2.5, -1.6, 18.1)]
        [TestCase(19.2, 2.6, 18.5)]
        [TestCase(333.3, -1.7, 2.0)]
        public void RequiresInRange_Real_OutOfRange_ThrowArgumentOutOfRangeException(double target, double lowerBound, double upperBound)
        {
            //action
            TestDelegate code = () => Argument.RequiresInRange(target, lowerBound, upperBound, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        #endregion
    }
}
