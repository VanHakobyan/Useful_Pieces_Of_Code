using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProxyDownloader
{
    public static class Proxys
    {
        public static List<string> CatchFreeProxies()
        {
            const string urlAddress = @"https://free-proxy-list.net/";
            const string pattern = "<td>(.*?)</td>";
            var html = new WebClient().DownloadString(urlAddress);
            var matches = Regex.Matches(html, pattern);
            var proxyList = matches.Cast<Match>().Select(match => match.Groups[1].Value).ToList();

            var addresses = new List<string>();
            var ports = new List<int>();
            for (var i = 0; i < proxyList.Count; i++)
            {
                if (i % 4 == 0) addresses.Add(proxyList[i]);
                if ((i - 1) % 4 == 0) ports.Add(int.Parse(proxyList[i]));
            }
            var myProxies = addresses.Select((t, i) => new MyProxy { IpAddress = t, Port = ports[i]});
            var proxyConcat= myProxies.Select(myProxy => myProxy.IpAddress + ":" + myProxy.Port).ToList();
            return proxyConcat;
        }
    }
}
