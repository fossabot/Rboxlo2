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

        /// <summary>
        /// Makes a string machine-readable, by removing all special symbols
        /// https://stackoverflow.com/a/1120407
        /// </summary>
        /// <param name="input">String to convert</param>
        /// <returns>Machine readable string</returns>
        public static string ToMachineReadable(string input)
        {
            char[] buffer = new char[input.Length];
            int idx = 0;

            foreach (char c in input)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z')
                    || (c >= 'a' && c <= 'z') || (c == '.') || (c == '_'))
                {
                    buffer[idx] = c;
                    idx++;
                }
            }

            return new string(buffer, 0, idx);
        }
    }
}
