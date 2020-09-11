using Report_scraper.Portal.PortalParameters;
using ScrapySharp.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Report_scraper.Portal
{
    [PortalParameters(
        Url = "",
        Name = "BqrfPortal",
        Login = "",
        Password = "",
        FormId = "form1",
        LoginFieldName = "txbLogin",
        PasswordFieldName = "txbPassword"
    )]
    public class BqrfPortal : AbstractPortal
    {
        private readonly WebPage context;
        public override WebPage Context => context;
        public BqrfPortal()
        {
            this.context = GetContext();
        }

        public override IEnumerator<IReport> GetEnumerator()
        {
            var matches = new Regex(@"{text:\W([\w\s()-]+)\W,iconCls:\Wicon-report\W,href:\W(http://sys00098.d0.vsw.ru/BQRF/\w+\W[\w%\?&=]+)\W}").Matches(context);
            if (matches.Count == 0) yield break;
            foreach (Match match in matches)
            {
                yield return new ReportBqrf(match.Groups[2].Value, match.Groups[1].Value);
            }
        }
    }
}
