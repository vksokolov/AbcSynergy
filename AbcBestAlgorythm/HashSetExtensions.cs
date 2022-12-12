using System;
using System.Collections.Generic;

namespace AbcBestAlgorythm
{
    public static class HashSetExtensions
    {
        public static void ForEach<T>(this HashSet<T> hashSet, Action<T> action)
        {
            foreach (var item in hashSet)
            {
                action(item);
            }
        }
    }
}