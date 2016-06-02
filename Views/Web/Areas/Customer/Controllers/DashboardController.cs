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

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult Tank()
        {
            TankDashboardViewModel viewModel = new TankDashboardViewModel();

            if (!IsSite)
            {
                LoadSites(CustomerId);
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
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
                        tankViewModel.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                        if (KEUnitOfWork.SensorRepository.HasSensorTank(tank.Id))
                        {
                            // Water Volume
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Range))
                            {
                                var waterVolumesLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.Range);

                                if (waterVolumesLastEvent != null)
                                {
                                    tankViewModel.WaterVolumeLast = Decimal.Parse(waterVolumesLastEvent.CalculatedValue);
                                    tankViewModel.WaterVolumeLastMeasurement = waterVolumesLastEvent.EventDate;
                                }
                            }

                            // Water Temperature
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterTemperature))
                            {
                                var waterTemperatureLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.WaterTemperature);

                                if (waterTemperatureLastEvent != null)
                                {
                                    tankViewModel.WaterTemperature = Decimal.Parse(waterTemperatureLastEvent.Value);
                                    tankViewModel.WaterTemperatureEventDate = waterTemperatureLastEvent.EventDate;
                                }
                            }

                            // Weather Temperature
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.AmbientTemperature))
                            {
                                var ambientTemperatureLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.AmbientTemperature);

                                if (ambientTemperatureLastEvent != null)
                                {
                                    tankViewModel.WeatherTemperature = Decimal.Parse(ambientTemperatureLastEvent.Value);
                                    tankViewModel.WeatherTemperatureEventDate = ambientTemperatureLastEvent.EventDate;
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

            LoadSites(CustomerId);
            return View("Tank", viewModel);
        }

        #endregion Tank

        #region Site

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult Site()
        {
            SiteDashboardViewModel viewModel = new SiteDashboardViewModel();

            if (!IsSite)
            {
                LoadSites(CustomerId);
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
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult SiteSiteSelected(SiteDashboardViewModel viewModel)
        {
            if (viewModel.SiteId.HasValue && viewModel.SiteId != default(Guid))
            {
                LoadTanks(viewModel);
                LoadAlarms(viewModel);
                LoadFlowMeters(viewModel);
            }

            LoadSites(CustomerId);
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
                        if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Range))
                        {
                            var waterVolume = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.Range);
                            if (waterVolume != null)
                            {
                                tankViewModel.WaterVolumeLast = Decimal.Parse(waterVolume.CalculatedValue);
                                tankViewModel.WaterVolumeLastMeasurement = waterVolume.EventDate;
                            }
                        }
                    }

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

        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetLastFlowMeter(Guid siteId)
        {
            FlowMeterViewModel viewModel = new FlowMeterViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensorSite(siteId))
            {
                if (KEUnitOfWork.SensorItemRepository.HasSiteSensorItem(siteId, ItemEnum.RateFlow))
                {
                    var rateFlow = KEUnitOfWork.SensorItemEventRepository.GetLastEventBySiteAndItem(siteId, ItemEnum.RateFlow);

                    if (rateFlow != null)
                    {
                        viewModel.RateFlow = Decimal.Parse(rateFlow.Value);
                        viewModel.RateFlowLastMeasurement = rateFlow.EventDate;
                    }
                }

                if (KEUnitOfWork.SensorItemRepository.HasSiteSensorItem(siteId, ItemEnum.Totalizer))
                {
                    var totalizer = KEUnitOfWork.SensorItemEventRepository.GetLastEventBySiteAndItem(siteId, ItemEnum.Totalizer);

                    if (totalizer != null)
                    {
                        viewModel.Totalizer = Int32.Parse(totalizer.Value);
                        viewModel.TotalizerLastMeasurement = totalizer.EventDate;
                    }
                }
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion Site
    }
}