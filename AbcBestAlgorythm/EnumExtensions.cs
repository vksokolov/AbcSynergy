using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AbcBestAlgorythm
{
    public static class EnumExtensions
    {
        private static readonly Dictionary<Type, IList> EnumValues = new Dictionary<Type, IList>();
        public static List<T> GetValues<T>()
        {
            if (!EnumValues.ContainsKey(typeof(T)))
            {
                EnumValues.Add(typeof(T), Enum.GetValues(typeof(T)).Cast<T>().ToList());
            }
            return (List<T>)EnumValues[typeof(T)];
        }
    }
}