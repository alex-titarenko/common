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

        #region RequiresNonNegative

        [TestCase(-1)]
        [TestCase(-5)]
        public void RequiresNonNegative_NegativeNumber_ThrowArgumentOutOfRangeException(long target)
        {
            //action
            TestDelegate code = () => Argument.RequiresNonNegative(target, nameof(target));

            //assert
            Assert.Throws<ArgumentOutOfRangeException>(code);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(153)]
        public void RequiresNonNegative_PositiveNumber_DoNoting(long target)
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
    }
}
