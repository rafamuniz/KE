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
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
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

            AddLog("Navigated to Tank View", LogTypeEnum.Info);
            return View(viewModels);
        }
        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Create()
        {
            return View(InitCreate());
        }

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
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
                LoadSites();
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
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
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
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
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
                LoadSites();
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
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
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

        //private void LoadTanks(DashboardViewModel viewModel)
        //{
        //    var tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(CustomerId, viewModel.SiteId);

        //    if (tanks.Any())
        //    {
        //        foreach (var tank in tanks)
        //        {
        //            DashboardTankViewModel vm = new DashboardTankViewModel();

        //            vm.TankId = tank.Id;
        //            vm.TankName = tank.Name;

        //            vm.TankModelId = tank.TankModelId;
        //            vm.TankModelImage = tank.TankModel.ImageFilename;

        //            vm.SiteId = tank.SiteId;
        //            vm.WaterVolumeCapacity = tank.WaterVolumeCapacity;

        //            if (KEUnitOfWork.SensorRepository.HasSensorTank(tank.Id))
        //            {
        //                // Water Volume
        //                if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.Range))
        //                {
        //                    var waterVolume = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.Range);
        //                    if (waterVolume != null)
        //                    {
        //                        vm.WaterVolume = Decimal.Parse(waterVolume.CalculatedValue);
        //                        vm.WaterVolumeLastMeasurement = waterVolume.EventDate;
        //                    }
        //                }

        //                if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.AmbientTemperature))
        //                {
        //                    // Ambient Temperature
        //                    var ambientTemperature = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.AmbientTemperature);
        //                    if (ambientTemperature != null)
        //                    {
        //                        vm.AmbientTemperature = Decimal.Parse(ambientTemperature.Value);
        //                        vm.AmbientTemperatureLastMeasurement = ambientTemperature.EventDate;
        //                    }
        //                }

        //                if (KEUnitOfWork.SensorItemRepository.HasSensorItem(tank.Id, ItemEnum.WaterTemperature))
        //                {
        //                    // Water Temperature
        //                    var waterTemperature = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tank.Id, ItemEnum.WaterTemperature);
        //                    if (waterTemperature != null)
        //                    {
        //                        vm.WaterTemperature = Decimal.Parse(waterTemperature.Value);
        //                        vm.WaterTemperatureLastMeasurement = waterTemperature.EventDate;
        //                    }
        //                }
        //            }

        //            // Alarms
        //            var alarms = KEUnitOfWork.AlarmRepository.GetTotalOpenByTankId(tank.Id);
        //            vm.Alarms = alarms;

        //            viewModel.Tanks.Add(vm);
        //        }
        //    }
        //}



        //#endregion Dashboard

        #region Partial View

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetTankModelPartial()
        {
            return PartialView("_TankModel");
        }

        #endregion Partial View

        #region Fills

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult FillTank(Guid siteId)
        {
            var tanks = LoadTanks(CustomerId, siteId);
            return Json(tanks, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetTankInfo(Guid tankId)
        {
            var tankInfo = KEUnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tankId, ItemEnum.WaterVolume);
            return Json(tankInfo, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetTankModel(String tankModelId)
        {
            var tankModel = KEUnitOfWork.TankModelRepository.Get(Int32.Parse(tankModelId));
            tankModel.WaterVolumeCapacity = tankModel.CalculateWaterCapacity();
            return Json(tankModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
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

        #endregion Fills
    }
}
