using System;
using System.Configuration;
using System.Globalization;


namespace TAlex.Common.Configuration
{
    /// <summary>
    /// Represents a simple helper methods for managing configurations
    /// in App.config or Web.config in appSettings section.
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// Returns the value associated with the specified configuration key for current application.
        /// </summary>
        /// <param name="key">The <see cref="System.String"/> key of the entry to locate. The key can be null.</param>
        /// <returns>
        /// A <see cref="System.String"/> that contains a value associated with the specified
        /// configuration key for current application, if found; otherwise, null.
        /// </returns>
        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Returns the value associated with the specified configuration key for current application.
        /// </summary>
        /// <typeparam name="T">The type of conversion configurations.</typeparam>
        /// <param name="key">The <see cref="System.String"/> key of the entry to locate. The key can be null.</param>
        /// <returns>
        /// A <typeparamref name="T"/> that contains a value associated with the specified
        /// configuration key for current application, if found; otherwise, null.
        /// </returns>
        public static T Get<T>(string key)
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Retrieves the value associated with the specified configuration key.
        /// A return value indicates whether the retrieving succeeded or failed.
        /// </summary>
        /// <typeparam name="T">The type of conversion configurations.</typeparam>
        /// <param name="key">The <see cref="System.String"/> key of the entry to locate.</param>
        /// <param name="result">The configuration value.</param>
        /// <returns>true if configuration value retrieved successfully; otherwise, false.</returns>
        public static bool TryGet<T>(string key, out T result)
        {
            try
            {
                result = Get<T>(key);
                return true;
            }
            catch (Exception)
            {
                result = default(T);
                return false;
            }
        }
    }
}
