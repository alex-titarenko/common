using System;
using NUnit.Framework;
using TAlex.Common.Extensions;


namespace TAlex.Common.Tests.Extensions
{
    [TestFixture]
    public class ObjectExtensionsTest
    {
        #region IsNull

        [Test]
        public void IsNull_NullObject_True()
        {
            //arrange
            object target = null;

            //action
            var actual = target.IsNull();

            //assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void IsNull_NotNullObject_False()
        {
            //arrange
            object target = "Test";

            //action
            var actual = target.IsNull();

            //assert
            Assert.IsFalse(actual);
        }

        #endregion

        #region IsNotNull

        [Test]
        public void IsNotNull_NotNullObject_True()
        {
            //arrange
            object target = "Test";

            //action
            var actual = target.IsNotNull();

            //assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void IsNotNull_NullObject_False()
        {
            //arrange
            object target = null;

            //action
            var actual = target.IsNotNull();

            //assert
            Assert.IsFalse(actual);
        }

        #endregion
    }
}
