using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string MenuItemActive(this HtmlHelper helper, string controller, string action, string area = "")
        {
            string classValue = "";
            var routeData = helper.ViewContext.RouteData;

            string currentArea = routeData.DataTokens["area"] as String;
            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            if (currentArea == area &&
                currentController == controller &&
                currentAction == action)
            {
                classValue = "active";
            }

            return classValue;
        }
    }
}