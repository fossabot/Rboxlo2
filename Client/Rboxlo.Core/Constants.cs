using System;
using System.Collections.Generic;
using Rboxlo.Core.Common;

namespace Rboxlo.Core
{
    /// <summary>
    /// Global constants compiled into each Client project. That means don't put sensitive information here. If you need to, add a .config or use the registry
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Whether we are debugging or not
        /// </summary>
#if DEBUG
        public static bool IsDebugging = true;
#else
        public static bool IsDebugging = false;
#endif

        /// <summary>
        /// Website domain
        /// </summary>
        public static string BaseURL = "rboxlo.loc";

        /// <summary>
        /// Project name
        /// </summary>
        public static string ProjectName = "Rboxlo";

        /// <summary>
        /// Base path to the Rboxlo registry key
        /// </summary>
        public static string BaseRegistryPath = String.Format(@"SOFTWARE\{0}", "Rboxlo");
    }
}