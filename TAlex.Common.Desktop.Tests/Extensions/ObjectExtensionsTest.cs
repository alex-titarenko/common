using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TAlex.Common.Extensions;


namespace TAlex.Common.Test.Extensions
{
    [TestClass]
    public class ObjectExtensionsTest
    {
        [TestMethod]
        public void IsNullTest()
        {
            object actual = null;
            Assert.IsTrue(actual.IsNull());
        }

        [TestMethod]
        public void IsNullTest_False()
        {
            object actual = "Test";
            Assert.IsFalse(actual.IsNull());
        }

        [TestMethod]
        public void IsNotNullTest()
        {
            object actual = "Test";
            Assert.IsTrue(actual.IsNotNull());
        }

        [TestMethod]
        public void IsNotNullTest_False()
        {
            object actual = null;
            Assert.IsFalse(actual.IsNotNull());
        }
    }
}
