using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace TAlex.Common.Extensions
{
    /// <summary>
    /// Provides the information about current assembly,
    /// such as title, description, company, version, etc.
    /// </summary>
    public static class AssemblyExtensions
    {
        #region Fields

        private const string TitlePropertyName = "Title";
        private const string DescriptionPropertyName = "Description";
        private const string CompanyPropertyName = "Company";
        private const string ProductPropertyName = "Product";
        private const string CopyrightPropertyName = "Copyright";
        private const string TrademarkPropertyName = "Trademark";

        #endregion

        #region Extensions

        /// <summary>
        /// Returns the assembly's title.
        /// </summary>
        public static string GetTitle(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyTitleAttribute>(assembly, TitlePropertyName);
        }

        /// <summary>
        /// Returns the assembly's description.
        /// </summary>
        public static string GetDescription(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyDescriptionAttribute>(assembly, DescriptionPropertyName);
        }

        /// <summary>
        /// Returns the assembly's company.
        /// </summary>
        public static string GetCompany(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyCompanyAttribute>(assembly, CompanyPropertyName);
        }

        /// <summary>
        /// Returns the assembly's product.
        /// </summary>
        public static string GetProduct(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyProductAttribute>(assembly, ProductPropertyName);
        }

        /// <summary>
        /// Returns the assembly's copyright.
        /// </summary>
        public static string GetCopyright(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyCopyrightAttribute>(assembly, CopyrightPropertyName);
        }

        /// <summary>
        /// Returns the assembly's trademark.
        /// </summary>
        public static string GetTrademark(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyTrademarkAttribute>(assembly, TrademarkPropertyName);
        }

        /// <summary>
        /// Returns the assembly's version.
        /// </summary>
        public static Version GetVersion(this Assembly assembly)
        {
            return assembly.GetName().Version;
        }

        #endregion

        #region Methods

        private static string GetAssemblyProperty<T>(Assembly assembly, string propertyName)
        {
            string result = String.Empty;

            if (assembly == null) return null;

            object[] attributes = assembly.GetCustomAttributes(typeof(T)).ToArray();

            if (attributes.Length > 0)
            {
                T attribute = (T)attributes[0];
                PropertyInfo property = attribute.GetType().GetTypeInfo().GetDeclaredProperty(propertyName);

                if (property != null)
                {
                    result = property.GetValue(attributes[0], null) as string;
                }
            }

            return result;
        }

        #endregion
    }
}
