using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services;
using KarmicEnergy.Core.Services.Interface;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Log;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Map;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.ViewModels.Shared;
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
        #region Constructor

        public MapController()
        {

        }

        #endregion Constructor

        #region Index

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel();
            ISiteService siteService = new SiteService(KEUnitOfWork);

            if (!IsSite)
            {
                var sites = siteService.GetsByCustomer(CustomerId);
                viewModel.Sites = SiteViewModel.Map(sites);
            }
            else // It is a site
            {
                viewModel.SiteId = SiteId;
                var site = siteService.Get(SiteId);
                viewModel.Map(site);
            }

            AddLog("Navigated to Map View", LogTypeEnum.Info);
            return View(viewModel);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult SiteSelected(Guid siteId)
        {
            IndexViewModel viewModel = new IndexViewModel();
            ISiteService siteService = new SiteService(KEUnitOfWork);
            var site = siteService.Get(siteId);
            viewModel.Map(site);

            var sites = siteService.GetsByCustomer(CustomerId);
            viewModel.Sites = SiteViewModel.Map(sites);
            viewModel.SiteId = siteId;
            return View("Index", viewModel);
        }

        #endregion Index       

        #region Json

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetPonds(Guid siteId)
        {
            using (var unitOfWork = KEUnitOfWork.Create(false))
            {
                var ponds = unitOfWork.PondRepository.GetsByCustomerAndSite(CustomerId, siteId);
                return Json(ponds, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetTanks(Guid siteId)
        {
            using (var unitOfWork = KEUnitOfWork.Create(false))
            {
                var tanks = unitOfWork.TankRepository.GetsByCustomerAndSite(CustomerId, siteId);
                return Json(tanks, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Json
    }
}