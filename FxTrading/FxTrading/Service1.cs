using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FxTrading
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Task task = new Task(Action, TaskCreationOptions.LongRunning);
            task.Start();
        }

        private void Action()
        {
            FxScrapper fxScrapper = new FxScrapper();
            fxScrapper.CheckPrice();
        }

        protected override void OnStop()
        {
            FxScrapper._driver.Quit();
            Thread.Sleep(5000);
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
