using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker;
using KarmicEnergy.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
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

        [HttpPost]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult Site(ListViewModel viewModel)
        {
            if (viewModel.SiteId != default(Guid))
            {
                var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, viewModel.SiteId);

                if (tanks.Any())
                {
                    foreach (var tank in tanks)
                    {
                        TankWithWaterVolume tankWithWaterVolume = new TankWithWaterVolume();

                        tankWithWaterVolume.TankId = tank.Id;
                        tankWithWaterVolume.UrlImageTankModel = tank.TankModel.ImageFilename;
                        tankWithWaterVolume.TankName = tank.Name;
                        tankWithWaterVolume.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                        if (KEUnitOfWork.SensorRepository.HasSensor(tank.Id) &&
                            KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterVolume))
                        {
                            var waterVolumesLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.WaterVolume);

                            if (waterVolumesLastEvent != null)
                            {
                                tankWithWaterVolume.WaterVolume = Decimal.Parse(waterVolumesLastEvent.Value);
                                tankWithWaterVolume.EventDate = waterVolumesLastEvent.EventDate;
                            }
                        }

                        viewModel.Tanks.Add(tankWithWaterVolume);
                    }
                }
            }

            LoadSites(CustomerId);
            return View("Index", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetsWaterVolume(Guid tankId)
        {
            TankViewModel viewModel = new TankViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensor(tankId) &&
                 KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.WaterVolume))
            {
                var waterInfos = KEUnitOfWork.SensorItemEventRepository.GetsByTankIdAndByItem(tankId, ItemEnum.WaterVolume, 5);

                if (waterInfos.Any())
                {
                    foreach (var wi in waterInfos)
                    {
                        viewModel.TankId = wi.SensorItem.Sensor.Tank.Id;
                        viewModel.TankName = wi.SensorItem.Sensor.Tank.Name;
                        viewModel.WaterVolumeCapacity = wi.SensorItem.Sensor.Tank.WaterVolumeCapacity;

                        WaterVolumeViewModel wvi = new WaterVolumeViewModel()
                        {
                            EventDate = wi.EventDate,
                            WaterVolume = Decimal.Parse(wi.Value)
                        };

                        viewModel.WaterVolumes.Add(wvi);
                    }
                }
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion FastTracker            
    }
}
