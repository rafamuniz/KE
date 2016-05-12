using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Tank;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class TankController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Index()
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();

            if (!IsSite)
            {
                var tanks = KEUnitOfWork.TankRepository.GetsByCustomerId(CustomerId).ToList();
                viewModels = ListViewModel.Map(tanks);
            }
            else
            {
                var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, SiteId).ToList();
                viewModels = ListViewModel.Map(tanks);
            }

            return View(viewModels);
        }
        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create()
        {
            return View(InitCreate());
        }

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create(CreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(InitCreate());
                }

                var tank = viewModel.Map();
                var tankModel = viewModel.TankModelViewModel.Map();
                tank.WaterVolumeCapacity = tank.CalculateWaterCapacity();

                KEUnitOfWork.TankRepository.Add(tank);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Tank");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(InitCreate());
        }

        private CreateViewModel InitCreate()
        {
            CreateViewModel viewModel = new CreateViewModel();

            if (!IsSite)
            {
                LoadSites(CustomerId);
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            LoadTankModels();
            LoadStatuses();
            LoadStickConversions();

            return viewModel;
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

            EditViewModel viewModel = InitEdit();
            viewModel.Map(tank);
            viewModel.Map(tank.TankModel);

            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(EditViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    InitEdit();
                    return View(viewModel);
                }

                var tank = KEUnitOfWork.TankRepository.Get(viewModel.Id);

                if (tank == null)
                {
                    AddErrors("Tank does not exist");
                    return View("Index");
                }

                viewModel.MapVMToEntity(tank);
                tank.WaterVolumeCapacity = tank.CalculateWaterCapacity();

                KEUnitOfWork.TankRepository.Update(tank);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Tank");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            InitEdit();
            return View(viewModel);
        }

        private EditViewModel InitEdit()
        {
            EditViewModel viewModel = new EditViewModel();

            if (!IsSite)
            {
                LoadSites(CustomerId);
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            LoadStickConversions();
            LoadTankModels();
            LoadStatuses();

            return viewModel;
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

            tank.DeletedDate = DateTime.UtcNow;
            KEUnitOfWork.TankRepository.Update(tank);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Tank");
        }

        #endregion Delete

        #region Dashboard

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult Dashboard()
        {
            DashboardViewModel viewModel = new DashboardViewModel();

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
        public ActionResult Site(DashboardViewModel viewModel)
        {
            if (viewModel.SiteId != default(Guid))
            {
                LoadTanks(viewModel);
            }

            LoadSites(CustomerId);
            return View("Dashboard", viewModel);
        }

        private void LoadTanks(DashboardViewModel viewModel)
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

                        vm.TankModelId = tank.TankModelId;
                        vm.TankModelImage = tank.TankModel.ImageFilename;

                        vm.SiteId = tank.SiteId;
                        vm.WaterVolumeCapacity = tank.WaterVolumeCapacity;

                        // Water Volume
                        if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Range))
                        {
                            var waterVolume = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.Range);
                            if (waterVolume != null)
                            {
                                vm.WaterVolume = Decimal.Parse(waterVolume.CalculatedValue);
                                vm.WaterVolumeLastMeasurement = waterVolume.EventDate;
                            }
                        }

                        if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.AmbientTemperature))
                        {
                            // Ambient Temperature
                            var ambientTemperature = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.AmbientTemperature);
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
                        var alarms = KEUnitOfWork.AlarmRepository.GetTotalOpenByTankId(tank.Id);
                        vm.Alarms = alarms;

                        viewModel.Tanks.Add(vm);
                    }
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetsWaterVolume(Guid tankId)
        {
            TankViewModel viewModel = new TankViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensor(tankId) &&
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
            if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Range))
            {
                var waterVolume = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tank.Id, ItemEnum.Range);
                if (waterVolume != null)
                {
                    viewModel.WaterVolume = Decimal.Parse(waterVolume.CalculatedValue);
                    viewModel.WaterVolumeLastMeasurement = waterVolume.EventDate;
                }
            }

            // Trigger
            var triggers = KEUnitOfWork.TriggerRepository.GetsByTank(tank.Id);
            viewModel.Triggers = TriggerViewModel.Map(triggers);

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetLastFlowMeter(Guid tankId)
        {
            FlowMeterViewModel viewModel = new FlowMeterViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensor(tankId))
            {
                if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.RateFlow))
                {
                    var rateFlow = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tankId, ItemEnum.RateFlow);

                    if (rateFlow != null)
                    {
                        viewModel.TankId = rateFlow.SensorItem.Sensor.Tank.Id;
                        viewModel.TankName = rateFlow.SensorItem.Sensor.Tank.Name;

                        viewModel.RateFlow = Decimal.Parse(rateFlow.Value);
                    }
                }

                if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tankId, ItemEnum.Totalizer))
                {
                    var totalizer = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankIdAndItem(tankId, ItemEnum.Totalizer);

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
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetsWaterVolumeByOption(Guid tankId, Int64 option)
        {
            TankViewModel viewModel = new TankViewModel();

            if (KEUnitOfWork.SensorRepository.HasSensor(tankId) &&
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

        #region Partial View

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetTankModelPartial()
        {
            return PartialView("_TankModel");
        }

        #endregion Partial View

        #region Trigger

        #region Delete
        //
        // GET: /Trigger/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin")]
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
            try
            {
                var tankModel = KEUnitOfWork.TankModelRepository.Get(Int32.Parse(tankModelId));
                tankModel.WaterVolumeCapacity = tankModel.CalculateWaterCapacity();
                return Json(tankModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult CalculateVolumeCapacity(TankWaterCapacityViewModel viewModel)
        {
            Tank tank = new Tank()
            {
                TankModelId = viewModel.TankModelId,
                Height = viewModel.Height,
                Width = viewModel.Width,
                Length = viewModel.Length,
                FaceLength = viewModel.FaceLength,
                BottomWidth = viewModel.BottomWidth,
                Dimension1 = viewModel.Dimension1,
                Dimension2 = viewModel.Dimension2,
                Dimension3 = viewModel.Dimension3,
                MinimumDistance = viewModel.MinimumDistance,
                MaximumDistance = viewModel.MaximumDistance
            };

            viewModel.WaterVolumeCapacity = tank.CalculateWaterCapacity();

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetTankWaterVolume(Guid tankId)
        //{
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetTankTemperature(Guid tankId)
        //{
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetTankSensorVoltage(Guid tankId)
        //{
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}

        #endregion Fills
    }
}
