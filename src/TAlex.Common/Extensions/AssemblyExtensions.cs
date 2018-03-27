using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TAlex.Common.Models;


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
        /// <param name="assembly">A target assembly.</param>
        /// <returns>title of assembly.</returns>
        public static string GetTitle(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyTitleAttribute>(assembly, TitlePropertyName);
        }

        /// <summary>
        /// Returns the assembly's description.
        /// </summary>
        /// <param name="assembly">A target assembly.</param>
        /// <returns>descriptions of assembly.</returns>
        public static string GetDescription(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyDescriptionAttribute>(assembly, DescriptionPropertyName);
        }

        /// <summary>
        /// Returns the assembly's company.
        /// </summary>
        /// <param name="assembly">A target assembly.</param>
        /// <returns>company name of assembly.</returns>
        public static string GetCompany(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyCompanyAttribute>(assembly, CompanyPropertyName);
        }

        /// <summary>
        /// Returns the assembly's product.
        /// </summary>
        /// <param name="assembly">A target assembly.</param>
        /// <returns>product of assembly.</returns>
        public static string GetProduct(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyProductAttribute>(assembly, ProductPropertyName);
        }

        /// <summary>
        /// Returns the assembly's copyright.
        /// </summary>
        /// <param name="assembly">A target assembly.</param>
        /// <returns>copyright of assembly.</returns>
        public static string GetCopyright(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyCopyrightAttribute>(assembly, CopyrightPropertyName);
        }

        /// <summary>
        /// Returns the assembly's trademark.
        /// </summary>
        /// <param name="assembly">A target assembly.</param>
        /// <returns>trademark of assembly.</returns>
        public static string GetTrademark(this Assembly assembly)
        {
            return GetAssemblyProperty<AssemblyTrademarkAttribute>(assembly, TrademarkPropertyName);
        }

        /// <summary>
        /// Returns the assembly's version.
        /// </summary>
        /// <param name="assembly">A target assembly.</param>
        /// <returns>version of assembly.</returns>
        public static Version GetVersion(this Assembly assembly)
        {
            return assembly.GetName().Version;
        }

        /// <summary>
        /// returns the assembly's info.
        /// </summary>
        /// <param name="assembly">A target assembly.</param>
        /// <returns>assembly info.</returns>
        public static AssemblyInfo GetAssemblyInfo(this Assembly assembly)
        {
            return new AssemblyInfo(assembly);
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
