using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Rboxlo.Core.Common
{
    /// <summary>
    /// Cryptography class providing a variety of general purpose cryptographic methods
    /// </summary>
    public static class Crypto
    {
        /// <summary>
        /// Computes a SHA256 hash of given data
        /// </summary>
        /// <param name="data">Data to hash. If you are computing the hash of a file, set this as the the path to it</param>
        /// <param name="isFile">If the given "data" parameter is the path to a file</param>
        /// <param name="returnLowerCase">Whether to return the hash in all lowercase or not</param>
        /// <returns>Computed SHA256 hash, or FALSE if the file doesn't exist and computing file hashes</returns>
        public static object Sha256(string data, bool isFile = false, bool returnLowerCase = true)
        {
            StringBuilder result = new StringBuilder();

            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes;

                if (isFile)
                {
                    if (!File.Exists(data))
                    {
                        return false;
                    }

                    using (FileStream stream = new FileStream(data, FileMode.Open))
                    {
                        stream.Position = 0;
                        bytes = sha.ComputeHash(stream);
                        stream.Close();
                    }
                }
                else
                {
                    bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(data));
                }

                // convert hash to string
                // TODO: Could we just use BitConverter? Is it expensive? Or is it just a wrapped version of what we are doing here? Check back on this later.
                for (int i = 0; i < bytes.Length; i++)
                {
                    result.Append(bytes[i].ToString("x2")); // format "x2" makes this lowercase, conversely "X2" Uppercase. could run a ternary here, e.g. (returnLowerCase?"x2":"X2")
                }
            }

            string hash = result.ToString();

            if (!returnLowerCase)
            {
                hash = hash.ToUpper();
            }

            return hash;
        }

        /// <summary>
        /// Decodes Base64 data, or conversely encodes data to be base64
        /// </summary>
        /// <param name="data">Data to transform</param>
        /// <param name="encode">If false, returns decoded version of data. If true, returns base64 of data</param>
        /// <returns>Encoded/decoded data</returns>
        public static string Base64(string data, bool encode = true)
        {
            if (encode)
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
            }
            else
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(data));
            }

            // no catch
        }

        /// <summary>
        /// Checks whether given data is a base64 encoded string
        /// </summary>
        /// <param name="data">String to try</param>
        /// <returns>If the data was base64</returns>
        public static bool IsBase64Data(string data)
        {
            try
            {
                Convert.FromBase64String(data);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
