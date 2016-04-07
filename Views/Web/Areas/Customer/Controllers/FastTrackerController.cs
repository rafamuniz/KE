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
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult FastTracker()
        {
            return View();
        }

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetsTankBySiteId(Guid siteId)
        {
            IList<TankWithWaterVolume> tankWithWaterVolumes = null;
            var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, siteId);

            if (tanks.Any())
            {
                tankWithWaterVolumes = new List<TankWithWaterVolume>();

                foreach (var t in tanks)
                {
                    if (t.Sensors.Any())
                    {
                        TankWithWaterVolume tankWithWaterVolume = new TankWithWaterVolume();
                        var tankWaterInfo = KEUnitOfWork.SensorItemEventRepository.GetTankWithWaterVolumeLastData(t.Id);

                        tankWithWaterVolume.TankId = t.Id;
                        tankWithWaterVolume.UrlImageTankModel = t.TankModel.ImageFilename;
                        tankWithWaterVolume.TankName = t.Name;
                        tankWithWaterVolume.WaterVolumeCapacity = t.WaterVolumeCapacity;

                        if (tankWaterInfo != null)
                        {
                            tankWithWaterVolume.WaterVolume = Decimal.Parse(tankWaterInfo.Value);
                        }

                        var waterInfos = KEUnitOfWork.SensorItemEventRepository.GetTankWithWaterVolume(t.Id, 5);

                        if (waterInfos.Any())
                        {
                            tankWithWaterVolume.WaterVolumeInfos = new List<WaterVolumeInfo>();

                            foreach (var wi in waterInfos)
                            {
                                WaterVolumeInfo wvi = new WaterVolumeInfo()
                                {
                                    EventDate = wi.EventDate,
                                    WaterVolume = Decimal.Parse(wi.Value)
                                };

                                tankWithWaterVolume.WaterVolumeInfos.Add(wvi);
                            }
                        }

                        tankWithWaterVolumes.Add(tankWithWaterVolume);
                    }
                }
            }

            return Json(tankWithWaterVolumes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SelectedSite(ListViewModel viewModel)
        {
            IList<TankWithWaterVolume> tankWithWaterVolumes = null;
            var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, viewModel.SiteId);

            if (tanks.Any())
            {
                tankWithWaterVolumes = new List<TankWithWaterVolume>();

                foreach (var t in tanks)
                {
                    TankWithWaterVolume tankWithWaterVolume = new TankWithWaterVolume();
                    var tankWaterInfo = KEUnitOfWork.SensorItemEventRepository.GetTankWithWaterVolumeLastData(t.Id);

                    tankWithWaterVolume.TankId = t.Id;
                    tankWithWaterVolume.UrlImageTankModel = t.TankModel.ImageFilename;
                    tankWithWaterVolume.TankName = t.Name;
                    tankWithWaterVolume.WaterVolumeCapacity = t.WaterVolumeCapacity;
                    tankWithWaterVolume.EventDate = tankWaterInfo.EventDate;
                    tankWithWaterVolume.WaterVolume = Decimal.Parse(tankWaterInfo.Value);

                    if (tankWaterInfo != null)
                    {
                        tankWithWaterVolume.WaterVolume = Decimal.Parse(tankWaterInfo.Value);
                    }

                    var waterInfos = KEUnitOfWork.SensorItemEventRepository.GetTankWithWaterVolume(t.Id, 5);

                    if (waterInfos.Any())
                    {
                        tankWithWaterVolume.WaterVolumeInfos = new List<WaterVolumeInfo>();

                        foreach (var wi in waterInfos)
                        {
                            WaterVolumeInfo wvi = new WaterVolumeInfo()
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

            LoadSites(CustomerId);
            return View("Index", viewModel);
        }

        [HttpPost]
        public ActionResult GetWaterInfo(Guid tankId)
        {
            var waterInfos = KEUnitOfWork.SensorItemEventRepository.GetTankWithWaterVolume(tankId, 5);
            List<WaterVolumeInfo> waterVolumeInfos = new List<WaterVolumeInfo>();

            if (waterInfos.Any())
            {
                foreach (var wi in waterInfos)
                {
                    WaterVolumeInfo wvi = new WaterVolumeInfo()
                    {
                        EventDate = wi.EventDate,
                        WaterVolume = Decimal.Parse(wi.Value)
                    };

                    waterVolumeInfos.Add(wvi);
                }
            }

            return Json(waterInfos, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetTankHTML()
        {
            return PartialView("_Tank");
        }

        #endregion FastTracker

        #region Fills

        public ActionResult FillTank(Guid siteId)
        {
            var tanks = LoadTanks(CustomerId, siteId);
            return Json(tanks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTankInfo(Guid tankId)
        {
            var tankInfo = KEUnitOfWork.SensorItemEventRepository.GetTankWithWaterVolumeLastData(tankId);
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
