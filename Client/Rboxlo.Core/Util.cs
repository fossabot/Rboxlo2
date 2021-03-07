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
        /// Formats bytes to human readable form
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <param name="decimals">Max amount of decimals to display</param>
        /// <param name="bi">Bisexual form</param>
        public static string FormatBytes(int bytes, int decimals = 2, bool bi = true)
        {
            if (bytes == 0)
            {
                return "";
            }

            int k = bi ? 1024 : 1000;
            int dm = decimals < 0 ? 0 : decimals;
            string[] sizes = bi ? new string[] { "Bytes", "KiB", "MiB", "GiB", "TiB", "PiB" } : new string[] { "Bytes", "KB", "MB", "GB", "TB", "PB" };

            int idx = Convert.ToInt32(Math.Floor(Math.Log(bytes) / Math.Log(k)));
            return String.Format("{0} {1}", ((bytes / Math.Pow(k, idx)).ToString(String.Format("N{0}", dm))), sizes[idx]);
        }
    }
}
