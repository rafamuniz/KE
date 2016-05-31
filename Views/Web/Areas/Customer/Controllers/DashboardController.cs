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
        public ActionResult Site(TankDashboardViewModel viewModel)
        {
            if (viewModel.SiteId != default(Guid))
            {
                var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, viewModel.SiteId);

                if (tanks.Any())
                {
                    foreach (var tank in tanks)
                    {
                        TankViewModel tankViewModel = new TankViewModel();

                        tankViewModel.Id = tank.Id;
                        tankViewModel.Name = tank.Name;
                        tankViewModel.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                        if (KEUnitOfWork.SensorRepository.HasSensor(tank.Id))
                        {
                            // Water Volume
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Range))
                            {
                                var waterVolumesLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.Range);

                                if (waterVolumesLastEvent != null)
                                {
                                    tankViewModel.WaterVolumeLast = Decimal.Parse(waterVolumesLastEvent.CalculatedValue);
                                    tankViewModel.WaterVolumeLastMeasurement = waterVolumesLastEvent.EventDate;
                                }
                            }

                            // Water Temperature
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterTemperature))
                            {
                                var waterTemperatureLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.WaterTemperature);

                                if (waterTemperatureLastEvent != null)
                                {
                                    tankViewModel.WaterTemperature = Decimal.Parse(waterTemperatureLastEvent.Value);
                                    tankViewModel.WaterTemperatureEventDate = waterTemperatureLastEvent.EventDate;
                                }
                            }

                            // Weather Temperature
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.AmbientTemperature))
                            {
                                var ambientTemperatureLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.AmbientTemperature);

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
            }

            LoadSites(CustomerId);
            return View("Index", viewModel);
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
            else
            {
                viewModel.SiteId = SiteId;
                LoadTanks(viewModel);
            }

            return View(viewModel);
        }
        
        [HttpPost]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult SiteSelected(SiteDashboardViewModel viewModel)
        {
            if (viewModel.SiteId != default(Guid))
            {
                LoadTanks(viewModel);
            }

            LoadSites(CustomerId);
            return View("Site", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetLastFlowMeter(Guid tankId)
        {
            FlowMeterViewModel viewModel = new FlowMeterViewModel();

            //if (KEUnitOfWork.SensorRepository.HasSensor(tankId))
            //{
            //    if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.RateFlow))
            //    {
            //        var rateFlow = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tankId, ItemEnum.RateFlow);

            //        if (rateFlow != null)
            //        {
            //            viewModel.TankId = rateFlow.SensorItem.Sensor.Tank.Id;
            //            viewModel.TankName = rateFlow.SensorItem.Sensor.Tank.Name;
            //            viewModel.RateFlow = Decimal.Parse(rateFlow.Value);
            //        }
            //    }

            //    if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.Totalizer))
            //    {
            //        var totalizer = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tankId, ItemEnum.Totalizer);

            //        if (totalizer != null)
            //        {
            //            viewModel.TankId = totalizer.SensorItem.Sensor.Tank.Id;
            //            viewModel.TankName = totalizer.SensorItem.Sensor.Tank.Name;

            //            viewModel.Totalizer = Int32.Parse(totalizer.Value);
            //        }
            //    }
            //}

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        
        private void LoadTanks(SiteDashboardViewModel viewModel)
        {
            var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, viewModel.SiteId);

            if (tanks.Any())
            {
                foreach (var tank in tanks)
                {
                    TankViewModel tankViewModel = new TankViewModel();

                    tankViewModel.Id = tank.Id;
                    tankViewModel.Name = tank.Name;
                    tankViewModel.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                    if (KEUnitOfWork.SensorRepository.HasSensor(tank.Id))
                    {
                        // Water Volume
                        if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Range))
                        {
                            var waterVolume = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.Range);
                            if (waterVolume != null)
                            {
                                tankViewModel.WaterVolumeLast = Decimal.Parse(waterVolume.CalculatedValue);
                                tankViewModel.WaterVolumeLastMeasurement = waterVolume.EventDate;
                            }
                        }

                        FlowMeterViewModel flowMeterViewModel = new FlowMeterViewModel();

                        // Rate Flow
                        if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.RateFlow))
                        {
                            var rateFlow = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.RateFlow);

                            if (rateFlow != null)
                            {
                                flowMeterViewModel.RateFlow = Decimal.Parse(rateFlow.Value);
                            }
                        }

                        // Totalizer
                        if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Totalizer))
                        {
                            var totalizer = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.Totalizer);

                            if (totalizer != null)
                            {
                                flowMeterViewModel.Totalizer = Int32.Parse(totalizer.Value);
                            }
                        }

                        viewModel.FlowMeters.Add(flowMeterViewModel);

                        //if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.AmbientTemperature))
                        //{
                        //    // Ambient Temperature
                        //    var ambientTemperature = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.AmbientTemperature);
                        //    if (ambientTemperature != null)
                        //    {
                        //        vm.AmbientTemperature = Decimal.Parse(ambientTemperature.Value);
                        //        vm.AmbientTemperatureLastMeasurement = ambientTemperature.EventDate;
                        //    }
                        //}

                        //if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterTemperature))
                        //{
                        //    // Water Temperature
                        //    var waterTemperature = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.WaterTemperature);
                        //    if (waterTemperature != null)
                        //    {
                        //        vm.WaterTemperature = Decimal.Parse(waterTemperature.Value);
                        //        vm.WaterTemperatureLastMeasurement = waterTemperature.EventDate;
                        //    }
                        //}
                    }

                    //// Alarms
                    //var alarms = KEUnitOfWork.AlarmRepository.GetTotalOpenByTankId(tank.Id);
                    //vm.Alarms = alarms;                   

                    viewModel.Tanks.Add(tankViewModel);
                }
            }
        }

        #endregion Site
    }
}