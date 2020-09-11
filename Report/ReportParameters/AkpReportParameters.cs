using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace Report_scraper.ReportParameters
{
    class AkpReportParameters : IReportParameters
    {
        public ParametersReportInfo GetParameters()
        {
            return new ParametersReportInfo
            {
                formId = "aspnetForm",
                formParams = new NameValueCollection
                {
                    { "ctl00_ContentPlaceHolder1_Date1", "" },
                    { "ctl00_ContentPlaceHolder1_Date2", "" },
                    { "__EVENTTARGET", "ctl00$ScriptManagerEXT" },
                    { "__EVENTARGUMENT", "-|public|LoadReport" },
                    { "submitAjaxEventConfig", "{\"config\":{\"extraParams\":{\"bFlag\":false}}}" }
                },
                headParams = new NameValueCollection
                {
                    { "X-Coolite", "delta=true" }
                }
            };
        }
    }
}
