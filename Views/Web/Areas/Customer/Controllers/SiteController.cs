using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Site;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class SiteController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Index()
        {
            List<Site> entities = KEUnitOfWork.SiteRepository.GetsByCustomerId(CustomerId).ToList();
            var viewModels = ListViewModel.Map(entities);
            return View(viewModels);
        }

        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create()
        {
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
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadStatuses();
                    return View(viewModel);
                }

                Site site = viewModel.Map();
                site.CustomerId = CustomerId;

                Core.Entities.Address address = viewModel.MapAddress();
                site.Address = address;

                KEUnitOfWork.SiteRepository.Add(site);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Site", new { area = "Customer" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadStatuses();
            return View(viewModel);
        }
        #endregion Create

        #region Edit

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(Guid id)
        {
            var site = KEUnitOfWork.SiteRepository.Get(id);

            if (site == null)
            {
                AddErrors("Site does not exist");
                return View("Index");
            }

            LoadStatuses();
            EditViewModel viewModel = new EditViewModel();
            viewModel.Map(site);
            viewModel.Map(site.Address);

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
                    LoadStatuses();
                    return View(viewModel);
                }

                var site = KEUnitOfWork.SiteRepository.Get(viewModel.Id);

                if (site == null)
                {
                    LoadStatuses();
                    AddErrors("Site does not exist");
                    return View("Index");
                }

                viewModel.MapVMToEntity(site);
                viewModel.MapVMToEntity(site.Address);

                KEUnitOfWork.SiteRepository.Update(site);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Site");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadStatuses();
            return View(viewModel);
        }
        #endregion Edit

        #region Delete
        //
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Delete(Guid id)
        {
            var site = KEUnitOfWork.SiteRepository.Get(id);

            if (site == null)
            {
                AddErrors("Site does not exist");
                return View("Index");
            }

            site.DeletedDate = DateTime.UtcNow;
            KEUnitOfWork.SiteRepository.Update(site);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Site");
        }
        #endregion Delete

        #region Report

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult Report()
        {
            ReportViewModel viewModel = new ReportViewModel();

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
        public ActionResult Site(ReportViewModel viewModel)
        {
            if (viewModel.SiteId != default(Guid))
            {
                LoadTanks(viewModel);
            }

            LoadSites(CustomerId);
            return View("Report", viewModel);
        }

        private void LoadTanks(ReportViewModel viewModel)
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
                                tankViewModel.WaterVolume = Decimal.Parse(waterVolume.CalculatedValue);
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

        #endregion Report
    }
}
