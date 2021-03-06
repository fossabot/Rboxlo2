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
        public static bool Debugging = true;
#else
        public static bool Debugging = !DotEnv.PRODUCTION;
#endif

        /// <summary>
        /// Website domain
        /// </summary>
        public static string BaseURL = DotEnv.SERVER_DOMAIN;

        /// <summary>
        /// Proper project name (title-cased)
        /// </summary>
        public static string ProjectName = Util.ToTitleCase(DotEnv.NAME);
    }
}