using System;
using System.Collections.Generic;

namespace Game.Extensions
{
    public static class Extensions
    {
        private static readonly Random _random = new Random();

        public static T GetRandom<T>(this ICollection<T> items)
        {
            int count = items.Count;
            var randomElement = _random.Next(0, count);
            int i = 0;

            foreach (T item in items)
            {
                if (i++ == randomElement)
                {
                    return item;
                }
            }

            return default(T);
        }
    }
}
