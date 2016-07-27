using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Log;
using KarmicEnergy.Web.Controllers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class MapController : BaseController
    {
        #region Index

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Index()
        {
            AddLog("Navigated to Map View", LogTypeEnum.Info);
            return View();
        }

        #endregion Index       
    }
}