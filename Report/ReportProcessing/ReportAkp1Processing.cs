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
    [ReportHandlerAttibute(ReportName = "АКП-1")]
    class ReportAkp1Processing : AkpReportProcessing
    {
        protected override DateTime StartDay { get; }
        protected override DateTime EndDate { get; }

        public ReportAkp1Processing(DateTime startDay, DateTime endDate)
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
                    columnsNumber: 9,
                    tableHeader: mainTableHeaders,
                    tablePosition: 1,
                    endTable: "Итого",
                    (data) =>
                    {
                        return new
                        {
                            Diametr = double.Parse(data[0]),
                            Tolshchina_stenki = double.Parse(data[1], new NumberFormatInfo { NumberDecimalSeparator = "." }),
                            Marka_stali = data[2],
                            Postavshchik = data[3],
                            Tsekh = data[4],
                            TU_chornoy = data[5],
                            TU_izolyatsii = data[6],
                            Kolichestvo = int.Parse(data[7]),
                            Inspektsiya = data[8],
                            BeginDate = StartDay,
                            EndDate = EndDate
                        };
                    }
                );

            var defectTable = tableNode.HtmlTableToObjects(
                    columnsNumber: 10,
                    tableHeader: decfectTableHeaders,
                    tablePosition: 1,
                    endTable: "Итого",
                    (data) =>
                    {
                        return new
                        {
                            Diametr = double.Parse(data[0]),
                            Tolshchina_stenki = double.Parse(data[1], new NumberFormatInfo { NumberDecimalSeparator = "." }),
                            Marka_stali = data[2],
                            Postavshchik = data[3],
                            Tsekh = data[4],
                            TU_chornoy = data[5],
                            TU_izolyatsii = data[6],
                            Defekt = data[7],
                            Kolichestvo = int.Parse(data[8]),
                            Primechaniye = data[9],
                            BeginDate = StartDay,
                            EndDate = EndDate
                        };
                    }
                );


            var brakTable = tableNode.HtmlTableToObjects(
                    columnsNumber: 10,
                    tableHeader: brakTableHeaders,
                    tablePosition: 2,
                    endTable: "Брак",
                    (data) =>
                    {
                        return new
                        {
                            Diametr = double.Parse(data[0]),
                            Tolshchina_stenki = double.Parse(data[1], new NumberFormatInfo { NumberDecimalSeparator = "." }),
                            Marka_stali = data[2],
                            Postavshchik = data[3],
                            Tsekh = data[4],
                            TU_chornoy = data[5],
                            TU_izolyatsii = data[6],
                            Defekt = data[7],
                            Kolichestvo = int.Parse(data[8]),
                            Primechaniye = data[9],
                            BeginDate = StartDay,
                            EndDate = EndDate
                        };
                    }
                );

            return new Dictionary<string, object>
            {
                { "akp1main", mainTable },
                { "akp1defect", defectTable },
                { "akp1brak", brakTable }
            };
        }
    }
}
