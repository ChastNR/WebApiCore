using System;
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
    }
}