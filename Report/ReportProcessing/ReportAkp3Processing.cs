using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Report_scraper.ReportParameters;
using ScrapySharp.Html.Forms;
using ScrapySharp.Network;
using HtmlAgilityPack;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json;
using Report_scraper.Report.ReportProcessing;
using Report_scraper.Extensions;
using Report_scraper.Report.ReportParameters;

namespace Report_scraper.Report.ReportProcessing
{
    [ReportHandlerAttibute(ReportName = "АКП-3")]
    class ReportAkp3Processing : AkpReportProcessing
    {
        protected override DateTime StartDay { get; }
        protected override DateTime EndDate { get; }

        public ReportAkp3Processing(DateTime startDay, DateTime endDate)
        {
            StartDay = startDay;
            EndDate = endDate;
        }

        public override Dictionary<string,object> Parse(string data)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);
            var tableNode = doc.DocumentNode.SelectSingleNode("//table");

            string mainTableHeaders = tableNode.SelectNodes("//tr[@class=\"class_tr_header\"]")[2].InnerText;
            string decfectTableHeaders = tableNode.SelectNodes("//tr[@class=\"class_tr_header\"]")[4].InnerText;
            string brakTableHeaders = tableNode.SelectNodes("//tr[@class=\"class_tr_header\"]")[6].InnerText;

            var mainTable = tableNode.HtmlTableToObjects(
                    columnsNumber: 7,
                    tableHeader: mainTableHeaders,
                    tablePosition: 1,
                    endTable: "Итого",
                    (data) =>
                    {
                        return new
                        {
                            Diametr = double.Parse(data[0], new NumberFormatInfo { NumberDecimalSeparator = "," }),
                            Tolshchina_stenki = double.Parse(data[1], new NumberFormatInfo { NumberDecimalSeparator = "." }),
                            Marka_stali = data[2],
                            TU_chornoy = data[3],
                            TU_izolyatsii = data[4],
                            Kolichestvo = int.Parse(data[5]),
                            Inspektsiya = data[6],
                            BeginDate = StartDay,
                            EndDate = EndDate
                        };
                    }
                );

            var defectTable = tableNode.HtmlTableToObjects(
                    columnsNumber: 8,
                    tableHeader: decfectTableHeaders,
                    tablePosition: 1,
                    endTable: "Итого",
                    (data) =>
                    {
                        return new
                        {
                            Diametr = double.Parse(data[0], new NumberFormatInfo { NumberDecimalSeparator = "," }),
                            Tolshchina_stenki = double.Parse(data[1], new NumberFormatInfo { NumberDecimalSeparator = "." }),
                            Marka_stali = data[2],
                            TU_chornoy = data[3],
                            TU_izolyatsii = data[4],
                            Defekt = data[5],
                            Kolichestvo = int.Parse(data[6]),
                            Primechaniye = data[7],
                            BeginDate = StartDay,
                            EndDate = EndDate
                        };
                    }
                );

            var brakTable = tableNode.HtmlTableToObjects(
                    columnsNumber: 8,
                    tableHeader: brakTableHeaders,
                    tablePosition: 2,
                    endTable: "Всего",
                    (data) =>
                    {
                        return new
                        {
                            Diametr = double.Parse(data[0], new NumberFormatInfo { NumberDecimalSeparator = "," }),
                            Tolshchina_stenki = double.Parse(data[1], new NumberFormatInfo { NumberDecimalSeparator = "." }),
                            Marka_stali = data[2],
                            TU_chornoy = data[3],
                            TU_izolyatsii = data[4],
                            Defekt = data[5],
                            Kolichestvo = int.Parse(data[6]),
                            Primechaniye = data[7],
                            BeginDate = StartDay,
                            EndDate = EndDate
                        };
                    }
                );

            return new Dictionary<string, object>
            {
                { "akp3main", mainTable },
                { "akp3defect", defectTable },
                { "akp3brak", brakTable }
            };
        }
    }
}
