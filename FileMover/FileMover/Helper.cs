using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMover
{
    static class Helper
    {
        public static IEnumerable<string> GetFileNames(string currentDir)
        {
            return Directory.GetFiles(currentDir).ToList();
        }
        public static string GetFileSize(string filename)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = new FileInfo(filename).Length;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return $"{len:0.##} {sizes[order]}";
        }
        public static string GetTempDirectory()
        {
            return Directory.CreateDirectory(Path.Combine(@"D:\", Path.GetRandomFileName())).FullName;
        }
    }
}
