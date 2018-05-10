using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Helper
    {
        public static async Task<string> SendGetRequest(string uri, string ipAddress, string port)
        {
            var response = string.Empty;

            try
            {

                ServicePointManager.DefaultConnectionLimit = 10;
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.DnsRefreshTimeout = 1000;
                ServicePointManager.UseNagleAlgorithm = false;

                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
                var builder = new UriBuilder(ipAddress)
                {
                    Port = int.Parse(port)
                };
                IWebProxy proxy = new WebProxy(builder.Uri);
                request.Proxy = proxy;
                request.Timeout = 30000;
                using (var stream = (await request.GetResponseAsync()).GetResponseStream())
                {
                    if (stream != null)
                    {
                        stream.ReadTimeout = 30000;

                        using (var streamReader = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
                        {
                            response = await streamReader.ReadToEndAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return response;
        }
    }
}
