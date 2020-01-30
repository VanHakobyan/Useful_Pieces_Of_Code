using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ConcurrentQueueLib
{
    public class ConcurrentQueueLib
    {
        private readonly ConcurrentQueue<SimpleModel> _concurrentQueue = new ConcurrentQueue<SimpleModel>();
        public static readonly string LargeString = ReadRandomString();
        private static int _counter = 1;
        public readonly List<long> Milliseconds = new List<long>();
        private long _millisecondsCounter;
        private readonly Timer _timer = new Timer(1000);
        private volatile int _enqueueCount;
        private volatile int _dequeueCount;

        public ConcurrentQueueLib()
        {
            FillQueue();
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();

        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"enqueueCount={_enqueueCount},dequeueCount={_dequeueCount},elapsed={_millisecondsCounter}");
            FillQueue();
        }

        private static string ReadRandomString()
        {
            string content;
            using (var fileStream = new FileStream(@"D:\14512311.json", FileMode.Open))
            using (var sr = new StreamReader(fileStream))
            {
                content = sr.ReadToEnd();
            }

            return content;
        }

        private void FillQueue()
        {
            for (var i = 0; i < 10000; i++)
            {
                Task.Run(() =>
                {

                    _concurrentQueue.Enqueue(new SimpleModel
                    {
                        Id = _counter++, Message = $"{DateTime.Now}:{LargeString}", Number = float.MaxValue / _counter
                    });
                    _enqueueCount++;
                });
            }
        }

        public void MeasureTryDequeue()
        {
            var stopwatch = Stopwatch.StartNew();
            while (true)
            {
                stopwatch.Restart();
                if (_concurrentQueue.TryDequeue(out var result))
                {
                    _dequeueCount++;
                    var x = result.Id;
                    stopwatch.Stop();
                    Milliseconds.Add(stopwatch.ElapsedMilliseconds);
                    _millisecondsCounter += stopwatch.ElapsedMilliseconds;
                    //Thread.Sleep(1);
                }
                else
                {
                    //break;
                }
            }


        }
    }
}
