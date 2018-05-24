using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using Helpers.XmlModels;
using Newtonsoft.Json;

namespace Helpers
{
    public class SimpleProxys
    {
        public IEnumerable<ProxyViewModel> GetProxys()
        {
            try
            {
#if DEBUG
                var proxies = File.ReadAllLines(@"../../stat/proxies.csv");
                return proxies.Select(x => new ProxyViewModel { IpAddress = x }).ToList();
#else
                var proxies = new List<ProxyViewModel>();
                using (var reader = new StreamReader(@"stat/proxies.xml"))
                {
                    var xRoot = new XmlRootAttribute { ElementName = "root", IsNullable = true };
                    var serializer = new XmlSerializer(typeof(root), xRoot);
                    var deserialize = (root)serializer.Deserialize(reader);
                    proxies.AddRange(deserialize.row.Select(rootRow => new ProxyViewModel { IpAddress = $"{rootRow.IP}:{rootRow.PORT}" }));
                }
                return proxies;
               
#endif
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return new List<ProxyViewModel>();
            }
        }
    }
}

