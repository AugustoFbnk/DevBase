using System;
using System.Linq;
using System.Runtime.Serialization;

namespace DevBase.Util.ExtensionMethods
{
    public static class EnumExtensions
    {
        public static char ToEnumChar<T>(this T type)
        {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return Convert.ToChar(enumMemberAttribute.Value);
        }

        public static T ToCharEnum<T>(this char? value)
        {
            var enumType = typeof(T);
            foreach (var name in Enum.GetNames(enumType))
            {
                var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
                if (Convert.ToChar(enumMemberAttribute.Value) == value) return (T)Enum.Parse(enumType, name);
            }
            return default(T);
        }
    }
}
