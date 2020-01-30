using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrentCollectionProblems
{
    class Program
    {
        static void Main(string[] args)
        {

            ConcurrentQueueLib.ConcurrentQueueLib concurrentQueueLib = new ConcurrentQueueLib.ConcurrentQueueLib();

            Task.Run(() => concurrentQueueLib.MeasureTryDequeue());

            Console.ReadKey();
        }
    }
}
