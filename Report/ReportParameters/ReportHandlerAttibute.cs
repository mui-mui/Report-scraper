using System;
using System.Collections.Generic;
using System.Text;

namespace Report_scraper.Report.ReportParameters
{
    [AttributeUsage(AttributeTargets.Class)]
    class ReportHandlerAttibute:Attribute
    {
        public string ReportName { get; set; }
    }
}
