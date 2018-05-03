using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Helpers
{
    public class SimpleProxys
    {
        public IEnumerable<ProxyModel> GetProxys()
        {
            try
            {
                var jsonString = "";//File.ReadAllText($@"{Environment.CurrentDirectory}\Proxies.json".Replace("\\bin\\Debug", "").Replace("\\bin\\Release", ""));
                var jsonObject = JsonConvert.DeserializeObject<List<ProxyModel>>(jsonString);
                return jsonObject.Take(100);
            }
            catch (Exception ex)
            {
                //File.AppendAllText(@"D:\proxyWpf.log", $"{Environment.CurrentDirectory}");
                return null;
            }
        }
    }
}
