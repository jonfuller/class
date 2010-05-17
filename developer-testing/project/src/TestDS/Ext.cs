using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;

namespace TestDS
{
    public static class Ext
    {
        public static string Pluralize(this string singular, int count)
        {
            if (count == 1)
                return singular;
            return PluralizationService.CreateService(CultureInfo.CurrentCulture).Pluralize(singular);
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> target, T itemToAppend)
        {
            foreach(var item in target)
                yield return item;
            yield return itemToAppend;
        }

        public static IEnumerable<T> AppendIf<T>(this IEnumerable<T> target, Func<IEnumerable<T>> toAppend, Func<bool> predicate)
        {
            foreach (var item in target)
                yield return item;
            if (predicate())
                foreach (var item in toAppend())
                    yield return item;
        }

        public static IEnumerable<T> Eval<T>(this IEnumerable<T> target)
        {
            return target.ToList();
        }

        public static bool None<T>(this IEnumerable<T> target, Func<T, bool> predicate)
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

        public static string Join(this IEnumerable<string> target, string separator)
        {
            return string.Join(separator, target);
        }
    }
}