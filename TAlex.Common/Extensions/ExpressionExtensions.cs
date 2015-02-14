using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace TAlex.Common.Extensions
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Converts the lambda expression to property info.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <param name="expression">Lambda expression that represents the property of the model.</param>
        /// <returns><see cref="System.Reflection.PropertyInfo"/> by specified <paramref name="expression"/>.</returns>
        public static PropertyInfo ToProperty<TModel>(this Expression<Func<TModel, object>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException();

            Expression body = expression.Body;
            MemberExpression op = null;

            if (body is UnaryExpression)
                op = (body as UnaryExpression).Operand as MemberExpression;
            else if (body is MemberExpression)
                op = body as MemberExpression;

            PropertyInfo property = null;
            if (op != null)
            {
                MemberInfo member = op.Member;
                property = typeof(TModel).GetTypeInfo().GetDeclaredProperty(member.Name);

                if (property == null)
                {
                    throw new ArgumentException();
                }
            }

            return property;
        }

        /// <summary>
        /// Returns the property name by the specified lambda expression.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <param name="expression">Lambda expression that represents the property of the model.</param>
        /// <returns><see cref="System.String"/> represents the property name for the specified <paramref name="expression"/>.</returns>
        public static string GetPropertyName<TModel>(this Expression<Func<TModel, object>> expression)
        {
            PropertyInfo info = ToProperty(expression);
            return (info != null) ? info.Name : null;
        }

        /// <summary>
        /// Extracts the name of a property from an expression.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">An expression returning the property's name.</param>
        /// <returns>The name of the property returned by the expression.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the expression is null.</exception>
        /// <exception cref="T:System.ArgumentException">If the expression does not represent a property.</exception>
        public static string GetPropertyName<T>(this Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }
            MemberExpression body = propertyExpression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }
            PropertyInfo member = body.Member as PropertyInfo;
            if (member == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }
            return member.Name;
        }
    }

    public static class PropertyName
    {
        public static string Get<T>(Expression<Func<T>> expression)
        {
            return expression.GetPropertyName();
        }

        public static string Get<T>(Expression<Func<T, object>> expression)
        {
            return expression.GetPropertyName();
        }
    }
}
