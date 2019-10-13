using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Tools.EnumTool
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value) => value
            .GetType()
            .GetMember(value.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DescriptionAttribute>()
            ?.Description;

        public static string GetDefaultValue(this Enum value) => (string) value
            .GetType()
            .GetMember(value.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DefaultValueAttribute>()
            ?.Value;

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