using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using System.Reflection;
using System.Security.Permissions;
using System.Globalization;
using TAlex.Common.Environment;
using TAlex.Common.Extensions;


namespace TAlex.Common.Configuration
{
    /// <summary>
    /// Provides persistence for application settings classes in xml file.
    /// </summary>
    [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    public class XmlSettingsProvider : SettingsProvider, IApplicationSettingsProvider
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        /// <example>
        /// Example usage:
        /// <code>
        /// <?xml version="1.0"?>
        /// <configuration>
        ///     <configSections>
        ///         <section name="xmlSettingsProvider" type="TAlex.Common.Configuration.XmlSettingsProviderSection, TAlex.Common" />
        ///     </configSections>
        ///     <xmlSettingsProvider IsPortableSettings="True" />
        /// </configuration>
        /// </code>
        /// </example>
        public const string ProviderSectionName = "xmlSettingsProvider";

        private const string SettingsRootNodeName = "configuration";
        private const string UserSettingsNodeName = "userSettings";

        private const string SettingNodeName = "setting";
        private const string SettingNameAttrName = "name";
        private const string SettingSerializeAsAttrName = "serializeAs";
        private const string SettingValueNodeName = "value";

        private string _appSettingsPath = null;
        private string _appSettingsFilename = null;

        private XmlDocument _settingsXml = null;
        private XmlEscaper _escaper;
        private ISettingsLocationInfoProvider _settingsLocationInfo = new AppDataSettingsLocationInfoProvider();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the friendly name used to refer to the provider during configuration.
        /// </summary>
        public override string Name
        {
            get
            {
                return "XmlSettingsProvider";
            }
        }

        /// <summary>
        /// Gets or sets the name of the currently running application.
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Name;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets the application settings path.
        /// </summary>
        public virtual string AppSettingsPath
        {
            get
            {
                if (_appSettingsPath == null)
                {
                    _appSettingsPath = _settingsLocationInfo.GetAppSettingsPath();
                }

                return _appSettingsPath;
            }
        }

        /// <summary>
        /// Gets the name of the application settings file.
        /// </summary>
        public virtual string AppSettingsFilename
        {
            get
            {
                if (_appSettingsFilename == null)
                {
                    _appSettingsFilename = _settingsLocationInfo.GetAppSettingsFilename();
                }

                return _appSettingsFilename;
            }
        }


        private XmlDocument SettingsXml
        {
            get
            {
                if (_settingsXml == null)
                {
                    _settingsXml = new XmlDocument();

                    try
                    {
                        _settingsXml.Load(Path.Combine(AppSettingsPath, AppSettingsFilename));
                    }
                    catch (Exception)
                    {
                        //Create new document
                        XmlDeclaration dec = _settingsXml.CreateXmlDeclaration("1.0", "utf-8", String.Empty);
                        _settingsXml.AppendChild(dec);

                        XmlNode nodeRoot = _settingsXml.CreateNode(XmlNodeType.Element, SettingsRootNodeName, String.Empty);
                        _settingsXml.AppendChild(nodeRoot);
                    }
                }

                return _settingsXml;
            }
        }

        private XmlEscaper Escaper
        {
            get
            {
                if (_escaper == null)
                {
                    _escaper = new XmlEscaper();
                }

                return _escaper;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TAlex.Common.Configuration.XmlSettingsProvider"/> class.
        /// </summary>
        public XmlSettingsProvider()
        {
            try
            {
                XmlSettingsProviderSection confSection =
                    ConfigurationManager.GetSection(ProviderSectionName) as XmlSettingsProviderSection;

                if (confSection.IsPortableSettings)
                    _settingsLocationInfo = new PortableSettingsLocationInfoProvider();
                else
                    _settingsLocationInfo = new AppDataSettingsLocationInfoProvider();
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="values">
        /// A collection of the name/value pairs representing the provider-specific attributes
        /// specified in the configuration for this provider.
        /// </param>
        public override void Initialize(string name, NameValueCollection values)
        {
            if (String.IsNullOrEmpty(name))
            {
                name = Name;
            }
            base.Initialize(name, values);
        }

        /// <summary>
        /// Returns the collection of settings property values for the specified application
        /// instance and settings property group.
        /// </summary>
        /// <param name="context">A <see cref="System.Configuration.SettingsContext"/> describing the current application use.</param>
        /// <param name="collection">
        /// A <see cref="System.Configuration.SettingsPropertyCollection"/> containing the settings
        /// property group whose values are to be retrieved.</param>
        /// <returns>
        /// A <see cref="System.Configuration.SettingsPropertyValueCollection"/> containing the values
        /// for the specified settings property group.
        /// </returns>
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            string sectionName = GetSectionName(context);
            return ReadSettingsFromXml(SettingsXml, sectionName, collection);
        }

        /// <summary>
        /// Sets the values of the specified group of property settings.
        /// </summary>
        /// <param name="context">A <see cref="System.Configuration.SettingsContext"/> describing the current application usage.</param>
        /// <param name="collection">
        /// A <see cref="System.Configuration.SettingsPropertyValueCollection"/> representing the group
        /// of property settings to set.
        /// </param>
        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            string sectionName = GetSectionName(context);

            if (String.IsNullOrEmpty(sectionName))
                throw new ConfigurationErrorsException("Failed to save settings: unable to access the configuration section.");

            // Iterate through the settings to be stored
            // Only dirty settings are included in propvals, and only ones relevant to this provider
            foreach (SettingsPropertyValue propValue in collection)
            {
                SetValue(propValue, sectionName);
            }

            try
            {
                if (!Directory.Exists(AppSettingsPath))
                {
                    Directory.CreateDirectory(AppSettingsPath);
                }

                SettingsXml.Save(Path.Combine(AppSettingsPath, AppSettingsFilename));
            }
            catch (Exception) // Ignore if cant save, device been ejected
            {
            }
        }


        private object GetValue(XmlDocument doc, SettingsProperty setting, string sectionName)
        {
            XmlNode settingNode = null;

            try
            {
                string settingSelector = XPathSettingSelector(setting.Name);
                
                if (IsRoamingSetting(setting))
                    settingNode = doc.SelectSingleNode(XPathCombine(SettingsRootNodeName, sectionName, settingSelector));
                else
                    settingNode = doc.SelectSingleNode(XPathCombine(SettingsRootNodeName, UserSettingsNodeName, sectionName, settingSelector));
            }
            catch (Exception)
            {
            }

            if (settingNode != null)
            {
                XmlNode settingValueNode = settingNode[SettingValueNodeName];
                if (settingValueNode != null)
                {
                    string innerXml = settingValueNode.InnerXml;
                    if (setting.SerializeAs == SettingsSerializeAs.String)
                    {
                        innerXml = Escaper.Unescape(innerXml);
                    }
                    return innerXml;
                }
                else
                {
                    return setting.DefaultValue;
                }
            }
            else
            {
                return setting.DefaultValue;
            }
        }

        private void SetValue(SettingsPropertyValue settingValue, string sectionName)
        {
            XmlNode settingsRootNode = SettingsXml.SelectSingleNode(SettingsRootNodeName);
            XmlNode userSettingsNode = null;
            XmlNode sectionNode = null;
            XmlNode settingNode = null;


            // Determine if the setting is roaming.
            // If roaming then the value is stored as an element under the root
            // Otherwise it is stored under a machine name node 
            try
            {
                string settingSelector = XPathSettingSelector(settingValue.Name);

                if (IsRoamingSetting(settingValue.Property))
                    settingNode = settingsRootNode.SelectSingleNode(XPathCombine(sectionName, settingSelector));
                else
                    settingNode = settingsRootNode.SelectSingleNode(XPathCombine(UserSettingsNodeName, sectionName, settingSelector));
            }
            catch (Exception)
            {
                settingNode = null;
            }


            if (settingNode == null)
            {
                settingNode = CreateSettingNode(settingValue);

                if (IsRoamingSetting(settingValue.Property))
                {
                    // Store the value as an element of the Settings Root Node
                    sectionNode = SettingsXml.CreateElement(sectionName);
                    sectionNode.AppendChild(settingNode);
                    settingsRootNode.AppendChild(sectionNode);
                }
                else
                {
                    userSettingsNode = settingsRootNode.SelectSingleNode(UserSettingsNodeName);
                    if (userSettingsNode == null)
                    {
                        userSettingsNode = SettingsXml.CreateElement(UserSettingsNodeName);
                        settingsRootNode.AppendChild(userSettingsNode);
                    }
                    
                    sectionNode = userSettingsNode.SelectSingleNode(sectionName);
                    if (sectionNode == null)
                    {
                        sectionNode = SettingsXml.CreateElement(sectionName);
                        userSettingsNode.AppendChild(sectionNode);
                    }

                    sectionNode.AppendChild(settingNode);
                }
            }

            settingNode.InnerXml = SerializeToXmlElement(settingValue).OuterXml;
        }


        private static bool IsRoamingSetting(SettingsProperty setting)
        {
            // Determine if the setting is marked as Roaming
            foreach (DictionaryEntry d in setting.Attributes)
            {
                if (d.Value is SettingsManageabilityAttribute)
                {
                    return true;
                }
            }
            return false;
        }


        #region Helpers

        private static string XPathCombine(params string[] nodeNames)
        {
            return String.Join("/", nodeNames);
        }

        private static string GetSectionName(SettingsContext context)
        {
            string groupName = (string)context["GroupName"];
            string settingsKey = (string)context["SettingsKey"];
            string name = groupName;

            if (!String.IsNullOrEmpty(settingsKey))
            {
                name = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", name, settingsKey);
            }

            return XmlConvert.EncodeLocalName(name);

        }

        private SettingsPropertyValueCollection GetSettingValuesFromFile(
            string configFileName, string sectionName,
            bool userScoped, SettingsPropertyCollection properties)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);

            return ReadSettingsFromXml(doc, sectionName, properties, true);
        }

        private SettingsPropertyValueCollection ReadSettingsFromXml(
            XmlDocument doc, string sectionName,
            SettingsPropertyCollection properties, bool isDirty = false)
        {
            SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();

            foreach (SettingsProperty property in properties)
            {
                SettingsPropertyValue value = new SettingsPropertyValue(property);
                value.IsDirty = isDirty;
                value.SerializedValue = GetValue(doc, property, sectionName);
                values.Add(value);
            }

            return values;
        }


        private void RevertToParent(string sectionName, bool isRoaming)
        {
            System.Configuration.Configuration userConfig = GetUserConfig(isRoaming);
            //ClientSettingsSection section = this.GetConfigSection(userConfig, sectionName, false);


        }

        private System.Configuration.Configuration GetUserConfig(bool isRoaming)
        {
            ConfigurationUserLevel userLevel = isRoaming ? ConfigurationUserLevel.PerUserRoaming : ConfigurationUserLevel.PerUserRoamingAndLocal;
            return ConfigurationManager.OpenExeConfiguration(userLevel);
        }






        private XmlNode SerializeToXmlElement(SettingsPropertyValue settingValue)
        {
            XmlElement element = SettingsXml.CreateElement(SettingValueNodeName);
            string serializedValue = settingValue.SerializedValue as String;

            if ((serializedValue == null) && (settingValue.Property.SerializeAs == SettingsSerializeAs.Binary))
            {
                byte[] inArray = settingValue.SerializedValue as byte[];
                if (inArray != null)
                {
                    serializedValue = Convert.ToBase64String(inArray);
                }
            }

            if (serializedValue == null)
            {
                serializedValue = String.Empty;
            }
            
            if (settingValue.Property.SerializeAs == SettingsSerializeAs.String)
            {
                serializedValue = Escaper.Escape(serializedValue);
            }

            element.InnerXml = serializedValue;
            XmlNode oldChild = null;

            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.XmlDeclaration)
                {
                    oldChild = node;
                    break;
                }
            }

            if (oldChild != null)
            {
                element.RemoveChild(oldChild);
            }

            return element;
        }

        private XmlNode CreateSettingNode(SettingsPropertyValue settingValue)
        {
            XmlNode settingNode = SettingsXml.CreateElement(SettingNodeName);

            XmlAttribute nameAttr = SettingsXml.CreateAttribute(SettingNameAttrName);
            nameAttr.InnerText = settingValue.Name;
            settingNode.Attributes.Append(nameAttr);

            XmlAttribute serializeAsAttr = SettingsXml.CreateAttribute(SettingSerializeAsAttrName);
            serializeAsAttr.InnerText = settingValue.Property.SerializeAs.ToString();
            settingNode.Attributes.Append(serializeAsAttr);

            return settingNode;
        }

        private string XPathSettingSelector(string name)
        {
            return String.Format("{0}[@{1}=\"{2}\"]", SettingNodeName, SettingNameAttrName, name);
        }

        #endregion


        #region IApplicationSettingsProvider Members

        /// <summary>
        /// Returns the value of the specified settings property for the previous version
        /// of the same application.
        /// </summary>
        /// <param name="context">
        /// A <see cref="System.Configuration.SettingsContext"/> describing the current application usage.
        /// </param>
        /// <param name="property">
        /// The <see cref="System.Configuration.SettingsProperty"/> whose value is to be returned.
        /// </param>
        /// <returns>
        /// A <see cref="System.Configuration.SettingsPropertyValue"/> containing the value of the
        /// specified property setting as it was last set in the previous version of
        /// the application; or null if the setting cannot be found.
        /// </returns>
        [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        [FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery | FileIOPermissionAccess.Read)]
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property)
        {
            bool isRoaming = IsRoamingSetting(property);
            string previousConfigFileName = _settingsLocationInfo.GetPreviousConfigFullName(isRoaming);
            if (!String.IsNullOrEmpty(previousConfigFileName))
            {
                SettingsPropertyCollection properties = new SettingsPropertyCollection();
                properties.Add(property);
                return GetSettingValuesFromFile(previousConfigFileName, GetSectionName(context), true, properties)[property.Name];
            }

            return new SettingsPropertyValue(property) { PropertyValue = null };
        }

        /// <summary>
        /// Resets the application settings associated with the specified application
        /// to their default values.
        /// </summary>
        /// <param name="context">A <see cref="System.Configuration.SettingsContext"/> describing the current application usage.</param>
        public void Reset(SettingsContext context)
        {
            string sectionName = GetSectionName(context);

            RevertToParent(sectionName, true);
            RevertToParent(sectionName, false);
        }

        /// <summary>
        /// Indicates to the provider that the application has been upgraded. This offers
        /// the provider an opportunity to upgrade its stored settings as appropriate.
        /// </summary>
        /// <param name="context">
        /// A <see cref="System.Configuration.SettingsContext"/> describing the current application usage.
        /// </param>
        /// <param name="properties">
        /// A <see cref="System.Configuration.SettingsPropertyCollection"/> containing the settings
        /// property group whose values are to be retrieved.
        /// </param>
        public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
        {
            SettingsPropertyCollection roamingPropertys = new SettingsPropertyCollection();
            SettingsPropertyCollection nonRoamingPropertys = new SettingsPropertyCollection();

            foreach (SettingsProperty property in properties)
            {
                if (IsRoamingSetting(property))
                    roamingPropertys.Add(property);
                else
                    nonRoamingPropertys.Add(property);
            }

            if (roamingPropertys.Count > 0)
            {
                Upgrade(context, roamingPropertys, true);
            }
            if (nonRoamingPropertys.Count > 0)
            {
                Upgrade(context, nonRoamingPropertys, false);
            }
        }

        [FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery | FileIOPermissionAccess.Read)]
        private void Upgrade(SettingsContext context, SettingsPropertyCollection properties, bool isRoaming)
        {
            string previousConfigFileName = _settingsLocationInfo.GetPreviousConfigFullName(isRoaming);
            if (!String.IsNullOrEmpty(previousConfigFileName))
            {
                SettingsPropertyCollection propertys = new SettingsPropertyCollection();
                foreach (SettingsProperty property in properties)
                {
                    if (!(property.Attributes[typeof(NoSettingsVersionUpgradeAttribute)] is NoSettingsVersionUpgradeAttribute))
                    {
                        propertys.Add(property);
                    }
                }
                SettingsPropertyValueCollection collection = GetSettingValuesFromFile(previousConfigFileName, GetSectionName(context), true, propertys);
                SetPropertyValues(context, collection);
            }
        }

        #endregion

        #endregion

        #region Nested Types

        private class XmlEscaper
        {
            private XmlDocument _doc;
            private XmlElement _tempElement;

            public XmlEscaper()
            {
                _doc = new XmlDocument();
                _tempElement = _doc.CreateElement("temp");
            }

            internal string Escape(string xmlString)
            {
                if (String.IsNullOrEmpty(xmlString))
                {
                    return xmlString;
                }

                _tempElement.InnerText = xmlString;
                return _tempElement.InnerXml;
            }

            internal string Unescape(string escapedString)
            {
                if (String.IsNullOrEmpty(escapedString))
                {
                    return escapedString;
                }

                _tempElement.InnerXml = escapedString;
                return _tempElement.InnerText;
            }
        }


        private interface ISettingsLocationInfoProvider
        {
            string GetAppSettingsFilename();

            string GetPreviousConfigFullName(bool isRoaming);

            string GetAppSettingsPath();
        }

        private class AppDataSettingsLocationInfoProvider : ISettingsLocationInfoProvider
        {
            private const string DefaultAppSettingsFilename = "user.config";

            private string _prevLocalConfigFileName;
            private string _prevRoamingConfigFileName;


            #region ISettingsStorageInfoProvider Members

            public string GetAppSettingsFilename()
            {
                return DefaultAppSettingsFilename;
            }

            public string GetPreviousConfigFullName(bool isRoaming)
            {
                string str = isRoaming ? _prevRoamingConfigFileName : _prevLocalConfigFileName;

                if (String.IsNullOrEmpty(str))
                {
                    Version currentVersion = Assembly.GetEntryAssembly().GetVersion();

                    if (currentVersion == null)
                    {
                        return null;
                    }

                    string path = GetAppSettingsPath();
                    DirectoryInfo parent = Directory.GetParent(path);
                    if (parent.Exists)
                    {
                        DirectoryInfo tempDirInfo = null;
                        Version tempVersion = null;

                        foreach (DirectoryInfo dir in parent.GetDirectories())
                        {
                            Version ver = CreateVersion(dir.Name);
                            if (ver != null && ver < currentVersion)
                            {
                                if (tempVersion == null)
                                {
                                    tempVersion = ver;
                                    tempDirInfo = dir;
                                }
                                else if (ver > tempVersion)
                                {
                                    tempVersion = ver;
                                    tempDirInfo = dir;
                                }
                            }
                        }

                        string tempStr = null;
                        if (tempDirInfo != null)
                        {
                            tempStr = Path.Combine(tempDirInfo.FullName, GetAppSettingsFilename());
                        }

                        if (File.Exists(tempStr))
                        {
                            str = tempStr;
                        }
                    }

                    if (isRoaming)
                        _prevRoamingConfigFileName = str;
                    else
                        _prevLocalConfigFileName = str;
                }

                return str;
            }

            public string GetAppSettingsPath()
            {
                var assemblyInfo = Assembly.GetEntryAssembly().GetAssemblyInfo();

                string fullPath = Path.GetFullPath(System.Environment.GetCommandLineArgs()[0]);
                string fileName = Path.GetFileName(fullPath);

                string manufacturer = assemblyInfo.Company;
                if (!String.IsNullOrEmpty(manufacturer)) manufacturer = manufacturer.Replace(' ', '_');

                string urlHash = ComputeHash(fullPath);
                urlHash = urlHash.Replace(Path.DirectorySeparatorChar, '0');
                urlHash = urlHash.Replace(Path.AltDirectorySeparatorChar, '1');

                return Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
                    !String.IsNullOrEmpty(manufacturer) ? manufacturer : Assembly.GetEntryAssembly().GetName().Name,
                    String.Format("{0}_Url_{1}", fileName, urlHash),
                    assemblyInfo.Version.ToString());
            }

            #endregion

            private static string ComputeHash(string s)
            {
                return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.Unicode.GetBytes(s)));
            }

            private Version CreateVersion(string name)
            {
                try
                {
                    return new Version(name);
                }
                catch (ArgumentException)
                {
                    return null;
                }
                catch (OverflowException)
                {
                    return null;
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        private class PortableSettingsLocationInfoProvider : ISettingsLocationInfoProvider
        {
            #region ISettingsStorageInfoProvider Members

            public string GetAppSettingsFilename()
            {
                string fullPath = Path.GetFullPath(System.Environment.GetCommandLineArgs()[0]);
                return Path.GetFileName(fullPath) + ".settings";
            }

            public string GetPreviousConfigFullName(bool isRoaming)
            {
                return null;
            }

            public string GetAppSettingsPath()
            {
                string fullPath = Path.GetFullPath(System.Environment.GetCommandLineArgs()[0]);
                return Path.GetDirectoryName(fullPath);
            }

            #endregion
        }

        #endregion
    }
}
