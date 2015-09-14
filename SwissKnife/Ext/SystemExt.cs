using System;

namespace SwissKnife.Ext
{
    public static class SystemExt
    {
        /// <summary>
        /// Check if passed object is null and throw <see cref="ArgumentNullException"/> if true.
        /// </summary>
        /// <param name="o">Object to check</param>
        /// <param name="paramName">Optional parameter name</param>
        /// <returns>False if not null; otherwise <see cref="ArgumentNullException"/> is thrown</returns>
        public static void ThrowIfNull(this object o, string paramName = "")
        {
            if (o == null)
            {
                if (paramName.IsNullOrEmpty())
                {
                    paramName = "o";
                }

                throw new ArgumentNullException(paramName, "Argument is null");
            }
        }
    }
}
