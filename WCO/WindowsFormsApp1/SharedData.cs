using System.IO;

namespace WindowsFormsApp1
{
    internal static class SharedData
    {
        public static string GetCacheDirectory()
        {
            var dir = @"C:\Code\Fusion.WorldCheckOne\cache";
            if (!Directory.Exists(dir))
            {
                dir = @"D:\Code\Fusion.WorldCheckOne\cache";
                if (!Directory.Exists(dir))
                {
                    dir = string.Empty;
                }
            }
            //
            return dir;
        }
    }
}