using System;
using System.IO;
using System.Linq;
using System.Threading;
using EntityLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace YViewer
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var list = File.ReadAllLines(@"C:\Users\vanik.hakobyan\Desktop\proxy.txt").ToList();
            var viewCount = 0;
            var i = 15000;
            while (i++ != 32079)
            {
                var ourProxy = list[i];
                try
                {
                    var profile = new FirefoxProfile();
                    var PROXY = ourProxy;
                    var proxy = new Proxy();
                    proxy.HttpProxy = PROXY;
                    proxy.FtpProxy = PROXY;
                    proxy.SslProxy = PROXY;
                    profile.SetProxyPreferences(proxy);
                    var driver = new FirefoxDriver(profile);
                    driver.Navigate().GoToUrl("https://www.youtube.com/watch?v=BTZ91oTr6mQ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"View +1 = {++viewCount}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(5000);
                    driver.Quit();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message + " " + exception.StackTrace);
                }
            }
        }
    }
}
