using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace ByteCarrot.Masslog.Core.Infrastructure.Configuration
{
    public abstract class ConfigurationBase
    {
        private readonly NameValueCollection _collection;

        protected string SectionName { get; private set; }

        protected ConfigurationBase(string sectionName)
        {
            _collection = (NameValueCollection)ConfigurationManager.GetSection(sectionName);
            if (_collection == null)
            {
                throw CreateSectionDoesNotExistException(sectionName);
            }
            SectionName = sectionName;
        }

        private static ConfigurationErrorsException CreateSectionDoesNotExistException(string sectionName)
        {
            return new ConfigurationErrorsException("Configuration section {0} does not exist.".Args(sectionName));
        }

        private string GetSetting(string key)
        {
            var value = _collection[key];
            return value.Empty() ? null : value;
        }

        protected string GetStringSetting(string key)
        {
            var value = GetSetting(key);
            if (value == null)
            {
                throw CreateException(key, SectionName);
            }
            return value;
        }

        protected int GetInt32Setting(string key)
        {
            return Int32.Parse(GetStringSetting(key));
        }

        protected bool GetBooleanSetting(string key)
        {
            return Boolean.Parse(GetStringSetting(key));
        }

        protected Guid GetGuidSetting(string key)
        {
            var value = GetSetting(key);
            return new Guid(value);
        }

        protected List<string> GetSemicolonSetting(string key)
        {
            var result = new List<string>();

            var setting = GetStringSetting(key).Trim();
            if (setting.Empty())
            {
                return result;
            }

            result.AddRange(setting.Split(';').Select(item => item.Trim()));

            return result;
        }

        private static ConfigurationErrorsException CreateException(string key, string sectionName)
        {
            return new ConfigurationErrorsException("Setting '{0}' does not exist in '{1}' configuration section.".Args(key, sectionName));
        }

        protected string GetStringSetting(string key, string @default)
        {
            var value = GetSetting(key);
            if (value == null)
            {
                return @default;
            }
            return value;
        }

        protected int GetInt32Setting(string key, int @default)
        {
            var value = GetSetting(key);
            if (value == null)
            {
                return @default;
            }

            int result;
            if (Int32.TryParse(value, out result))
            {
                return result;
            }
            return @default;
        }
    }
}
