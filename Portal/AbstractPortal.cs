using Report_scraper.Extensions;
using Report_scraper.Portal.PortalParameters;
using ScrapySharp.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Report_scraper
{
    public abstract class AbstractPortal : IEnumerable<IReport>
    {
        public abstract WebPage Context { get; } // хранение контекста открытого портала 
        protected WebPage GetContext()
        {
            var param = GetParams();
            return Browser.GetBrowser().NavigateToPage(new Uri(param.Url)).AuthenticationEx(param);
        }
        public PortalParametersAttribute GetPortalParameters { get => GetParams(); }
        protected virtual PortalParametersAttribute GetParams()
        {
            var type = this.GetType();
            var attr = type.GetCustomAttributes(typeof(PortalParametersAttribute), false);
            if (attr.Length > 0)
                return ((PortalParametersAttribute)attr[0]);

            throw new InvalidOperationException("No attribute specified for connecting to the portal!");
        }

        public abstract IEnumerator<IReport> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            return Context;
        }
    }
}
