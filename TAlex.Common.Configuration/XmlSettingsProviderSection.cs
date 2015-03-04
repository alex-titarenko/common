using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace TAlex.Common.Configuration
{
    /// <summary>
    /// Represents the <see cref="TAlex.Common.Configuration.XmlSettingsProvider"/> section within a configuration file.
    /// </summary>
    public class XmlSettingsProviderSection : ConfigurationSection
    {
        internal const string IsPortableSettingsPropName = "IsPortableSettings";

        /// <summary>
        /// Initializes a new instance of the <see cref="TAlex.Common.Configuration.XmlSettingsProviderSection"/> class.
        /// </summary>
        public XmlSettingsProviderSection()
        {
        }


        /// <summary>
        /// Gets or sets a value that indicates the need to store settings
        /// in a file that is located close to the executable file.
        /// </summary>
        [ConfigurationProperty(IsPortableSettingsPropName, DefaultValue = false, IsRequired = false)]
        public bool IsPortableSettings
        {
            get
            {
                return (bool)this[IsPortableSettingsPropName];
            }

            set
            {
                this[IsPortableSettingsPropName] = value;
            }
        }
    }
}
