using System;
using System.Collections.Generic;
using System.Text;

namespace Report_scraper.Portal.PortalParameters
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PortalParametersAttribute:Attribute
    {
        public string Login { get; set; } = null;
        public string Password { get; set; } = null;
        public string FormId { get; set; } = null;
        public string PasswordFieldName { get; set; } = null;
        public string LoginFieldName { get; set; } = null;
        public string Url { get; set; }
        public string Name { get; set; }
    }
}
