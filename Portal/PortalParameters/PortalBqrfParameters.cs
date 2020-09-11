using Report_scraper.Portal.PortalParameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report_scraper
{
    [PortalParameters(
            Url = "http://sys00098.d0.vsw.ru/BQRF/AuthenticationPage.aspx",
            Login = "titov_as",
            Password = "Tit17346446",
            FormId = "form1",
            LoginFieldName = "txbPassword",
            PasswordFieldName = "txbLogin"
        )]
    public class PortalBqrfParameters : IPortalParameters
    {
    }
}
