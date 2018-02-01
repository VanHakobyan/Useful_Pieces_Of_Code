using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMover
{
    class Program
    {
        static void Main(string[] args)
        {
            var mover = new Mover();
            mover.MoveZipedFile();
        }
    }
}
