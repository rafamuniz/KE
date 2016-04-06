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

                    tankWithWaterVolumes.Add(tankWithWaterVolume);
                }
            }

            return Json(tankWithWaterVolumes, JsonRequestBehavior.AllowGet);
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
