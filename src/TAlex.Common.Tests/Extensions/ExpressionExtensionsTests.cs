using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TAlex.Common.Extensions;


namespace TAlex.Common.Tests.Extensions
{
    [TestFixture]
    public class ExpressionExtensionsTests
    {
        #region ToProperty

        [Test]
        public void ToProperty_Expression_PropertyInfo()
        {
            //arrange
            Expression<Func<SimpleModel, object>> expr = x => x.Id;
            PropertyInfo expected = typeof(SimpleModel).GetProperty("Id");

            //action
            PropertyInfo actual = expr.ToProperty();

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GetPropertyName<TModel>(this Expression<Func<TModel, object>> expression)

        [Test]
        public void GetPropertyName_ExpressionForModel_PropertyName()
        {
            Expression<Func<SimpleModel, object>> expr = x => x.Text;
            string expected = "Text";

            //action
            string actual = expr.GetPropertyName();

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GetPropertyName<T>(this Expression<Func<T>> propertyExpression)

        [Test]
        public void GetPropertyName_Expression_PropertyName()
        {
            //arrange
            var model = new SimpleModel();

            Expression<Func<string>> expr = () => model.Text;
            string expected = "Text";

            //action
            string actual = expr.GetPropertyName();

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Test Data

        private class SimpleModel
        {
            public int Id { get; set; }

            public string Text { get; set; }
        }

        #endregion
    }
}
