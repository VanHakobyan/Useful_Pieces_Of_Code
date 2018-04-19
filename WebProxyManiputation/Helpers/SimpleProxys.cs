using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Helpers
{
   public class SimpleProxys
    {
        public IEnumerable<ProxyModel> GetProxys()
        {
            var jsonString = File.ReadAllText($@"{Environment.CurrentDirectory}\Proxies.json".Replace("\\bin\\Debug",""));
            var jsonObject = JsonConvert.DeserializeObject<List<ProxyModel>>(jsonString);
            return jsonObject.Take(10);
        }
    }
}
