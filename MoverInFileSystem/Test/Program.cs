using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
          
            bool success;
            var username = @"vanik.Hakobyan";
            var password = "VAN606580##";
            var hostname = @"ftp://172.16.79.185:21";
            string existingFilepath = @"D:\edik.zip";
            string newFilepath = @"D:\edik.zip";


            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest requestDir = (FtpWebRequest)WebRequest.Create(hostname);
                requestDir.Method = WebRequestMethods.Ftp.ListDirectory;
                requestDir.Credentials = new NetworkCredential(username, password);
                FtpWebResponse responseDir = (FtpWebResponse)requestDir.GetResponse();
                var responseDirContentLength = responseDir.ContentLength;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
    }
}
