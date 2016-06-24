using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard;
using KarmicEnergy.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        #region Tank

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Tanks()
        {
            TankDashboardViewModel viewModel = new TankDashboardViewModel();

            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            AddLog("Navigated to Tank Dashboard View", LogTypeEnum.Info);
            return View(viewModel);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult SiteDashboard(Guid tankId)
        {
            TankDashboardViewModel viewModel = new TankDashboardViewModel();

            viewModel.TankId = tankId;

            return View("Gauge", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult SiteTankSelected(TankDashboardViewModel viewModel)
        {
            if (viewModel.SiteId.HasValue && viewModel.SiteId.Value != default(Guid))
            {
                #region Tanks
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
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterVolume))
                            {
                                var waterVolumesLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.WaterVolume);

                                if (waterVolumesLastEvent != null)
                                {
                                    tankViewModel.WaterVolumeLastValue = Decimal.Parse(waterVolumesLastEvent.Value);
                                    tankViewModel.WaterVolumeLastEventDate = waterVolumesLastEvent.EventDate;
                                }
                            }

                            // Water Temperature
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterTemperature))
                            {
                                var waterTemperatureLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.WaterTemperature);

                                if (waterTemperatureLastEvent != null)
                                {
                                    tankViewModel.WaterTemperatureLastEventValue = Decimal.Parse(waterTemperatureLastEvent.Value);
                                    tankViewModel.WaterTemperatureLastEventDate = waterTemperatureLastEvent.EventDate;
                                }
                            }

                            // Weather Temperature
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.AmbientTemperature))
                            {
                                var ambientTemperatureLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.AmbientTemperature);

                                if (ambientTemperatureLastEvent != null)
                                {
                                    tankViewModel.AmbientTemperatureLastEventValue = Decimal.Parse(ambientTemperatureLastEvent.Value);
                                    tankViewModel.AmbientTemperatureLastEventDate = ambientTemperatureLastEvent.EventDate;
                                }
                            }
                        }

                        // Alarms
                        var alarms = KEUnitOfWork.AlarmRepository.GetTotalOpenByTankId(tank.Id);
                        tankViewModel.Alarms = alarms;

                        viewModel.Tanks.Add(tankViewModel);
                    }
                }
                #endregion Tanks    
            }

            LoadSites();
            return View("Tanks", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsWaterVolume(Guid tankId)
        {
            TankViewModel viewModel = new TankViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensorTank(tankId) &&
                KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.WaterVolume))
            {
                var waterInfos = KEUnitOfWork.SensorItemEventRepository.GetsByTankIdAndByItem(tankId, ItemEnum.WaterVolume, 5);

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
                            WaterVolume = Decimal.Parse(wi.Value)
                        };

                        viewModel.WaterVolumes.Add(wvi);
                    }
                }
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion Tank

        #region Site

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Site()
        {
            SiteDashboardViewModel viewModel = new SiteDashboardViewModel();

            if (!IsSite)
            {
                LoadSites();
            }
            else // It is a site
            {
                viewModel.SiteId = SiteId;
                LoadTanks(viewModel);
                LoadAlarms(viewModel);
                LoadFlowMeters(viewModel);
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult SiteSiteSelected(SiteDashboardViewModel viewModel)
        {
            if (viewModel.SiteId.HasValue && viewModel.SiteId != default(Guid))
            {
                LoadTanks(viewModel);
                LoadAlarms(viewModel);
                LoadFlowMeters(viewModel);
            }

            LoadSites();
            return View("Site", viewModel);
        }

        private void LoadTanks(SiteDashboardViewModel viewModel)
        {
            var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, viewModel.SiteId.Value);

            if (tanks.Any())
            {
                foreach (var tank in tanks)
                {
                    TankViewModel tankViewModel = new TankViewModel();

                    tankViewModel.Id = tank.Id;
                    tankViewModel.Name = tank.Name;
                    tankViewModel.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                    if (KEUnitOfWork.SensorRepository.HasSensorTank(tank.Id))
                    {
                        // Water Volume
                        if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterVolume))
                        {
                            var waterVolume = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.WaterVolume);
                            if (waterVolume != null)
                            {
                                tankViewModel.WaterVolumeLastValue = Decimal.Parse(waterVolume.Value);
                                tankViewModel.WaterVolumeLastEventDate = waterVolume.EventDate;
                            }
                        }
                    }

                    // Alarms
                    var alarms = KEUnitOfWork.AlarmRepository.GetsByTank(tank.Id);
                    tankViewModel.HasAlarmHigh = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Critical).Any();
                    tankViewModel.HasAlarmMedium = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Medium).Any();
                    tankViewModel.HasAlarmLow = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Low).Any();
                    tankViewModel.Alarms = alarms.Count();

                    viewModel.Tanks.Add(tankViewModel);
                }
            }
        }

        private void LoadAlarms(SiteDashboardViewModel viewModel)
        {
            AlarmViewModel alarmViewModel = new AlarmViewModel();
        }

        private void LoadFlowMeters(SiteDashboardViewModel viewModel)
        {
            FlowMeterViewModel flowMeterViewModel = new FlowMeterViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensorSite(viewModel.SiteId.Value) &&
                KEUnitOfWork.SensorItemRepository.HasSiteSensorItem(viewModel.SiteId.Value, ItemEnum.RateFlow) &&
                KEUnitOfWork.SensorItemRepository.HasSiteSensorItem(viewModel.SiteId.Value, ItemEnum.Totalizer))
            {
                var rateFlow = KEUnitOfWork.SensorItemEventRepository.GetLastEventBySiteAndItem(viewModel.SiteId.Value, ItemEnum.RateFlow);
                if (rateFlow != null)
                {
                    flowMeterViewModel.RateFlow = Decimal.Parse(rateFlow.Value);
                    flowMeterViewModel.RateFlowLastMeasurement = rateFlow.EventDate;
                }

                var totalizer = KEUnitOfWork.SensorItemEventRepository.GetLastEventBySiteAndItem(viewModel.SiteId.Value, ItemEnum.Totalizer);
                if (totalizer != null)
                {
                    flowMeterViewModel.Totalizer = Int32.Parse(totalizer.Value);
                    flowMeterViewModel.TotalizerLastMeasurement = totalizer.EventDate;
                }

                viewModel.FlowMeters.Add(flowMeterViewModel);
            }
        }

        //[HttpGet]
        //[Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        //public ActionResult GetLastFlowMeter(Guid siteId)
        //{
        //    FlowMeterViewModel viewModel = new FlowMeterViewModel();

        //    if (KEUnitOfWork.SensorRepository.HasSensorSite(siteId))
        //    {
        //        if (KEUnitOfWork.SensorItemRepository.HasSiteSensorItem(siteId, ItemEnum.RateFlow))
        //        {
        //            var rateFlow = KEUnitOfWork.SensorItemEventRepository.GetLastEventBySiteAndItem(siteId, ItemEnum.RateFlow);

        //            if (rateFlow != null)
        //            {
        //                viewModel.RateFlow = Decimal.Parse(rateFlow.Value);
        //                viewModel.RateFlowLastMeasurement = rateFlow.EventDate;
        //            }
        //        }

        //        if (KEUnitOfWork.SensorItemRepository.HasSiteSensorItem(siteId, ItemEnum.Totalizer))
        //        {
        //            var totalizer = KEUnitOfWork.SensorItemEventRepository.GetLastEventBySiteAndItem(siteId, ItemEnum.Totalizer);

        //            if (totalizer != null)
        //            {
        //                viewModel.Totalizer = Int32.Parse(totalizer.Value);
        //                viewModel.TotalizerLastMeasurement = totalizer.EventDate;
        //            }
        //        }
        //    }

        //    return Json(viewModel, JsonRequestBehavior.AllowGet);
        //}

        #endregion Site

        #region Gauge

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Gauge(Guid tankId)
        {
            var tank = KEUnitOfWork.TankRepository.Get(tankId);

            GaugeViewModel viewModel = new GaugeViewModel()
            {
                TankId = tank.Id,
                TankName = tank.Name,
                WaterVolumeCapacity = tank.WaterVolumeCapacity
            };

            // Water Volume
            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterVolume))
            {
                var waterVolume = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.WaterVolume);
                if (waterVolume != null)
                {
                    viewModel.WaterVolume = Decimal.Parse(waterVolume.Value);
                    viewModel.WaterVolumeLastMeasurement = waterVolume.EventDate;
                }
            }

            // Trigger
            var triggers = KEUnitOfWork.TriggerRepository.GetsByTank(tank.Id);
            viewModel.Triggers = TriggerViewModel.Map(triggers);

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetLastFlowMeter(Guid tankId)
        {
            FlowMeterViewModel viewModel = new FlowMeterViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensorTank(tankId))
            {
                if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.RateFlow))
                {
                    var rateFlow = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tankId, ItemEnum.RateFlow);

                    if (rateFlow != null)
                    {
                        viewModel.TankId = rateFlow.SensorItem.Sensor.Tank.Id;
                        viewModel.TankName = rateFlow.SensorItem.Sensor.Tank.Name;

                        viewModel.RateFlow = Decimal.Parse(rateFlow.Value);
                    }
                }

                if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.Totalizer))
                {
                    var totalizer = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tankId, ItemEnum.Totalizer);

                    if (totalizer != null)
                    {
                        viewModel.TankId = totalizer.SensorItem.Sensor.Tank.Id;
                        viewModel.TankName = totalizer.SensorItem.Sensor.Tank.Name;

                        viewModel.Totalizer = Int32.Parse(totalizer.Value);
                    }
                }
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsWaterVolumeByOption(Guid tankId, Int64 option)
        {
            TankViewModel viewModel = new TankViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensorTank(tankId) &&
                KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.WaterVolume))
            {
                var waterInfos = KEUnitOfWork.SensorItemEventRepository.GetsByTankIdAndByItem(tankId, ItemEnum.WaterVolume, 5);

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
                            WaterVolume = Decimal.Parse(wi.Value)
                        };

                        viewModel.WaterVolumes.Add(wvi);
                    }
                }
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsVoltage(Guid tankId)
        {
            VoltageViewModel viewModel = new VoltageViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensorTank(tankId) &&
                KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.Voltage))
            {
                var voltages = KEUnitOfWork.SensorItemEventRepository.GetsByTankIdAndByItem(tankId, ItemEnum.Voltage, 5);

                if (voltages.Any())
                {
                    foreach (var volt in voltages)
                    {
                        EventViewModel evm = new EventViewModel()
                        {
                            EventDate = volt.EventDate,
                            Value = volt.Value
                        };

                        viewModel.Voltages.Add(evm);
                    }
                }
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsWaterAndWeatherTemperature(Guid tankId)
        {
            TemperatureViewModel viewModel = new TemperatureViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensorTank(tankId))
            {
                if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.WaterTemperature))
                {
                    var waterTemperatures = KEUnitOfWork.SensorItemEventRepository.GetsByTankIdAndByItem(tankId, ItemEnum.WaterTemperature, 5);

                    if (waterTemperatures.Any())
                    {
                        foreach (var wt in waterTemperatures)
                        {
                            EventViewModel evm = new EventViewModel()
                            {
                                EventDate = wt.EventDate,
                                Value = wt.Value
                            };

                            viewModel.WaterTempertatures.Add(evm);
                        }
                    }
                }

                // Weather Temperature
                if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.AmbientTemperature))
                {
                    var ambientTemperatures = KEUnitOfWork.SensorItemEventRepository.GetsByTankIdAndByItem(tankId, ItemEnum.AmbientTemperature, 5);

                    if (ambientTemperatures.Any())
                    {
                        foreach (var at in ambientTemperatures)
                        {
                            EventViewModel evm = new EventViewModel()
                            {
                                EventDate = at.EventDate,
                                Value = at.Value
                            };

                            viewModel.WeatherTempertatures.Add(evm);
                        }
                    }
                }
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion Gauge

        #region Trigger

        #region Delete

        //
        // GET: /Trigger/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult DeleteTrigger(Guid id, Guid tankId)
        {
            var trigger = KEUnitOfWork.TriggerRepository.Get(id);

            if (trigger == null)
            {
                AddErrors("Trigger does not exist");
                return View();
            }

            trigger.DeletedDate = DateTime.UtcNow;
            KEUnitOfWork.TriggerRepository.Update(trigger);
            KEUnitOfWork.Complete();

            return RedirectToAction("Gauge", "Tank", new { TankId = tankId });
        }

        #endregion Delete

        #endregion Trigger
    }
}