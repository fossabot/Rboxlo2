using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Rboxlo.Core.Platform
{
    /// <summary>
    /// Methods and variables specific to the Windows operating system. These are all tested to work on Win10 ONLY
    /// </summary>
    public static class Windows
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        private const int GWL_STYLE = -16;
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_DLGMODALFRAME = 0x0001;
        private const int WS_SYSMENU = 0x80000;

        /// <summary>
        /// Base path to the Rboxlo registry key
        /// </summary>
        public static string BaseRegistryPath = String.Format(@"SOFTWARE\{0}", ToMachineReadable(DotEnv.NAME));

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

        /// <summary>
        /// Adds a program to the uninstall registry
        /// </summary>
        /// <param name="icon">Sets the icon of the program in the Control Panel. Path to an icon resource (can just do "game.exe,0")</param>
        /// <param name="applicationName">Application name, not displayed</param>
        /// <param name="displayName">Proper name displayed</param>
        /// <param name="version">Display version</param>
        /// <param name="uninstall">Command line arguments to uninstall the program</param>
        /// <param name="location">The path/folder where the program is being installed</param>
        /// <param name="url">About page for the application. Leave as blank if no page</param>
        public static void AddUninstallOption(string icon, string applicationName, string displayName, string version, string location, string uninstall, string url = null)
        {
            DateTime now = DateTime.Today;
            int install = Convert.ToInt32(now.ToString("yyyymmdd"));

            RegistryKey key = Registry.CurrentUser.CreateSubKey(String.Format(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{0}", applicationName));
            key.SetValue("DisplayIcon", icon, RegistryValueKind.String);
            key.SetValue("DisplayName", displayName, RegistryValueKind.String);
            key.SetValue("DisplayVersion", version, RegistryValueKind.String);
            key.SetValue("InstallDate", install, RegistryValueKind.String);
            key.SetValue("InstallLocation", location, RegistryValueKind.String);
            key.SetValue("NoModify", 1, RegistryValueKind.DWord);
            key.SetValue("NoRepair", 1, RegistryValueKind.DWord);
            key.SetValue("Publisher", displayName, RegistryValueKind.String);
            key.SetValue("UninstallString", uninstall, RegistryValueKind.String);

            if (url != null)
            {
                key.SetValue("URLInfoAbout", url, RegistryValueKind.String);
            }

            key.Close();
        }

        /// <summary>
        /// Adds an application's URI protocol
        /// </summary>
        /// <param name="protocol">Application name or "protocol", as the "game" in "game://args"</param>
        /// <param name="path">Direct application path</param>
        /// <param name="description">Application description</param>
        public static void AddURIProtocol(string protocol, string path, string description)
        {
            RegistryKey uri = Registry.ClassesRoot.CreateSubKey(protocol);
            uri.SetValue(null, description);
            uri.SetValue("URL Protocol", String.Empty);
            uri.Close();

            RegistryKey shell = Registry.ClassesRoot.CreateSubKey(String.Format(@"{0}\Shell\open\command", protocol));
            shell.SetValue(null, String.Format("\"{0}\" \"%1\"", path));
            shell.Close();
        }

        /// <summary>
        /// Deletes an uninstall option from registry
        /// </summary>
        /// <param name="application">Name of application</param>
        public static void RemoveUninstallOption(string application)
        {
            Registry.ClassesRoot.DeleteSubKeyTree(String.Format(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{0}", application));
        }

        /// <summary>
        /// Deletes a URI protocol
        /// </summary>
        /// <param name="protocol">Protocol to remove</param>
        public static void RemoveURIProtocol(string protocol)
        {
            Registry.ClassesRoot.DeleteSubKey(protocol);
        }

        /// <summary>
        /// Removes the icon from a WPF window
        /// </summary>
        /// <param name="window">Window to modify</param>
        public static void RemoveIcon(Window window)
        {
            IntPtr hWnd = new WindowInteropHelper(window).Handle;
            int extendedStyle = GetWindowLong(hWnd, GWL_EXSTYLE);

            SetWindowLong(hWnd, GWL_EXSTYLE, extendedStyle | WS_EX_DLGMODALFRAME);
        }

        /// <summary>
        /// Removes the close button from a WPF window
        /// </summary>
        /// <param name="window">Window to modify</param>
        public static void RemoveCloseButton(Window window)
        {
            IntPtr hWnd = new WindowInteropHelper(window).Handle;
            int style = GetWindowLong(hWnd, GWL_STYLE);

            SetWindowLong(hWnd, GWL_STYLE, style & ~WS_SYSMENU);
        }
    }
}
