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

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Index()
        {
            ListViewModel viewModel = new ListViewModel();

            if (!IsSite)
            {
                LoadSites(CustomerId);
            }
            else
            {
                viewModel.SiteId = SiteId;
                LoadTanksWithWaterVolume(viewModel);
            }

            return View(viewModel);
        }

        #endregion Index

        #region FastTracker

        [HttpPost]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Site(ListViewModel viewModel)
        {
            if (viewModel.SiteId != default(Guid))
            {
                LoadTanksWithWaterVolume(viewModel);
            }

            LoadSites(CustomerId);
            return View("Index", viewModel);
        }

        private void LoadTanksWithWaterVolume(ListViewModel viewModel)
        {
            var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, viewModel.SiteId);

            if (tanks.Any())
            {
                foreach (var tank in tanks)
                {
                    TankWithWaterVolume tankWithWaterVolume = new TankWithWaterVolume();

                    tankWithWaterVolume.TankId = tank.Id;
                    tankWithWaterVolume.TankName = tank.Name;

                    tankWithWaterVolume.TankModelId = tank.TankModelId;
                    tankWithWaterVolume.TankModelImage = tank.TankModel.ImageFilename;

                    tankWithWaterVolume.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                    if (KEUnitOfWork.SensorRepository.HasSensorTank(tank.Id) &&
                        KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Range))
                    {
                        var waterVolumesLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.Range);

                        if (waterVolumesLastEvent != null)
                        {
                            tankWithWaterVolume.WaterVolume = Decimal.Parse(waterVolumesLastEvent.CalculatedValue);
                            tankWithWaterVolume.EventDate = waterVolumesLastEvent.EventDate;
                        }
                    }

                    viewModel.Tanks.Add(tankWithWaterVolume);
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsWaterVolume(Guid tankId)
        {
            TankViewModel viewModel = new TankViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensorTank(tankId) &&
                 KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.Range))
            {
                var waterInfos = KEUnitOfWork.SensorItemEventRepository.GetsByTankIdAndByItem(tankId, ItemEnum.Range, 5);

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
                            WaterVolume = Decimal.Parse(wi.CalculatedValue)
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
