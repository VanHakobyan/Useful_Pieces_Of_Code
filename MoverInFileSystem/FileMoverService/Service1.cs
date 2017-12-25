using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FileMoverService
{
    public partial class Service1 : ServiceBase
    {
        private System.Timers.Timer timer;
        public Service1()
        {
            InitializeComponent();
            timer = new System.Timers.Timer(10000);
            timer.Elapsed += TimerOnElapsed;
        }

        public void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            timer.Enabled = false;
            var fileNames = GetFileNames();
            foreach (var fileName in fileNames)
            {
                var fileSize = GetFileSize(fileName);
                if (double.Parse(fileSize.Split(' ').First()) > 10 && fileSize.Split(' ').Last() == "MB")
                {
                    var sourceFileName = fileName;
                    var destFileName =fileName.Replace(@"D:\move",@"D:\newTemp");
                    File.Move(sourceFileName, destFileName);
                }
            }
            timer.Enabled = true;
        }
        private List<string> GetFileNames()
        {
            return Directory.GetFiles(@"D:\move").ToList();
        }
        private static string GetFileSize(string filename)
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
        protected override void OnStart(string[] args)
        {
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }
    }
}
