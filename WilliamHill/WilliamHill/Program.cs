using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;

namespace WilliamHill
{
    class Program
    {
        static void Main(string[] args)
        {
            var html = File.ReadAllText(@"D:\william.txt");
            var htmlDocument=new HtmlParser().Parse(html);
            var element = htmlDocument.Links.Where(x=>x.InnerHtml.ToLower().Contains("more")).ToList();//Children.SelectMany(x=>x.Children).Select(x=>x.ClassName=="header");//All.Where(x => x.TagName=="table").ToList();
            var leagueName = htmlDocument.QuerySelectorAll("body > table");
        }
    }
}
