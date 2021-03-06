using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Rboxlo.Core
{
    /// <summary>
    /// General purpose methods
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Converts a string to titlecase
        /// </summary>
        /// <param name="input">String to convert</param>
        /// <returns>Titlecased string</returns>
        public static string ToTitleCase(string input)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(input.ToLower());
        }
    }
}
