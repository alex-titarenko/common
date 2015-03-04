using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TAlex.Common.Configuration;


namespace TAlex.Common.Test.Configuration
{
    [TestClass]
    public class ConfigurationHelperTest
    {
        [TestMethod]
        public void Get_MissingConfiguration()
        {
            string actual = ConfigurationHelper.Get("UnknownSetting");
            Assert.IsNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Get_TypedMissingConfiguration()
        {
            int actual = ConfigurationHelper.Get<int>("UnknownSetting");
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Get_String()
        {
            string expected = "Some String";
            string actual = ConfigurationHelper.Get("StringSetting");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Int()
        {
            int expected = 35;
            int actual = ConfigurationHelper.Get<int>("IntSetting");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Double()
        {
            double expected = 36.262;
            double actual = ConfigurationHelper.Get<double>("DoubleSetting");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_WithDifferentCultures()
        {
            double expected = 36.262;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            double actual = ConfigurationHelper.Get<double>("DoubleSetting");
            Assert.AreEqual(expected, actual);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            actual = ConfigurationHelper.Get<double>("DoubleSetting");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_DateTime()
        {
            DateTime expected = new DateTime(2012, 11, 23);
            DateTime actual = ConfigurationHelper.Get<DateTime>("DateTimeSetting");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Boolean()
        {
            bool expected = true;
            bool actual = ConfigurationHelper.Get<bool>("BooleanSetting");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TryGet_Success()
        {
            int expected = 35;
            int actual;
            bool success = ConfigurationHelper.TryGet<int>("IntSetting", out actual);

            Assert.AreEqual(expected, actual);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TryGet_Fail()
        {
            int expected = 0;
            int actual;
            bool success = ConfigurationHelper.TryGet<int>("UnknownSetting", out actual);

            Assert.AreEqual(expected, actual);
            Assert.IsFalse(success);
        }
    }
}
