using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using Newtonsoft.Json;

namespace Helpers
{
    public class SimpleProxys
    {
        public IEnumerable<ProxyViewModel> GetProxys()
        {
            try
            {
                var proxies = File.ReadAllLines(@"../../stat/proxies.csv");
                return proxies.Select(x => new ProxyViewModel { IpAddress = x }).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
