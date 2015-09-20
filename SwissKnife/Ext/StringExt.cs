using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace SwissKnife.Ext
{
    public static class StringExt
    {
        #region Formatting

        /// <summary>
        /// Format a <see cref="System.String"/> using <see cref="string.Format(string, object[])"/> but with a fall back, if the arguments don't fit the format it won't throw an Exception
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.</returns>
        public static string FormatSafe(this string format, params object[] args)
        {
            string formattedText;

            try
            {
                formattedText = string.Format(format, args);
            }
            catch
            {
                formattedText = string.Format("{0} ({1})", format, args.CombineWith(", "));
            }

            return formattedText;
        }

        #endregion

        #region Casing

        /// <summary>
        /// Converts the specified string to title case (e.g. Beer and wine > Beer And Wine)
        /// </summary>
        /// <param name="format">Text to format</param>
        /// <param name="uppercaseAsAcronyms">Consider words that are entirely in uppercase to be acronyms</param>
        /// <returns></returns>
        public static string ToTitleCase(this string format, bool trimLeadingSpaces = true, bool uppercaseAsAcronyms = true)
        {
            if (trimLeadingSpaces)
                format = format.TrimStart(' ');

            if (!uppercaseAsAcronyms)
                format = format.ToLower();

            return System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(format);
        }

        /// <summary>
        /// Converts the specified string to Pascal Case (e.g. Beer and wine > BeerAndWine)
        /// </summary>
        /// <param name="format">Text to format</param>
        /// <param name="uppercaseAsAcronyms">Consider words that are entirely in uppercase to be acronyms</param>
        /// <returns></returns>
        public static string ToPascalCase(this string format, bool trimLeadingSpaces = true, bool uppercaseAsAcronyms = true)
        {
            int amountOfSpacesToAdd = 0;

            // TODO Find / discuss a better implementation, seems a bit verbose.
            if (!trimLeadingSpaces)
            {
                for (int i = 0; i < format.Length; i++)
                {
                    if (format[i] == ' ')
                        amountOfSpacesToAdd++;
                    else
                        break;
                }
            }

            if (amountOfSpacesToAdd > 0)
            {
                return string.Empty.PadLeft(amountOfSpacesToAdd, ' ') + format.ToTitleCase(trimLeadingSpaces: trimLeadingSpaces, uppercaseAsAcronyms: uppercaseAsAcronyms).Replace(" ", string.Empty);
            }
            else
            {
                return format.ToTitleCase(trimLeadingSpaces: trimLeadingSpaces, uppercaseAsAcronyms: uppercaseAsAcronyms).Replace(" ", string.Empty);
            }
        }

        /// <summary>
        /// Converts the specified string to Camel Case (e.g. Beer and wine > beerAndWine)        
        /// </summary>
        /// <param name="format">Text to format</param>
        /// <param name="uppercaseAsAcronyms">Consider words that are entirely in uppercase to be acronyms</param>
        /// <returns></returns>
        public static string ToCamelCase(this string format, bool trimLeadingSpaces = true, bool uppercaseAsAcronyms = true)
        {
            string pascalCase = format.ToPascalCase(trimLeadingSpaces: trimLeadingSpaces, uppercaseAsAcronyms: uppercaseAsAcronyms);

            for (int i = 0; i < format.Length; i++)
            {
                if (pascalCase[i] != ' ')
                {
                    return string.Empty.PadLeft(i, ' ') + char.ToLowerInvariant(pascalCase[i]) + pascalCase.Substring(i + 1);
                }               
            }

            // Empty string
            return format;
        }

        #endregion

        #region Misc

        /// <summary>
        /// Indicates whether the specified string is null or an Empty string.
        /// </summary>
        /// <param name="value">The string to test</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test</param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        #endregion

        #region Combiners

        // TODO At to a IEnumerableExt
        /// <summary>
        /// Use this to combine infinite parts with a specified seperator
        /// </summary>
        /// <param name="seperator">Seperator</param>
        /// <param name="args">Elements to combine</param>
        /// <returns>Combined string with seperator if both a and b have a value</returns>
        public static string CombineWith(this IEnumerable args, string seperator)
        {
            var toReturn = new StringBuilder();

            foreach (object arg in args)
            {
                string value = arg == null ? "null" : arg.ToString();

                if (value.Length > 0)
                {
                    if (toReturn.Length > 0)
                    {
                        toReturn.Append(seperator);
                        toReturn.Append(value);
                    }
                    else
                    {
                        toReturn.Append(value);
                    }
                }
            }

            return toReturn.ToString();
        }

        #endregion

        #region Conversion

        /// <summary>
        /// Convert string to a byte[] based on UTF8
        /// </summary>
        /// <param name="value">String to convert</param>
        /// <returns>Array of bytes of the string based on UTF8</returns>
        public static byte[] ToBytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        #endregion
    }
}
