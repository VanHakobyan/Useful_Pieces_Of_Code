using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleRegexDataTime
{
    class Program
    {
        static void Main(string[] args)
        {
            var content=File.ReadAllText(@"Your Address\TextFile.txt");
            var datatime = Regex.Match(content, "\'\\d+\\/+.*?\'").Value.Trim('\'');
        }
    }
}
