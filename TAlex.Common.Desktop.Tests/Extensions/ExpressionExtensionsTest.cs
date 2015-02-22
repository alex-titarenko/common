using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using TAlex.Common.Extensions;
using System.Reflection;


namespace TAlex.Common.Test.Extensions
{
    /// <summary>
    /// Summary description for ExpressionExtensionsTest
    /// </summary>
    [TestClass]
    public class ExpressionExtensionsTest
    {
        [TestMethod]
        public void ToPropertyTest()
        {
            Expression<Func<SimpleModel, object>> expr = x => x.Id;

            PropertyInfo actual = expr.ToProperty();
            PropertyInfo expected = typeof(SimpleModel).GetProperty("Id");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPropertyNameTest()
        {
            Expression<Func<SimpleModel, object>> expr = x => x.Text;

            string actual = expr.GetPropertyName();
            string expected = "Text";

            Assert.AreEqual(expected, actual);
        }

        private class SimpleModel
        {
            public int Id { get; set; }

            public string Text { get; set; }
        }
    }
}
