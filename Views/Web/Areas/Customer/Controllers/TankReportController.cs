using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.TankReport;
using KarmicEnergy.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class TankReportController : BaseController
    {
        #region Index

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult Index()
        {
            ListViewModel viewModel = new ListViewModel();
            LoadSites(CustomerId);
            return View(viewModel);
        }

        #endregion Index

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
                        Report report = new Report();

                        report.TankId = tank.Id;
                        report.TankName = tank.Name;
                        report.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                        if (KEUnitOfWork.SensorRepository.HasSensor(tank.Id))
                        {
                            // Water Volume
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Range))
                            {
                                var waterVolumesLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.Range);

                                if (waterVolumesLastEvent != null)
                                {
                                    report.WaterVolume = Decimal.Parse(waterVolumesLastEvent.CalculatedValue);
                                    report.WaterVolumeEventDate = waterVolumesLastEvent.EventDate;
                                }
                            }

                            // Water Temperature
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterTemperature))
                            {
                                var waterTemperatureLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.WaterTemperature);

                                if (waterTemperatureLastEvent != null)
                                {
                                    report.WaterTemperature = Decimal.Parse(waterTemperatureLastEvent.Value);
                                    report.WaterTemperatureEventDate = waterTemperatureLastEvent.EventDate;
                                }
                            }

                            // Weather Temperature
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.AmbientTemperature))
                            {
                                var ambientTemperatureLastEvent = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.AmbientTemperature);

                                if (ambientTemperatureLastEvent != null)
                                {
                                    report.WeatherTemperature = Decimal.Parse(ambientTemperatureLastEvent.Value);
                                    report.WeatherTemperatureEventDate = ambientTemperatureLastEvent.EventDate;
                                }
                            }
                        }

                        // Alarms
                        var alarms = KEUnitOfWork.AlarmRepository.GetTotalOpenByTankId(tank.Id);
                        report.Alarms = alarms;

                        viewModel.Reports.Add(report);
                    }
                }
            }

            LoadSites(CustomerId);
            return View("Index", viewModel);
        }
    }
}