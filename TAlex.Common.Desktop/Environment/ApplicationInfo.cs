using System;
using System.Reflection;


namespace TAlex.Common.Environment
{
    /// <summary>
    /// Provides the information about currently executing application,
    /// such as title, description, company, version, etc.
    /// </summary>
    public class ApplicationInfo
    {
        #region Fields

        private static ApplicationInfo _instance;

        private const string TitlePropertyName = "Title";
        private const string DescriptionPropertyName = "Description";
        private const string CompanyPropertyName = "Company";
        private const string ProductPropertyName = "Product";
        private const string CopyrightPropertyName = "Copyright";
        private const string TrademarkPropertyName = "Trademark";

        private static string _title;
        private static string _description;
        private static string _company;
        private static string _product;
        private static string _copyright;
        private static string _trademark;
        private static Version _version;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the application info class.
        /// </summary>
        public static ApplicationInfo Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApplicationInfo();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Gets the current application's title.
        /// </summary>
        public virtual string Title
        {
            get
            {
                if (_title == null)
                {
                    _title = GetAssemblyProperty<AssemblyTitleAttribute>(TitlePropertyName);
                }

                return _title;
            }
        }

        /// <summary>
        /// Gets the current application's description.
        /// </summary>
        public virtual string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = GetAssemblyProperty<AssemblyDescriptionAttribute>(DescriptionPropertyName);
                }

                return _description;
            }
        }

        /// <summary>
        /// Gets the current application's company.
        /// </summary>
        public virtual string Company
        {
            get
            {
                if (_company == null)
                {
                    _company = GetAssemblyProperty<AssemblyCompanyAttribute>(CompanyPropertyName);
                }

                return _company;
            }
        }

        /// <summary>
        /// Gets the current application's product.
        /// </summary>
        public virtual string Product
        {
            get
            {
                if (_product == null)
                {
                    _product = GetAssemblyProperty<AssemblyProductAttribute>(ProductPropertyName);
                }

                return _product;
            }
        }

        /// <summary>
        /// Gets the current application's copyright.
        /// </summary>
        public virtual string Copyright
        {
            get
            {
                if (_copyright == null)
                {
                    _copyright = GetAssemblyProperty<AssemblyCopyrightAttribute>(CopyrightPropertyName);
                }

                return _copyright;
            }
        }

        /// <summary>
        /// Gets the current application's copyright display text.
        /// </summary>
        public virtual string CopyrightDisplayText
        {
            get
            {
                return String.Format("{0}. All rights reserved.", Copyright);
            }
        }

        /// <summary>
        /// Gets the current application's trademark.
        /// </summary>
        public virtual string Trademark
        {
            get
            {
                if (_trademark == null)
                {
                    _trademark = GetAssemblyProperty<AssemblyTrademarkAttribute>(TrademarkPropertyName);
                }

                return _trademark;
            }
        }

        /// <summary>
        /// Gets the current application's version.
        /// </summary>
        public virtual Version Version
        {
            get
            {
                if (_version == null)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    _version = (entryAssembly == null) ? null : entryAssembly.GetName().Version;
                }

                return _version;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a new instance of the <see cref="TAlex.Common.Environment.ApplicationInfo"/> class.
        /// </summary>
        protected ApplicationInfo()
        {
        }

        #endregion

        #region Methods

        private static string GetAssemblyProperty<T>(string propertyName)
        {
            string result = String.Empty;

            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null) return null;

            object[] attributes = entryAssembly.GetCustomAttributes(typeof(T), false);

            if (attributes.Length > 0)
            {
                T attribute = (T)attributes[0];
                PropertyInfo property = attribute.GetType().GetProperty(propertyName);

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
