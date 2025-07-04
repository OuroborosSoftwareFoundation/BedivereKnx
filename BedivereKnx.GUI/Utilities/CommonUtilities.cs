using System.Diagnostics;

namespace BedivereKnx.GUI
{

    internal static class CommonUtilities
    {

        /// <summary>
        /// 调用系统默认浏览器打开url
        /// </summary>
        /// <param name="url"></param>
        internal static void OpenUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;
            ProcessStartInfo psi = new(url)
            {
                UseShellExecute = true,
            };
            Process.Start(psi);
        }

    }

}
