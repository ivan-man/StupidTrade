using System;

namespace Common.Extensions
{
    public static class EnumExtension
    {
        public static string ToLower<T>(this T enumItem) where T : struct, IConvertible
        {
            return enumItem.ToString().ToLower();
        }
    }
}
