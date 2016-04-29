using System;
using System.Configuration;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Extensions
{
    public static class KEExtension
    {
        public static Boolean IsSite(this WebViewPage page)
        {
            String siteId = ConfigurationManager.AppSettings["Site:Id"];

            if (siteId.Trim() == String.Empty)
                return false;
            else
                return true;
        }
    }
}