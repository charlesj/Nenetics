using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nenetics.Examples
{
    public static class Extension
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
    }
}
