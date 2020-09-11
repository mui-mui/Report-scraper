using Newtonsoft.Json.Linq;
using Report_scraper.ReportParameters;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report_scraper.Report.ReportProcessing
{
    public abstract class AkpReportProcessing : IReportProcessing
    {
        protected abstract DateTime StartDay { get; }
        protected abstract DateTime EndDate { get; }

        public string Load(string url)
        {
            var param = new AkpReportParameters().GetParameters();
            var browser = Browser.GetBrowser();

            param.formParams["ctl00_ContentPlaceHolder1_Date1"] = StartDay.ToString("dd.MM.yyyy");
            param.formParams["ctl00_ContentPlaceHolder1_Date2"] = EndDate.ToString("dd.MM.yyyy");

            var reportPage = browser.NavigateToPage(new Uri(url));
            var form = reportPage.FindForm(param.formId);

            foreach (string key in param.formParams)
            {
                form[key] = param.formParams[key];
            }
            foreach (string key in param.headParams)
            {
                browser.Headers.Add(key, param.headParams[key]);
            }
            form.Method = HttpVerb.Post;
            WebPage response = form.Submit();
            return JObject.Parse(response)["result"].ToString();
        }

        public abstract Dictionary<string, object> Parse(string data);
    }
}
