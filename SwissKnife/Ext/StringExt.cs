using System.Collections;
using System.Text;

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

        #region Combiners

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
    }
}
