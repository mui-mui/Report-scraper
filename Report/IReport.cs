using HtmlAgilityPack;
using Report_scraper.Report.ReportParameters;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Report_scraper
{
    public interface IReport
    {
        string Url { get; }
        string Name { get; }

        virtual Dictionary<string, object> GetReport(DateTime startDay, DateTime endDay)
        {
            var classProcessing = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.Namespace == "Report_scraper.Report.ReportProcessing" & type.IsClass)
                .FirstOrDefault(c => 
                        c.GetCustomAttributes<ReportHandlerAttibute>()
                        .FirstOrDefault(attr => attr.ReportName == Name) != null
                 );

            if(classProcessing != null)
            {
                var processingObj = Activator.CreateInstance(classProcessing, new object[] { startDay, endDay });

                string data = (string)classProcessing.GetMethod("Load").Invoke(processingObj, new string[] { Url });
                return (Dictionary<string, object>)classProcessing.GetMethod("Parse").Invoke(processingObj, new string[] { data });
            }
            return null;
        }
    }
}
