using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissKnife.Utils
{
    public static class PathUtil
    {
        /// <summary>
        /// Get the path of the application that's running
        /// </summary>
        /// <param name="combineWithPath">Filename or relative path to combine with the Application Path</param>
        /// <returns></returns>
        public static string GetApplicationPath(string combineWithPath = null)
        {
            // TODO Create a ValidPath method that checks for invalid characters and check that before
            // this method is executed

            // Best way to get application folder path: http://stackoverflow.com/a/6041505/2104
            if (combineWithPath != null)
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, combineWithPath);
            else
                return AppDomain.CurrentDomain.BaseDirectory; 
        }
    }
}
