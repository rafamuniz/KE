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
            ISiteService siteService = new SiteService(KEUnitOfWork.Create(false));

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
            ISiteService siteService = new SiteService(KEUnitOfWork.Create(false));
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
            IList<PondViewModel> viewModels = new List<PondViewModel>();
            PondService pondService = new PondService(KEUnitOfWork.Create(false));
            var ponds = pondService.GetsBySite(siteId);
            viewModels = PondViewModel.Map(ponds);
            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetTanks(Guid siteId)
        {
            IList<TankViewModel> viewModels = new List<TankViewModel>();
            TankService tankService = new TankService(KEUnitOfWork.Create(false));
            var tanks = tankService.GetsBySite(siteId);
            viewModels = TankViewModel.Map(tanks);
            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetTanksWithInfo(Guid tankId)
        {
            TankViewModel viewModel = new TankViewModel();

            try
            {
                var unitOfWork = KEUnitOfWork.Create(false);
                TankService tankService = new TankService(unitOfWork);
                var tank = tankService.Get(tankId, "TankModel");
                viewModel = TankViewModel.Map(tank);

                SensorService sensorService = new SensorService(unitOfWork);
                SensorItemService sensorItemService = new SensorItemService(unitOfWork);
                SensorItemEventService sensorItemEventService = new SensorItemEventService(unitOfWork);

                if (sensorService.HasSensorTank(viewModel.Id) &&
                    sensorItemService.HasTankSensorItem(viewModel.Id, ItemEnum.WaterVolume))
                {
                    var waterVolumesLastEvent = sensorItemEventService.GetLastEventByTankAndItem(viewModel.Id, ItemEnum.WaterVolume);

                    if (waterVolumesLastEvent != null)
                    {
                        viewModel.WaterVolumeLastValue = Decimal.Parse(waterVolumesLastEvent.Value);
                        viewModel.WaterVolumeLastEventDate = waterVolumesLastEvent.EventDate;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetSensors(Guid siteId)
        {
            using (var unitOfWork = KEUnitOfWork.Create(false))
            {
                var sensors = unitOfWork.SensorRepository.GetsByCustomerAndSite(CustomerId, siteId);
                return Json(sensors, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Json
    }
}