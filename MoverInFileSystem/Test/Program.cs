using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FileMoverService;
using Microsoft.Win32;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var sourceFileName = @"D:\move\StsParserOnlyPolish3rdLeague.rar";
            //var destFileName = @"D:\newTemp\p.rar";
            //File.Move(sourceFileName, destFileName);
            Service1 s=new Service1();
            s.TimerOnElapsed(null, null);
        }

        private static string GetFileSize(string filename)
        {
            string[] sizes = {"B", "KB", "MB", "GB", "TB"};
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

        private static void ConnectFtp(string hostname, string username, string password)
        {
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest requestDir = (FtpWebRequest) WebRequest.Create(hostname);
                requestDir.Method = WebRequestMethods.Ftp.ListDirectory;
                requestDir.Credentials = new NetworkCredential(username, password);
                FtpWebResponse responseDir = (FtpWebResponse) requestDir.GetResponse();
                var responseDirContentLength = responseDir.ContentLength;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
    }
}
