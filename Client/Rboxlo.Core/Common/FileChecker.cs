using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rboxlo.Core.Common
{
    /// <summary>
    /// Checks files against a SHA256 manifest to see if they are malformed, and returns "earmarked" files-- files that failed the check
    /// </summary>
    public class FileChecker : IDisposable
    {
        private static Dictionary<string, string> hashes;
        private static List<string> earmarks = new List<string>();
        private static bool completed = false;

        private static bool disposedValue;

        /// <summary>
        /// Class constructor for FileChecker
        /// </summary>
        /// <param name="_hashes">Hash list</param>
        public FileChecker(Dictionary<string, string> _hashes)
        {
            hashes = _hashes;
        }

        /// <summary>
        /// Starts the test
        /// </summary>
        public async Task Run()
        {
            Work();

            while (!completed)
            {
                await Task.Delay(200);
            }

            return;
        }

        /// <summary>
        /// Actual FileChecker work
        /// </summary>
        private static void Work()
        {
            foreach (KeyValuePair<string, string> file in hashes)
            {
                string location = file.Key;
                string hash = file.Value;
                bool success = Verify(location, hash);

                if (!success) earmarks.Add(location);
            }

            completed = true;
        }

        /// <summary>
        /// Returns earmarked files
        /// </summary>
        /// <returns>Earmarked files</returns>
        public List<string> GetEarmarkedFiles()
        {
            return earmarks;
        }

        /// <summary>
        /// Verifies a file with given hash
        /// </summary>
        /// <param name="location">Location of file</param>
        /// <param name="hash">SHA256 hash</param>
        /// <returns>Whether the hash comparison has succeeded</returns>
        private static bool Verify(string location, string hash)
        {
            object result = Crypto.Sha256(location, true, true);
            if (result is bool)
            {
                throw new ArgumentException($"Given file {location} does not exist", location);
            }

            return (result.ToString().Trim() == hash.ToLower().Trim());
        }

        /// <summary>
        /// Disposes of FileChecker
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                hashes = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
