using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CPUusige
{
    public class CPU
    {
        private float _percent;
        private PerformanceCounter _cpuCounter;

        public float Percent
        {
            get
            {
                _percent = _cpuCounter.NextValue();
                return _percent;
            }
        }

        public CPU()
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            Thread.Sleep(500);
            _percent = _cpuCounter.NextValue();
        }
    }
}
