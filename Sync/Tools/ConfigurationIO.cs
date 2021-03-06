﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Sync.Tools
{
    /// <summary>
    /// INI File reader
    /// </summary>
    static class ConfigurationIO
    {
        /// <summary>
        /// InI key
        /// </summary>
        public enum DefaultConfig
        {
            Client,
            Source,
            Language,
            LoggerFile,
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        /// <summary>
        /// Config path
        /// </summary>
        public readonly static string ConfigFile = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
        /// <summary>
        /// Read value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="column">Section</param>
        /// <returns>Value</returns>
        internal static string IniReadValue(string FilePath, string key, string column = "config")
        {
            StringBuilder temp = new StringBuilder(2048);
            GetPrivateProfileString(column, key, "", temp, 2048, FilePath);
            return temp.ToString();
        }

        internal static bool IniWriteValue(string FilePath, string key, string value, string column = "config")
        {
            return WritePrivateProfileString(column, key, value, FilePath);
        }
        /// <summary>
        /// Read value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="column">Section</param>
        /// <returns>Value</returns>
        public static string Read(string key, string column = "config")
        {
            return IniReadValue(ConfigFile, key, column);
        }
        /// <summary>
        /// Read via enum
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static string ReadConfig(DefaultConfig key)
        {
            return IniReadValue(ConfigFile, Enum.GetName(typeof(DefaultConfig), key));
        }
        /// <summary>
        /// Write <see cref="value"/> to <see cref="key"/>
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="column">Section</param>
        /// <returns></returns>
        public static bool Write(string key, string value, string column = "config")
        {
            return IniWriteValue(ConfigFile, key, value, column);
        }
        /// <summary>
        /// Write value via enum
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        public static bool WriteConfig(DefaultConfig key, string value)
        {
            return Write(Enum.GetName(typeof(DefaultConfig), key), value);
        }
    }
}
