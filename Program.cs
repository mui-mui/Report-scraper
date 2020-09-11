using System;
using System.Collections;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp;
using ScrapySharp.Extensions;
using ScrapySharp.Html.Forms;
using ScrapySharp.Network;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Report_scraper.ReportParameters;
using System.Globalization;
using Newtonsoft.Json;
using System.Text;
using Report_scraper.Portal;

namespace Report_scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            



            // Аргументы указыаются последовательно в формате:
            // <report name 1>, <begin date 1>, <end date 1>, ... , <report name N>, <begin date N>, <end date N>

            if (args.Length % 3 != 0) throw new ArgumentException("Указаны не все аргументы!");
            var reportArgs = Enumerable.Range(0, args.Length / 3).Select(i => args.Skip(3 * i).Take(3).ToList()).ToList();

            //var reportArgs = new List<List<string>> { new List<string> { "АКП-1", "01.08.2020", "19.08.2020" }, new List<string> { "АКП-3", "01.08.2020", "19.08.2020" }, new List<string> { "АКП-4", "01.08.2020", "19.08.2020" } };


            var portal = new BqrfPortal();
            

            var result = new Dictionary<string, object>();
            foreach (var rArg in reportArgs)
            {
                Dictionary<string, object> resp = null;
                var beginDate = DateTime.Parse(rArg[1], CultureInfo.GetCultureInfo("ru-RU"));
                var endDate = DateTime.Parse(rArg[2], CultureInfo.GetCultureInfo("ru-RU"));
                
                var report = portal.FirstOrDefault(r => r.Name == rArg[0]);
                resp = report.GetReport(beginDate, endDate);
                
                if (resp != null) result = result.Concat(resp).ToDictionary(pair => pair.Key, pair => pair.Value);
            }
            string filePath = Path.Join("/tmp/report_temporary", Path.GetRandomFileName());
            File.WriteAllText(filePath, JsonConvert.SerializeObject(result));
            Console.WriteLine(filePath);

        }
    }
}
