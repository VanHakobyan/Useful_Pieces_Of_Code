using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace WebProxyManiputation
{
    class Program
    {
        static void Main(string[] args)
        {
          
            var sendGetRequest = Helper.SendGetRequest(Url, "83.164.222.15:8080");
        }
    }
}
