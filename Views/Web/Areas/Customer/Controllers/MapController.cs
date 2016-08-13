using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services;
using KarmicEnergy.Core.Services.Interface;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Map;
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
                viewModel.Sites = SiteViewModel.Map(sites.ToList());
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
            viewModel.Sites = SiteViewModel.Map(sites.ToList());
            viewModel.SiteId = siteId;
            return View("Index", viewModel);
        }

        #endregion Index       

        #region Json

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetSite(Guid siteId)
        {
            ViewModels.Map.SiteViewModel viewModel = new ViewModels.Map.SiteViewModel();
            SiteService siteService = new SiteService(KEUnitOfWork.Create(false));
            var site = siteService.Get(siteId);
            viewModel = ViewModels.Map.SiteViewModel.Map(site);
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetSiteWithInfo(Guid siteId)
        {
            ViewModels.Map.SiteViewModel viewModel = new ViewModels.Map.SiteViewModel();
            var unitOfWork = KEUnitOfWork.Create(false);
            SiteService siteService = new SiteService(unitOfWork);
            var sensor = siteService.Get(siteId);
            viewModel = ViewModels.Map.SiteViewModel.Map(sensor);

            SensorService sensorService = new SensorService(unitOfWork);
            SensorItemService sensorItemService = new SensorItemService(unitOfWork);
            SensorItemEventService sensorItemEventService = new SensorItemEventService(unitOfWork);

            //if (sensorService.HasSensorTank(viewModel.Id) &&
            //    sensorItemService.HasTankSensorItem(viewModel.Id, ItemEnum.WaterVolume))
            //{
            //    var waterVolumesLastEvent = sensorItemEventService.GetLastEventByTankAndItem(viewModel.Id, ItemEnum.WaterVolume);

            //    if (waterVolumesLastEvent != null)
            //    {
            //        viewModel.WaterVolumeLastValue = Decimal.Parse(waterVolumesLastEvent.Value);
            //        viewModel.WaterVolumeLastEventDate = waterVolumesLastEvent.EventDate;
            //    }
            //}

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsPond(Guid siteId)
        {
            IList<PondViewModel> viewModels = new List<PondViewModel>();
            PondService pondService = new PondService(KEUnitOfWork.Create(false));
            var ponds = pondService.GetsBySite(siteId);
            viewModels = PondViewModel.Map(ponds);
            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetPondWithInfo(Guid pondId)
        {
            PondViewModel viewModel = new PondViewModel();

            var unitOfWork = KEUnitOfWork.Create(false);
            PondService pondService = new PondService(unitOfWork);
            var sensor = pondService.Get(pondId);
            viewModel = PondViewModel.Map(sensor);

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

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsTank(Guid siteId)
        {
            IList<TankViewModel> viewModels = new List<TankViewModel>();
            TankService tankService = new TankService(KEUnitOfWork.Create(false));
            var tanks = tankService.GetsBySite(siteId);
            viewModels = TankViewModel.Map(tanks);
            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetTankWithInfo(Guid tankId)
        {
            TankViewModel viewModel = new TankViewModel();

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

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsSensor(Guid siteId)
        {
            IList<SensorViewModel> viewModels = new List<SensorViewModel>();
            SensorService sensorService = new SensorService(KEUnitOfWork.Create(false));
            var sensors = sensorService.GetsBySite(siteId);
            viewModels = SensorViewModel.Map(sensors);
            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetSensorWithInfo(Guid sensorId)
        {
            SensorViewModel viewModel = new SensorViewModel();

            var unitOfWork = KEUnitOfWork.Create(false);
            SensorService sensorService = new SensorService(unitOfWork);
            var sensor = sensorService.Get(sensorId);
            viewModel = SensorViewModel.Map(sensor);

            SensorItemService sensorItemService = new SensorItemService(unitOfWork);
            SensorItemEventService sensorItemEventService = new SensorItemEventService(unitOfWork);

            //if (sensorService.HasSensorTank(viewModel.Id) &&
            //    sensorItemService.HasTankSensorItem(viewModel.Id, ItemEnum.WaterVolume))
            //{
            //    var waterVolumesLastEvent = sensorItemEventService.GetLastEventByTankAndItem(viewModel.Id, ItemEnum.WaterVolume);

            //    if (waterVolumesLastEvent != null)
            //    {
            //        viewModel.WaterVolumeLastValue = Decimal.Parse(waterVolumesLastEvent.Value);
            //        viewModel.WaterVolumeLastEventDate = waterVolumesLastEvent.EventDate;
            //    }
            //}

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion Json
    }
}