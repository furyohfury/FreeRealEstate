using System;
using System.Collections.Generic;

namespace Utils
{
    public static class EnumUtils<T> where T : struct, Enum
    {
        public static readonly T[] Values = CalculateValues();
        private static readonly Dictionary<T, string> _cachedStrings = new();

        public static string ToString(T t)
        {
            if (!_cachedStrings.TryGetValue(t, out string exist))
            {
                exist = t.ToString();
                _cachedStrings.Add(t, exist);
            }

            return exist;
        }

        private static T[] CalculateValues()
        {
            var type = typeof(T);
            return (T[])Enum.GetValues(type);
        }
    }
}
