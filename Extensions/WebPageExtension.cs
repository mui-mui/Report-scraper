using Report_scraper.Portal.PortalParameters;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report_scraper.Extensions
{
    public static class WebPageExtension
    {
        public static WebPage AuthenticationEx(this WebPage webPage, PortalParametersAttribute parameters)
        {
            if(parameters.Login != null)
            {
                var form = webPage.FindFormById(parameters.FormId);
                form[parameters.LoginFieldName] = parameters.Login;
                form[parameters.PasswordFieldName] = parameters.Password;

                form.Method = HttpVerb.Post;
                return form.Submit();
            }
            return webPage;
        }
    }
}
