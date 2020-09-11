using HtmlAgilityPack;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Report_scraper
{

    public class ReportBqrf : IReport
    {
        public string Url { get; }
        public string Name { get; }

        public ReportBqrf(string url, string name)
        {
            Url = url;
            Name = name;
        }
    }
}
