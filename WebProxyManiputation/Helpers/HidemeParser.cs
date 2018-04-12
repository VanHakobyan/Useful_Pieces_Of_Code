using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Helpers
{
    public class HidemeParser
    {
        private static string url = "https://hidemy.name/en/proxy-list/?start={page}";
        public async Task<string> SendGetRequest(string uri)
        {
            string response = "";

            try
            {
                ServicePointManager.DefaultConnectionLimit = 10;
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.DnsRefreshTimeout = 1000;
                ServicePointManager.UseNagleAlgorithm = false;


                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                //request.Headers.Add("accept-encoding", "gzip, deflate, br");
                request.Headers.Add("accept-language", "en-US,en;q=0.9,hy;q=0.8,mt;q=0.7");
                request.Headers.Add("cookie", "__cfduid=d6a63aa8059242bf5001d1146357fb3df1523510667; cf_clearance=d9c618e71b19cba99319b6482d5a3292e8708a58-1523510672-86400; ca_id=7PLZ1N23NHZAehf%7C5aceed90; _ga=GA1.2.587552409.1523510674; _gid=GA1.2.1659191249.1523510674; _ym_uid=1523510674592833539; _ym_isad=2; _ym_visorc_42065329=w; jv_enter_ts_EBSrukxUuA=1523510678137; jv_visits_count_EBSrukxUuA=1; jv_refer_EBSrukxUuA=https%3A%2F%2Fhidemy.name%2Fen%2Fproxy-list%2F%3Fclick_id%3D7PLZ1N23NHZAehf%26aip%3D5zcW%26utm_source%3Dcityads%26utm_medium%3Dcpa%26utm_campaign%3Dcityads; jv_utm_EBSrukxUuA=; t=63848138; PAPVisitorId=6c43b5f1ef5733f0193c10e309afbbae; _dc_gtm_UA-90263203-1=1; jv_pages_count_EBSrukxUuA=12");
                using (var stream = (await request.GetResponseAsync()).GetResponseStream())
                {
                    stream.ReadTimeout = 30000;
                    using (var streamReader = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
                    {
                        response = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                Thread.Sleep(1000);
            }


            return response;
        }

        public async Task<string> GetContent(int pageNumber)
        {
            return await SendGetRequest(url.Replace("{page}", pageNumber.ToString()));

        }
        public async Task<List<ProxyModel>> GetProxy()
        {
            var proxyList = new List<ProxyModel>();
            HtmlDocument document = new HtmlDocument();
            var pageSource = await GetContent(64);
            document.LoadHtml(pageSource);
            var pagination = document.DocumentNode.SelectSingleNode(".//div[@class='proxy__pagination']");
            var lastPageIndex = pagination.FirstChild.LastChild.InnerText;
            var lastPageStartWith = int.Parse(lastPageIndex) * 64;
            for (var i = 64; i < lastPageStartWith + 1; i += 64)
            {
                var tbody = document.DocumentNode.SelectSingleNode(".//table[@class='proxy__t']").ChildNodes.FirstOrDefault(x => x.Name == "tbody");
                if(tbody is null) continue;
                foreach (var childNode in tbody.ChildNodes)
                {
                    var proxy = new ProxyModel
                    {
                        Ip = childNode.ChildNodes[0].InnerText,
                        Port = childNode.ChildNodes[1].InnerText,
                        Type = childNode.ChildNodes[4].InnerText,
                        Country = childNode.ChildNodes[2].InnerText.Replace(" &nbsp;", "").Trim()
                    };
                    proxyList.Add(proxy);
                }
                pageSource =await GetContent(i);
                document.LoadHtml(pageSource);
            }
            return proxyList;
        }
    }
}
