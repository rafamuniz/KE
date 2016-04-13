using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    public class FastTrackerController : BaseController
    {
        #region Index

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult Index()
        {
            LoadSites(CustomerId);
            return View();
        }

        #endregion Index

        #region FastTracker
        //[Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        //public ActionResult FastTracker()
        //{
        //    return View();
        //}

        //[Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        //public ActionResult GetsTankBySiteId(Guid siteId)
        //{
        //    IList<TankWithWaterVolume> tankWithWaterVolumes = null;
        //    var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, siteId);

        //    if (tanks.Any())
        //    {
        //        tankWithWaterVolumes = new List<TankWithWaterVolume>();

        //        foreach (var tank in tanks)
        //        {
        //            if (tank.Sensors.Any() && 
        //                KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterVolume))
        //            {
        //                TankWithWaterVolume tankWithWaterVolume = new TankWithWaterVolume();
        //                var tankWaterInfo = KEUnitOfWork.SensorItemEventRepository.GetTankWithWaterVolumeLastData(tank.Id);

        //                tankWithWaterVolume.TankId = tank.Id;
        //                tankWithWaterVolume.UrlImageTankModel = tank.TankModel.ImageFilename;
        //                tankWithWaterVolume.TankName = tank.Name;
        //                tankWithWaterVolume.WaterVolumeCapacity = tank.WaterVolumeCapacity;

        //                if (tankWaterInfo != null)
        //                {
        //                    tankWithWaterVolume.WaterVolume = Decimal.Parse(tankWaterInfo.Value);
        //                }

        //                var waterInfos = KEUnitOfWork.SensorItemEventRepository.GetTankWithWaterVolume(tank.Id, 5);

        //                if (waterInfos.Any())
        //                {
        //                    tankWithWaterVolume.WaterVolumeInfos = new List<WaterVolumeGraphViewModel>();

        //                    foreach (var wi in waterInfos)
        //                    {
        //                        WaterVolumeGraphViewModel wvi = new WaterVolumeGraphViewModel()
        //                        {
        //                            EventDate = wi.EventDate,
        //                            WaterVolume = Decimal.Parse(wi.Value)
        //                        };

        //                        tankWithWaterVolume.WaterVolumeInfos.Add(wvi);
        //                    }
        //                }

        //                tankWithWaterVolumes.Add(tankWithWaterVolume);
        //            }
        //        }
        //    }

        //    return Json(tankWithWaterVolumes, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult Site(ListViewModel viewModel)
        {
            IList<TankWithWaterVolume> tankWithWaterVolumes = new List<TankWithWaterVolume>();
            var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, viewModel.SiteId);

            if (tanks.Any())
            {
                foreach (var tank in tanks)
                {
                    if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterVolume))
                    {
                        TankWithWaterVolume tankWithWaterVolume = new TankWithWaterVolume();
                        var tankWaterInfo = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.WaterVolume);

                        tankWithWaterVolume.TankId = tank.Id;
                        tankWithWaterVolume.UrlImageTankModel = tank.TankModel.ImageFilename;
                        tankWithWaterVolume.TankName = tank.Name;
                        tankWithWaterVolume.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                        if (tankWaterInfo != null)
                        {
                            tankWithWaterVolume.WaterVolume = Decimal.Parse(tankWaterInfo.Value);
                            tankWithWaterVolume.EventDate = tankWaterInfo.EventDate;
                        }

                        var waterInfos = KEUnitOfWork.SensorItemEventRepository.GetTankWithWaterVolume(tank.Id, 5);

                        if (waterInfos.Any())
                        {
                            tankWithWaterVolume.WaterVolumeInfos = new List<WaterVolumeGraphViewModel>();

                            foreach (var wi in waterInfos)
                            {
                                WaterVolumeGraphViewModel wvi = new WaterVolumeGraphViewModel()
                                {
                                    EventDate = wi.EventDate,
                                    WaterVolume = Decimal.Parse(wi.Value)
                                };

                                tankWithWaterVolume.WaterVolumeInfos.Add(wvi);
                            }
                        }

                        tankWithWaterVolumes.Add(tankWithWaterVolume);
                        viewModel.Tanks.Add(tankWithWaterVolume);
                    }
                }
            }

            LoadSites(CustomerId);
            return View("Index", viewModel);
        }

        [HttpGet]
        public ActionResult GetWaterInfo(Guid tankId)
        {
            TankViewModel viewModel = null;
            var waterInfos = KEUnitOfWork.SensorItemEventRepository.GetTankWithWaterVolume(tankId, 5);
            List<WaterVolumeGraphViewModel> waterVolumeInfos = new List<WaterVolumeGraphViewModel>();

            if (waterInfos.Any())
            {
                viewModel = new TankViewModel();

                foreach (var wi in waterInfos)
                {
                    viewModel.TankId = wi.SensorItem.Sensor.Tank.Id;
                    viewModel.TankName = wi.SensorItem.Sensor.Tank.Name;
                    viewModel.WaterVolumeCapacity = wi.SensorItem.Sensor.Tank.WaterVolumeCapacity;

                    WaterVolumeGraphViewModel wvi = new WaterVolumeGraphViewModel()
                    {
                        EventDate = wi.EventDate,
                        WaterVolume = Decimal.Parse(wi.Value)
                    };

                    viewModel.WaterVolumeData.Add(wvi);
                }
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        //[Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        //public ActionResult GetTankHTML()
        //{
        //    return PartialView("_Tank");
        //}

        #endregion FastTracker

        #region Fills

        public ActionResult FillTank(Guid siteId)
        {
            var tanks = LoadTanks(CustomerId, siteId);
            return Json(tanks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTankInfo(Guid tankId)
        {
            var tankInfo = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tankId, ItemEnum.WaterVolume);
            return Json(tankInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTankWaterVolume(Guid tankId)
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTankTemperature(Guid tankId)
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTankSensorVoltage(Guid tankId)
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        #endregion Fills
    }
}
