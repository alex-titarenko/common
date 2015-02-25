using System;
using System.Reflection;
using TAlex.Common.Extensions;


namespace TAlex.Common.Models
{
    /// <summary>
    /// Provides the information about currently executing application,
    /// such as title, description, company, version, etc.
    /// </summary>
    public class AssemblyInfo
    {
        #region Fields

        protected Assembly Assembly;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current application's title.
        /// </summary>
        public virtual string Title
        {
            get
            {
                return Assembly.GetTitle();
            }
        }

        /// <summary>
        /// Gets the current application's description.
        /// </summary>
        public virtual string Description
        {
            get
            {
                return Assembly.GetDescription();
            }
        }

        /// <summary>
        /// Gets the current application's company.
        /// </summary>
        public virtual string Company
        {
            get
            {
                return Assembly.GetCompany();
            }
        }

        /// <summary>
        /// Gets the current application's product.
        /// </summary>
        public virtual string Product
        {
            get
            {
                return Assembly.GetProduct();
            }
        }

        /// <summary>
        /// Gets the current application's copyright.
        /// </summary>
        public virtual string Copyright
        {
            get
            {
                return Assembly.GetCopyright();
            }
        }

        /// <summary>
        /// Gets the current application's trademark.
        /// </summary>
        public virtual string Trademark
        {
            get
            {
                return Assembly.GetTrademark();
            }
        }

        /// <summary>
        /// Gets the current application's version.
        /// </summary>
        public virtual Version Version
        {
            get
            {
                return Assembly.GetVersion();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a new instance of the <see cref="TAlex.Common.Environment.ApplicationInfo"/> class.
        /// </summary>
        /// <param name="assembly">A target assembly.</param>
        public AssemblyInfo(Assembly assembly)
        {
            Assembly = assembly;
        }

        #endregion
    }
}
