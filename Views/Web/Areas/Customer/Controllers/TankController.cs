﻿using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Tank;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    public class TankController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Index()
        {
            var tanks = KEUnitOfWork.TankRepository.GetsByCustomerId(CustomerId).ToList();
            var viewModels = ListViewModel.Map(tanks);
            return View(viewModels);
        }
        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create()
        {
            LoadSites(CustomerId);
            LoadTankModels();
            LoadStatuses();
            CreateViewModel viewModel = new CreateViewModel();
            return View(viewModel);
        }

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadSites(CustomerId);
                LoadTankModels();
                LoadStatuses();
                return View(viewModel);
            }

            try
            {
                var tank = viewModel.Map();
                var tankModel = viewModel.TankModelViewModel.Map();
                tank.WaterVolumeCapacity = tank.CalculateWaterCapacity();

                KEUnitOfWork.TankRepository.Add(tank);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Tank");
            }
            catch (DbEntityValidationException dbex)
            {
                AddErrors(dbex);
            }
            catch (Exception ex)
            {
                AddErrors(ex.Message);
            }

            LoadSites(CustomerId);
            LoadTankModels();
            LoadStatuses();
            return View(viewModel);
        }
        #endregion Create

        #region Edit
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(Guid id)
        {
            var tank = KEUnitOfWork.TankRepository.Get(id);

            if (tank == null)
            {
                AddErrors("Tank does not exist");
                return View("Index");
            }

            LoadStatuses();
            LoadSites(CustomerId);
            LoadTankModels();
            EditViewModel viewModel = EditViewModel.Map(tank);
            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(EditViewModel viewModel)
        {
            var tank = KEUnitOfWork.TankRepository.Get(viewModel.Id);

            if (tank == null)
            {
                LoadSites(CustomerId);
                LoadTankModels();
                LoadStatuses();
                AddErrors("Tank does not exist");
                return View("Index");
            }

            tank.Name = viewModel.Name;
            tank.Description = viewModel.Description;
            tank.Status = viewModel.Status;
            tank.SiteId = viewModel.SiteId;
            tank.TankModelId = viewModel.TankModelId;

            KEUnitOfWork.TankRepository.Update(tank);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Tank");
        }
        #endregion Edit

        #region Delete
        //
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Delete(Guid id)
        {
            var tank = KEUnitOfWork.TankRepository.Get(id);

            if (tank == null)
            {
                AddErrors("Tank does not exist");
                return View("Index");
            }

            KEUnitOfWork.TankRepository.Remove(tank);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Tank");
        }

        #endregion Delete

        #region Dashboard

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult Dashboard()
        {
            LoadSites(CustomerId);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult Site(DashboardViewModel viewModel)
        {
            if (viewModel.SiteId != default(Guid))
            {
                var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, viewModel.SiteId);

                if (tanks.Any())
                {
                    foreach (var tank in tanks)
                    {
                        if (KEUnitOfWork.SensorRepository.HasSensor(tank.Id))
                        {
                            DashboardTankViewModel vm = new DashboardTankViewModel();

                            vm.TankId = tank.Id;
                            vm.TankName = tank.Name;
                            vm.ImageTankModelFilename = tank.TankModel.ImageFilename;
                            vm.SiteId = tank.SiteId;
                            vm.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                            // Water Volume
                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterVolume))
                            {
                                var waterVolume = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.WaterVolume);
                                if (waterVolume != null)
                                {
                                    vm.WaterVolume = Decimal.Parse(waterVolume.Value);
                                    vm.WaterVolumeLastMeasurement = waterVolume.EventDate;
                                }
                            }

                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WeatherTemperature))
                            {
                                // Ambient Temperature
                                var ambientTemperature = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.WeatherTemperature);
                                if (ambientTemperature != null)
                                {
                                    vm.AmbientTemperature = Decimal.Parse(ambientTemperature.Value);
                                    vm.AmbientTemperatureLastMeasurement = ambientTemperature.EventDate;
                                }
                            }

                            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterTemperature))
                            {
                                // Water Temperature
                                var waterTemperature = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.WaterTemperature);
                                if (waterTemperature != null)
                                {
                                    vm.WaterTemperature = Decimal.Parse(waterTemperature.Value);
                                    vm.WaterTemperatureLastMeasurement = waterTemperature.EventDate;
                                }
                            }

                            // Alarms
                            vm.Alarms = 0;
                            //var alarms = KEUnitOfWork.AlarmRepository.GetAll(tank.Id, ItemEnum.WaterTemperature);

                            viewModel.Tanks.Add(vm);
                        }
                    }
                }
            }

            LoadSites(CustomerId);
            return View("Dashboard", viewModel);
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

        #endregion Dashboard

        #region Gauge

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
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
                var waterVolume = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.WaterVolume);
                if (waterVolume != null)
                {
                    viewModel.WaterVolume = Decimal.Parse(waterVolume.Value);
                    viewModel.WaterVolumeLastMeasurement = waterVolume.EventDate;
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetsVoltage(Guid tankId)
        {
            VoltageViewModel viewModel = new VoltageViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensor(tankId) &&
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
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetsWaterAndWeatherTemperature(Guid tankId)
        {
            TemperatureViewModel viewModel = new TemperatureViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensor(tankId))
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
                if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.WeatherTemperature))
                {
                    var weatherTemperatures = KEUnitOfWork.SensorItemEventRepository.GetsByTankIdAndByItem(tankId, ItemEnum.WeatherTemperature, 5);

                    if (weatherTemperatures.Any())
                    {
                        foreach (var wt in weatherTemperatures)
                        {
                            EventViewModel evm = new EventViewModel()
                            {
                                EventDate = wt.EventDate,
                                Value = wt.Value
                            };

                            viewModel.WeatherTempertatures.Add(evm);
                        }
                    }
                }
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion Gauge

        #region Partial View

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetTankModelPartial()
        {
            return PartialView("_TankModel");
        }

        #endregion Partial View

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

        public ActionResult GetTankModel(String tankModelId)
        {
            var tankModel = KEUnitOfWork.TankModelRepository.Get(Int32.Parse(tankModelId));
            return Json(tankModel, JsonRequestBehavior.AllowGet);
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
