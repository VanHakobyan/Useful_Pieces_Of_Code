using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CPUusige
{
    class Program
    {
        public static string GetUsageCPU()
        {
            PerformanceCounter CPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            var x = CPUCounter.NextValue().ToString() + "%";
            return x;
        }
        static void Main(string[] args)
        {
            // Ne pas modifier l'ordre pour l'instant ------------

            Processes p = new Processes();
            CPU cpu = new CPU();
            float currentPercent = cpu.Percent;
            p.Update(currentPercent);
            // ---------------------------------------------------


            var activeProcess = p.ProcessList.Where(ap => ap.CpuUsage > 0);
            //string jsonResult = JsonConvert.SerializeObject(activeProcess);

            //activeProcess.ToList().ForEach(process => System.Console.WriteLine(process.CpuUsage + " - " + process.Name));
            var cpoP = 0f;
            foreach (var processInfo in activeProcess)
            {
                cpoP += processInfo.CpuUsage;
            }
            Console.WriteLine(cpoP);

            //double t = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            //StreamWriter fileout = new StreamWriter("c:\\temp\\jsonprocessoutput\\" + t.ToString() + ".json");

            //fileout.Write(jsonResult);
            //fileout.Close();


            //StreamWriter csvOut = new StreamWriter("c:\\temp\\csvprocessoutput\\" + t.ToString() + ".csv");
            ////csvOut.WriteLine("\"Name\", \"CpuUsage");
            //activeProcess.ToList().ForEach(process => csvOut.WriteLine("\"" + process.Name + "\"" + "," + "\"" + process.CpuUsage + "\""));
            //csvOut.Close();
        }
    }
}
