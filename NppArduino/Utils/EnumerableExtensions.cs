using System;
using System.Collections.Generic;


namespace NppArduino.Utils
{
    static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (T value in values)
                action(value);
        }
    }
}
