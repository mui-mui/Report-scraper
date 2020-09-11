using Report_scraper.Portal.PortalParameters;
using ScrapySharp.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Report_scraper
{
    [PortalParameters(Login = null)]
    class PortalWeightSystem : IPortal
    {
        public WebPage Context { get; private set; }
        public string Url { get; private set; }

        public IEnumerator<IReport> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void OpenPortal()
        {
            Context = Browser.GetBrowser().NavigateToPage(new Uri(Url));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
