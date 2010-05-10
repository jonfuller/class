using System;
using System.Collections.Generic;

namespace TestDS
{
    public static class Ext
    {
        public static bool None<T>(this IEnumerable<T> target, Predicate<T> predicate)
        {
            foreach (var item in target)
                if (predicate(item))
                    return false;
            return true;
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> target, Action<T> action)
        {
            foreach(var item in target)
                action(item);
            return target;
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}