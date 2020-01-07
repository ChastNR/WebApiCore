using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Tools.EnumTool
{
    public static class EnumHelper
    {
        /// <summary>
        /// Get enum description attribute value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Description attribute value</returns>
        public static string GetDescription(this Enum value)
        {
            return value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description;
        }

        /// <summary>
        /// Get enum default value attribute value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Default value attribute value</returns>
        public static string GetDefaultValue(this Enum value)
        {
            return (string) value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DefaultValueAttribute>()
                ?.Value;
        }

        public static NameValueCollection ToList<T>() where T : struct
        {
            var result = new NameValueCollection();
            if (!typeof(T).IsEnum) return result;
            var enumType = typeof(T);
            var values = Enum.GetValues(enumType);
            foreach (var value in values)
            {
                var memInfo = enumType.GetMember(enumType.GetEnumName(value));
                var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = descriptionAttributes.Length > 0
                    ? ((DescriptionAttribute) descriptionAttributes.First()).Description
                    : value.ToString();
                result.Add(description, value.ToString());
            }

            return result;
        }
    }
}