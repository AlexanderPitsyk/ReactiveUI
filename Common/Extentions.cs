using System;
using System.Collections.Generic;

namespace ReactiveUIApplication.Common
{
    public static class Extentions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }

        public static void ForEach<T>(this Array list, Action<T> action)
        {
            foreach (T item in list)
            {
                action(item);
            }
        }

        public static bool IsValid(this string value)
        {
            return !value.IsInvalid();
        }

        public static bool IsInvalid(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static TEnum GetEnumByIndex<TEnum>(int index)
        {
            var etype = typeof (TEnum);
            if (!etype.IsEnum)
            {
                throw new ArgumentException("TEnum must be of an enum type.");
            }

            var enums = Enum.GetValues(typeof (TEnum));
            if (index > enums.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var option = (TEnum) enums.GetValue(index);
            return option;
        }
    }
}