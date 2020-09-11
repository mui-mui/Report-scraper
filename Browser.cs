using ScrapySharp.Html.Forms;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Text;
using ScrapySharp.Exceptions;

namespace Report_scraper
{
    class Browser
    {
        private Browser() { }
        private static ScrapingBrowser browser;
        private static readonly object _lock = new object();

        public static ScrapingBrowser GetBrowser()
        {
            if (browser == null)
            {
                lock (_lock)
                {
                    if(browser == null)
                    {
                        browser = new ScrapingBrowser
                        {
                            Encoding = System.Text.Encoding.UTF8,
                            UserAgent = FakeUserAgents.Chrome,
                            AllowAutoRedirect = true,
                            AllowMetaRedirect = true,
                            KeepAlive = true,
                            Timeout = TimeSpan.FromMinutes(15)
                            
                        };
                    }
                }
            }
            return browser;
        }
    }
}
