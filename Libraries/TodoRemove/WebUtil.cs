using System.Diagnostics;

namespace BackwardsCompatibility;

public static class WebUtil
{
    public static void OpenUrl(string url)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
}