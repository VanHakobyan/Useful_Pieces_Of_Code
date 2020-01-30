using System;
using System.Net.Http;
using System.Text;
using Microsoft.Owin.Hosting;

namespace Self_Asp.Net
{
    public class Program
    {
        static void Main()
        {

            var sb = new StringBuilder("Objects Count:");
            sb.AppendLine();
            sb.AppendLine($"Partner=");
            sb.AppendLine($"TranslationEntry=");
            sb.AppendLine($"Sport=");
            sb.AppendLine($"Region=");
            Console.WriteLine(sb.ToString());

            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                var response = client.GetAsync(baseAddress + "api/values").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }
        }
    }
}
