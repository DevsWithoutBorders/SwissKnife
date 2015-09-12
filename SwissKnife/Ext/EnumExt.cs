using System;
using System.Collections.Generic;
using System.Linq;

namespace SwissKnife.Ext
{
    public static class EnumExt
    {
        #region Convert to Enum

        /// <summary>
        /// Retrieve a typed Enum from a String value.
        /// </summary>
        /// <typeparam name="T">The Enum type to parse to.</typeparam>
        /// <param name="value">The String value.</param>
        /// <param name="ignoreCase">If true, ignore case; otherwise, regard case.</param>
        /// <returns>Typed Enum instance for the specified String value.</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase = false)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException("Can't parse an empty string", "value");
            }

            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("T must be an enumerated type.");
            }

            // Convert value to PascalCase
            value = value.ToPascalCase();

            // Warning, can throw ArgumentExceptions
            var result = (T)Enum.Parse(enumType, value, ignoreCase);
            
            if (!Enum.IsDefined(enumType, result))
            {
                throw new ArgumentOutOfRangeException("value", "#2 Value '{0}' is not part of enum type '{1}'".FormatSafe(value, enumType));
            }

            return result;
        }

        /// <summary>
        /// Retrieve a typed Enum from an int value.
        /// </summary>
        /// <typeparam name="T">The Enum type to parse to.</typeparam>
        /// <param name="value">The int value.</param>
        /// <returns>Typed Enum instance for the specified int value.</returns>
        public static T ToEnum<T>(this object value)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("T must be an enumerated type.");
            }

            if (!Enum.IsDefined(enumType, value))
            {
                throw new ArgumentOutOfRangeException("value", "Value '{0}' is not part of enum type '{1}'".FormatSafe(value, enumType));
            }

            // Warning, can throw ArgumentExceptions
            return (T)Enum.Parse(enumType, value.ToString());
        }

        /// <summary>
        /// Converts the integer representation of an enum to its enum equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="value">A integer containing an enum to convert.</param>
        /// <param name="result">When this method returns, contains the enum value equivalent to the integer contained in value, if the conversion succeeded; otherwise contains the default value.</param>
        /// <returns>true if value was converted successfully; otherwise, false.</returns>
        public static bool TryParse<T>(object value, out T result)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            bool isSuccess = false;
            try
            {
                result = ToEnum<T>(value);
                isSuccess = true;
            }
            catch
            {
                result = default(T);
            }

            return isSuccess;
        }

        /// <summary>
        /// Converts the string representation of an enum to its enum equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="value">A string containing an enum to convert.</param>
        /// <param name="result">When this method returns, contains the enum value equivalent to the string contained in value, if the conversion succeeded; otherwise contains the default value.</param>
        /// <param name="ignoreCase">If true, ignore case; otherwise, regard case.</param>
        /// <returns>true if value was converted successfully; otherwise, false.</returns>
        public static bool TryParse<T>(string value, out T result, bool ignoreCase = false)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            bool isSuccess = false;
            try
            {
                result = ToEnum<T>(value, ignoreCase);
                isSuccess = true;
            }
            catch
            {
                result = default(T);
            }

            return isSuccess;
        }

        #endregion

        #region Flags

        /// <summary>
        /// Indicates whether the specified enum value has the flag applied.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="value">The Enum that is checked.</param>
        /// <param name="flag">The flag to check.</param>
        /// <returns>true if the flag is applied; otherwise false.</returns>
        public static bool HasFlag<T>(this Enum value, T flag)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            try
            {
                return (Convert.ToInt64(value) & Convert.ToInt64(flag)) == Convert.ToInt64(flag);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all the individual flags for a type. Only works for Enums with <see cref="FlagsAttribute"/>!
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetIndividualFlags(this Enum value)
        {
            return GetFlags(value, GetFlagValues(value.GetType()).ToArray());
        }

        private static IEnumerable<Enum> GetFlags(Enum value, Enum[] values)
        {
            ulong bits = Convert.ToUInt64(value);
            var results = new List<Enum>();
            for (int i = values.Length - 1; i >= 0; i--)
            {
                ulong mask = Convert.ToUInt64(values[i]);
                if (i == 0 && mask == 0L)
                    break;

                if ((bits & mask) == mask)
                {
                    results.Add(values[i]);
                    bits -= mask;
                }
            }

            if (bits != 0L)
                return Enumerable.Empty<Enum>();

            if (Convert.ToUInt64(value) != 0L)
                return results.Reverse<Enum>();

            if (bits == Convert.ToUInt64(value) && values.Length > 0 && Convert.ToUInt64(values[0]) == 0L)
                return values.Take(1);

            return Enumerable.Empty<Enum>();
        }

        private static IEnumerable<Enum> GetFlagValues(Type enumType)
        {
            ulong flag = 0x1;
            foreach (var value in Enum.GetValues(enumType).Cast<Enum>())
            {
                ulong bits = Convert.ToUInt64(value);
                if (bits == 0L)
                {
                    continue; // skip the zero value
                }

                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits)
                {
                    yield return value;
                }
            }
        }

        #endregion
    }
}