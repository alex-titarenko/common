using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;
using TAlex.Common.Configuration;
using NUnit.Framework;


namespace TAlex.Common.Configuration.Tests
{
    [TestFixture]
    public class ConfigurationHelperTests
    {
        #region Get

        [Test]
        public void Get_MissingConfiguration_Null()
        {
            //action
            string actual = ConfigurationHelper.Get("UnknownSetting");

            //assert
            Assert.IsNull(actual);
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void Get_TypedMissingConfiguration_ThrowInvalidCastException()
        {
            //action
            int actual = ConfigurationHelper.Get<int>("UnknownSetting");

            //assert
            Assert.IsNull(actual);
        }

        [Test]
        public void Get_StringSetting_String()
        {
            //arrange
            string expected = "Some String";

            //action
            string actual = ConfigurationHelper.Get("StringSetting");

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Get_IntSetting_Int()
        {
            //arrange
            int expected = 35;

            //action
            int actual = ConfigurationHelper.Get<int>("IntSetting");

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Get_DoubleSetting_Double()
        {
            //arrange
            double expected = 36.262;

            //action
            double actual = ConfigurationHelper.Get<double>("DoubleSetting");

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("en-US")]
        [TestCase("ru-RU")]
        public void Get_DoubleSettingAndDifferentCurrentThreadCulture_Double(string culture)
        {
            //arrange
            double expected = 36.262;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            
            //action
            double actual = ConfigurationHelper.Get<double>("DoubleSetting");

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Get_DateTimeSetting_DateTime()
        {
            //arrange
            DateTime expected = new DateTime(2012, 11, 23);
            
            //action
            DateTime actual = ConfigurationHelper.Get<DateTime>("DateTimeSetting");

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Get_BooleanSetting_Boolean()
        {
            //arrange
            bool expected = true;

            //action
            bool actual = ConfigurationHelper.Get<bool>("BooleanSetting");

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region TryGet

        [Test]
        public void TryGet_IntSetting_TrueAndInt()
        {
            //arrange
            int expected = 35;
            int actual;
            
            //action
            bool success = ConfigurationHelper.TryGet<int>("IntSetting", out actual);

            //assert
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(success);
        }

        [Test]
        public void TryGet_UnknownSetting_FalseAndZero()
        {
            //arrange
            int expected = 0;
            int actual;

            //action
            bool success = ConfigurationHelper.TryGet<int>("UnknownSetting", out actual);

            //assert
            Assert.AreEqual(expected, actual);
            Assert.IsFalse(success);
        }

        #endregion
    }
}
