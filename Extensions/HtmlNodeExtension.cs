using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Report_scraper.Extensions
{
    public static class HtmlNodeExtension
    {
        public static List<object> HtmlTableToObjects(this HtmlNode tableNode, int columnsNumber, string tableHeader, int tablePosition, string endTable, Func<string[], object> objCreator)
        {
            bool isWorked = false;
            int curHeadPos = 1;
            var table = new List<object>();
            foreach (HtmlNode tr in tableNode.SelectNodes("//tr"))
            {
                if (tr.InnerText == tableHeader)
                {

                    // Нужно, чтобы можно было выбрать откуда начать чтение, если у разных таблиц одинаковые заголовки
                    if (curHeadPos != tablePosition)
                    {
                        curHeadPos++;
                        continue;
                    }

                    //включить сборку
                    isWorked = true;
                    continue;
                }

                if (isWorked)
                {
                    if (tr.InnerText.Trim().Contains(endTable)) break;

                    var tdTextArr = tr.ChildNodes
                        .Select(n => n.InnerHtml?.Trim().Replace("&quot;", ""))
                        .Select(n => n = n == "&nbsp;" ? null : n)
                        .ToArray();

                    if (tdTextArr.Length == columnsNumber)
                    {
                        table.Add(objCreator(tdTextArr));
                    }
                }
            };
            return table;
        }
    }
}
