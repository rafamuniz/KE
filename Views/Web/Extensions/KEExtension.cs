using KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger;
using Munizoft.MVC.Helpers;
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
            Guid id;
            if (!String.IsNullOrEmpty(siteId) && Guid.TryParse(siteId, out id))
                return true;
            return false;
        }

        public static MvcHtmlString SensorEditButton(this HtmlHelper htmlHelper, SensorViewModel viewModel)
        {
            if (viewModel.PondId.HasValue)
            {
                return ActionLinkHelper.NoEncodeActionLink(htmlHelper, "<span class='glyphicon glyphicon-pencil'></span>", "Edit", "PondEdit", "Sensor", new { id = viewModel.Id, PondId = viewModel.PondId, area = "Customer" }, htmlAttributes: new { data_modal = "", @class = "btn btn-default" });
            }
            else if (viewModel.TankId.HasValue)
            {
                return ActionLinkHelper.NoEncodeActionLink(htmlHelper, "<span class='glyphicon glyphicon-pencil'></span>", "Edit", "TankEdit", "Sensor", new { id = viewModel.Id, TankId = viewModel.TankId, area = "Customer" }, htmlAttributes: new { data_modal = "", @class = "btn btn-default" });
            }
            else
            {
                return ActionLinkHelper.NoEncodeActionLink(htmlHelper, "<span class='glyphicon glyphicon-pencil'></span>", "Edit", "SiteEdit", "Sensor", new { id = viewModel.Id, SiteId = viewModel.SiteId, area = "Customer" }, htmlAttributes: new { data_modal = "", @class = "btn btn-default" });
            }
        }

        public static MvcHtmlString TriggerEditButton(this HtmlHelper htmlHelper, TriggerViewModel viewModel)
        {
            if (viewModel.PondId.HasValue)
            {
                return ActionLinkHelper.NoEncodeActionLink(htmlHelper, "<span class='glyphicon glyphicon-pencil'></span>", "Edit", "PondEdit", "Trigger", new { id = viewModel.Id, PondId = viewModel.PondId, area = "Customer" }, htmlAttributes: new { data_modal = "", @class = "btn btn-default" });
            }
            else if (viewModel.TankId.HasValue)
            {
                return ActionLinkHelper.NoEncodeActionLink(htmlHelper, "<span class='glyphicon glyphicon-pencil'></span>", "Edit", "TankEdit", "Trigger", new { id = viewModel.Id, TankId = viewModel.TankId, area = "Customer" }, htmlAttributes: new { data_modal = "", @class = "btn btn-default" });
            }
            else
            {
                return ActionLinkHelper.NoEncodeActionLink(htmlHelper, "<span class='glyphicon glyphicon-pencil'></span>", "Edit", "SiteEdit", "Trigger", new { id = viewModel.Id, SiteId = viewModel.SiteId, area = "Customer" }, htmlAttributes: new { data_modal = "", @class = "btn btn-default" });
            }
        }
    }
}