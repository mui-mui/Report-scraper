using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Report_scraper
{
    /// <summary>
    /// Класс содержит описание одного параметра для настроки отчета
    /// </summary>
    public class ParametersReportInfo
    {
        public string formId;
        public NameValueCollection formParams;
        public NameValueCollection headParams;
    }
}
