using System;
using System.Linq;

namespace Common.Generic
{
    public static class EnumAttributeReader
    {
        public static T GetAttribute<T>(Enum enumValue) where T : Attribute
        {
            var memberInfo = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault();

            if (memberInfo == null) return null;
            var attribute = (T) memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            return attribute;
        }
    }
}