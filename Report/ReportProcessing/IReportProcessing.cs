using System;
using System.Collections.Generic;
using System.Text;

namespace Report_scraper
{
    public interface IReportProcessing
    {
        string Load(string url);
        Dictionary<string,object> Parse(string data);
    }
}
