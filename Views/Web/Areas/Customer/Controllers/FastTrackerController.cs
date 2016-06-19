using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
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
                LoadSites();
            }
            else // It is a site
            {
                viewModel.SiteId = SiteId;
                LoadTanks(viewModel);
                LoadTriggersBySite(viewModel);
                LoadWaterQualityBySite(viewModel);
                LoadFlowMeters(viewModel);
            }

            AddLog("Navigate to Fast Tracker View", LogTypeEnum.Info);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult SiteSiteSelected(ListViewModel viewModel)
        {
            if (viewModel.SiteId.HasValue && viewModel.SiteId != default(Guid))
            {
                LoadSite(viewModel);
                LoadTanks(viewModel);
                LoadTriggersBySite(viewModel);
                LoadWaterQualityBySite(viewModel);
                LoadFlowMeters(viewModel);
            }

            LoadSites();
            return View("Index", viewModel);
        }

        private void LoadSite(ListViewModel viewModel)
        {
            var site = KEUnitOfWork.SiteRepository.Get(viewModel.SiteId.Value);
            viewModel.Longitude = site.Longitude;
            viewModel.Latitude = site.Latitude;
        }

        private void LoadTanks(ListViewModel viewModel)
        {
            var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, viewModel.SiteId.Value);

            if (tanks.Any())
            {
                foreach (var tank in tanks)
                {
                    TankViewModel tankViewModel = new TankViewModel();

                    tankViewModel.Id = tank.Id;
                    tankViewModel.Name = tank.Name;

                    tankViewModel.TankModelId = tank.TankModelId;
                    tankViewModel.TankModelImage = tank.TankModel.ImageFilename;

                    tankViewModel.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                    if (KEUnitOfWork.SensorRepository.HasSensorTank(tank.Id))
                    {
                        // Water Volume
                        if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Range))
                        {
                            var waterVolume = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.Range);
                            if (waterVolume != null)
                            {
                                tankViewModel.WaterVolumeLastValue = Decimal.Parse(waterVolume.CalculatedValue);
                                tankViewModel.WaterVolumeLastEventDate = waterVolume.EventDate;
                            }
                        }
                    }

                    // Alarms By Tank
                    var alarms = KEUnitOfWork.AlarmRepository.GetsActiveByTank(tank.Id);
                    tankViewModel.Alarms = AlarmViewModel.Map(alarms);
                    viewModel.Tanks.Add(tankViewModel);
                }
            }
        }

        private void LoadTriggersBySite(ListViewModel viewModel)
        {
            var triggers = KEUnitOfWork.TriggerRepository.GetsBySite(viewModel.SiteId.Value, 5);

            foreach (var trigger in triggers)
            {
                TriggerViewModel triggerViewModel = new TriggerViewModel();
                var alarm = KEUnitOfWork.AlarmRepository.GetActiveByTrigger(trigger.Id);
                triggerViewModel = TriggerViewModel.Map(trigger);
                if (alarm != null)
                {
                    triggerViewModel.HasAlarm = true;
                }

                viewModel.Triggers.Add(triggerViewModel);
            }
        }

        private void LoadWaterQualityBySite(ListViewModel viewModel)
        {
            WaterQualityViewModel waterQualityViewModel = new WaterQualityViewModel();

            if (KEUnitOfWork.SensorItemRepository.HasSiteSensorItem(viewModel.SiteId.Value, ItemEnum.AmbientTemperature))
            {
                var ambient = KEUnitOfWork.SensorItemEventRepository.GetLastEventBySiteAndItem(viewModel.SiteId.Value, ItemEnum.AmbientTemperature);
                if (ambient != null)
                {
                    waterQualityViewModel.TemperatureAmbientLastEventValue = Decimal.Parse(ambient.Value);
                    waterQualityViewModel.TemperatureAmbientLastEventDate = ambient.EventDate;
                }
            }

            if (KEUnitOfWork.SensorItemRepository.HasSiteSensorItem(viewModel.SiteId.Value, ItemEnum.PH))
            {
                var ph = KEUnitOfWork.SensorItemEventRepository.GetLastEventBySiteAndItem(viewModel.SiteId.Value, ItemEnum.PH);
                if (ph != null)
                {
                    waterQualityViewModel.PHLastEventValue = Decimal.Parse(ph.Value);
                    waterQualityViewModel.PHLastEventDate = ph.EventDate;
                }
            }

            //if (KEUnitOfWork.SensorItemRepository.HasSiteSensorItem(viewModel.SiteId.Value, ItemEnum.PH))
            //{
            //    var ph = KEUnitOfWork.SensorItemEventRepository.GetLastEventBySiteAndItem(viewModel.SiteId.Value, ItemEnum.PH);
            //    if (ph != null)
            //    {
            //        waterQualityViewModel.PHLastEventValue = Decimal.Parse(ph.Value);
            //        waterQualityViewModel.PHLastEventDate = ph.EventDate;
            //    }
            //}

            viewModel.WaterQuality = waterQualityViewModel;
        }

        private void LoadFlowMeters(ListViewModel viewModel)
        {
            List<FlowMeterViewModel> viewModels = new List<FlowMeterViewModel>();

            var flowMeters = KEUnitOfWork.SensorRepository.GetsBySiteAndSensorType(viewModel.SiteId.Value, SensorTypeEnum.FlowMeter);

            if (flowMeters.Any())
            {
                foreach (var flowMeter in flowMeters)
                {
                    FlowMeterViewModel flowMeterViewModel = new FlowMeterViewModel();

                    flowMeterViewModel.SensorId = flowMeter.Id;

                    if (KEUnitOfWork.SensorItemRepository.HasSensorSensorItem(flowMeter.Id, ItemEnum.RateFlow))
                    {
                        var sensorItem = KEUnitOfWork.SensorItemRepository.GetsBySensorAndItem(flowMeter.Id, ItemEnum.RateFlow);
                        var rateFlow = KEUnitOfWork.SensorItemEventRepository.GetLastEventBySensorItem(sensorItem.Id);
                        if (rateFlow != null)
                        {
                            flowMeterViewModel.RateFlow = Decimal.Parse(rateFlow.Value);
                            flowMeterViewModel.RateFlowLastMeasurement = rateFlow.EventDate;
                        }
                    }

                    if (KEUnitOfWork.SensorItemRepository.HasSensorSensorItem(flowMeter.Id, ItemEnum.Totalizer))
                    {
                        var sensorItem = KEUnitOfWork.SensorItemRepository.GetsBySensorAndItem(flowMeter.Id, ItemEnum.Totalizer);
                        var totalizer = KEUnitOfWork.SensorItemEventRepository.GetLastEventBySensorItem(sensorItem.Id);
                        if (totalizer != null)
                        {
                            flowMeterViewModel.Totalizer = Int32.Parse(totalizer.Value);
                            flowMeterViewModel.TotalizerLastMeasurement = totalizer.EventDate;
                        }
                    }

                    viewModel.FlowMeters.Add(flowMeterViewModel);
                }
            }
        }

        #endregion Index

        #region Index

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult FastTracker()
        {
            ListViewModel viewModel = new ListViewModel();

            if (!IsSite)
            {
                LoadSites();
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
        public ActionResult FastTrackerSiteSelected(ListViewModel viewModel)
        {
            if (viewModel.SiteId != default(Guid) && viewModel.SiteId != default(Guid))
            {
                LoadTanksWithWaterVolume(viewModel);
            }

            LoadSites();
            return View("Index", viewModel);
        }

        private void LoadTanksWithWaterVolume(ListViewModel viewModel)
        {
            var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, viewModel.SiteId.Value);

            if (tanks.Any())
            {
                foreach (var tank in tanks)
                {
                    TankViewModel tankViewModel = new TankViewModel();

                    tankViewModel.Id = tank.Id;
                    tankViewModel.Name = tank.Name;

                    tankViewModel.TankModelId = tank.TankModelId;
                    tankViewModel.TankModelImage = tank.TankModel.ImageFilename;

                    tankViewModel.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                    if (KEUnitOfWork.SensorRepository.HasSensorTank(tank.Id) &&
                        KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Range))
                    {
                        var waterVolumesLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.Range);

                        if (waterVolumesLastEvent != null)
                        {
                            tankViewModel.WaterVolumeLastValue = Decimal.Parse(waterVolumesLastEvent.CalculatedValue);
                            tankViewModel.WaterVolumeLastEventDate = waterVolumesLastEvent.EventDate;
                        }
                    }

                    viewModel.Tanks.Add(tankViewModel);
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
                        viewModel.Id = wi.SensorItem.Sensor.Tank.Id;
                        viewModel.Name = wi.SensorItem.Sensor.Tank.Name;
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
