using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Services.Interface;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor;
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
        #region Fields
        private readonly ITankService _tankService;
        #endregion Fields

        #region Constructor
        public TankController(ITankService tankService)
        {
            this._tankService = tankService;
        }
        #endregion Constructor

        #region Index
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Index()
        {
            IEnumerable<Tank> tanks;

            if (!IsSite)
            {
                tanks = _tankService.GetsByCustomer(CustomerId);
            }
            else
            {
                tanks = _tankService.GetsBySite(SiteId);
            }

            List<ViewModels.Tank.ListViewModel> viewModels = ViewModels.Tank.ListViewModel.Map(tanks);

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
        // POST: /Tank/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Create(ViewModels.Tank.CreateViewModel viewModel)
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

                _tankService.Create(tank);

                return RedirectToAction("Index", "Tank");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(InitCreate());
        }

        private ViewModels.Tank.CreateViewModel InitCreate()
        {
            ViewModels.Tank.CreateViewModel viewModel = new ViewModels.Tank.CreateViewModel();

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

            ViewModels.Tank.EditViewModel viewModel = InitEdit();
            viewModel.Map(tank);
            viewModel.Map(tank.TankModel);

            return View(viewModel);
        }

        //
        // POST: /Tank/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Edit(ViewModels.Tank.EditViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    InitEdit();
                    return View(viewModel);
                }

                var tank = viewModel.Map();
                tank.WaterVolumeCapacity = tank.CalculateWaterCapacity();

                _tankService.Update(tank);

                return RedirectToAction("Index", "Tank");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            InitEdit();
            return View(viewModel);
        }

        private ViewModels.Tank.EditViewModel InitEdit()
        {
            ViewModels.Tank.EditViewModel viewModel = new ViewModels.Tank.EditViewModel();

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
        // GET: /Tank/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Delete(Guid id)
        {
            _tankService.Delete(id);
            return RedirectToAction("Index", "Tank");
        }

        #endregion Delete

        #region Sensor

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorIndex()
        {
            ViewModels.Sensor.ListViewModel viewModel = new ViewModels.Sensor.ListViewModel();
            List<SensorViewModel> sensorViewModels = new List<SensorViewModel>();
            List<Sensor> sensors = null;
            Guid? TankId = null;

            if (Request.QueryString["TankId"] != null)
            {
                Guid tId;
                if (Guid.TryParse(Request.QueryString["TankId"], out tId))
                {
                    TankId = tId;
                }
            }

            viewModel.TankId = TankId;

            if (viewModel.TankId.HasValue)
            {
                sensors = KEUnitOfWork.SensorRepository.GetsByCustomerAndTank(CustomerId, viewModel.TankId.Value).ToList();
                sensorViewModels = SensorViewModel.Map(sensors);
            }

            viewModel.Sensors = sensorViewModels;
            LoadSites();

            AddLog("Navigated to Sensor of Tank View", LogTypeEnum.Info);
            return View(viewModel);
        }

        #region Create

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorCreate(Guid tankId)
        {
            Tank tank = KEUnitOfWork.TankRepository.Get(tankId);

            ViewModels.Sensor.CreateViewModel viewModel = new ViewModels.Sensor.CreateViewModel()
            {
                SiteId = tank.SiteId,
                TankId = tankId
            };

            AddLog("Navigated to Create Sensor of Tank View", LogTypeEnum.Info);
            return View(LoadCreateViewModel(viewModel));
        }

        private ViewModels.Sensor.CreateViewModel LoadCreateViewModel(ViewModels.Sensor.CreateViewModel viewModel)
        {
            if (viewModel == null)
                viewModel = new ViewModels.Sensor.CreateViewModel();

            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            LoadTanks(CustomerId, viewModel.SiteId);
            LoadSensorTypes();
            LoadStatuses();

            return viewModel;
        }

        //
        // POST: /Sensor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorCreate(ViewModels.Sensor.CreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(LoadCreateViewModel(viewModel));
                }

                // Validate Item check=true
                if (!IsValidateCreateItems(viewModel))
                {
                    return View(LoadCreateViewModel(viewModel));
                }

                Sensor sensor = new Sensor()
                {
                    Name = viewModel.Name,
                    TankId = viewModel.TankId,
                    SensorTypeId = viewModel.SensorTypeId,
                    Status = viewModel.Status,
                    Reference = viewModel.Reference
                };

                if (viewModel.Items.Any())
                {
                    foreach (var item in viewModel.Items)
                    {
                        if (item.IsSelected)
                        {
                            SensorItem sensorItem = new SensorItem()
                            {
                                ItemId = item.Id,
                                UnitId = item.UnitSelected.Value
                            };

                            sensor.SensorItems.Add(sensorItem);
                        }
                    }
                }

                KEUnitOfWork.SensorRepository.Add(sensor);
                KEUnitOfWork.Complete();

                AddLog("Created a Sensor of Tank", LogTypeEnum.Info);
                return RedirectToAction("SensorIndex", "Tank", new { TankId = sensor.TankId });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(LoadCreateViewModel(viewModel));
        }

        private Boolean IsValidateCreateItems(ViewModels.Sensor.CreateViewModel viewModel)
        {
            Boolean flag = true;

            if (viewModel.Items.Any())
            {
                foreach (var item in viewModel.Items)
                {
                    if (item.IsSelected && !item.UnitSelected.HasValue)
                    {
                        AddErrors(String.Format("Unit is required for {0}", item.Name));
                        flag = false;
                    }
                }
            }

            return flag;
        }
        #endregion Create

        #region Edit

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorEdit(Guid id)
        {
            var sensor = KEUnitOfWork.SensorRepository.Get(id);

            if (sensor == null)
            {
                AddErrors("Sensor does not exist");
                return View("SensorIndex");
            }

            ViewModels.Sensor.EditViewModel viewModel = new ViewModels.Sensor.EditViewModel();
            viewModel = ViewModels.Sensor.EditViewModel.Map(sensor);

            viewModel.SiteId = sensor.Tank.SiteId;
            LoadEditViewModel(viewModel);

            var items = LoadItems();
            viewModel.Items = ItemViewModel.Map(items);

            if (sensor.SensorItems.Any())
            {
                List<ItemViewModel> selectedItems = new List<ItemViewModel>();

                foreach (var item in sensor.SensorItems)
                {
                    foreach (var avalItem in viewModel.Items)
                    {
                        if (item.ItemId == avalItem.Id)
                        {
                            avalItem.IsSelected = true;
                            avalItem.UnitSelected = item.UnitId;
                        }
                    }
                }
            }

            AddLog("Navigated to Edit Sensor of Tank View", LogTypeEnum.Info);
            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorEdit(ViewModels.Sensor.EditViewModel viewModel)
        {
            Sensor sensor = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    LoadEditViewModel(viewModel);
                    AddErrors("Sensor does not exist");
                    return View(viewModel);
                }

                // Validate Item check=true
                if (!IsValidateEditItems(viewModel))
                {
                    LoadEditViewModel(viewModel);
                    return View(viewModel);
                }

                sensor = KEUnitOfWork.SensorRepository.Get(viewModel.Id);

                sensor.Name = viewModel.Name;
                sensor.TankId = viewModel.TankId;
                sensor.SensorTypeId = viewModel.SensorTypeId;
                sensor.Status = viewModel.Status;
                sensor.Reference = viewModel.Reference;

                if (viewModel.Items.Any())
                {
                    foreach (var item in viewModel.Items)
                    {
                        if (item.IsSelected)
                        {
                            var hasSensorItem = sensor.SensorItems.Where(x => x.ItemId == item.Id && x.DeletedDate == null).SingleOrDefault();

                            if (hasSensorItem == null) // ADD
                            {
                                SensorItem sensorItem = new SensorItem()
                                {
                                    ItemId = item.Id,
                                    UnitId = item.UnitSelected.Value
                                };

                                sensor.SensorItems.Add(sensorItem);
                            }
                            else if (hasSensorItem.UnitId != item.UnitSelected.Value) // UPDATE
                            {
                                sensor.SensorItems.Where(x => x.ItemId == item.Id && x.DeletedDate == null).SingleOrDefault().UnitId = item.UnitSelected.Value;
                            }
                        }
                        else // DELETE
                        {
                            var hasSensorItem = sensor.SensorItems.Where(x => x.ItemId == item.Id && x.DeletedDate == null).SingleOrDefault();

                            if (hasSensorItem != null)
                            {
                                hasSensorItem.DeletedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }

                KEUnitOfWork.SensorRepository.Update(sensor);
                KEUnitOfWork.Complete();

                AddLog("Updated a Sensor of Tank", LogTypeEnum.Info);
                return RedirectToAction("SensorIndex", "Tank", new { TankId = sensor.TankId });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadEditViewModel(viewModel);

            return View(viewModel);
        }

        private void LoadEditViewModel(ViewModels.Sensor.EditViewModel viewModel)
        {
            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            LoadTanks(CustomerId, viewModel.SiteId.Value);
            LoadStatuses();
            LoadSensorTypes();
        }

        private Boolean IsValidateEditItems(ViewModels.Sensor.EditViewModel viewModel)
        {
            Boolean flag = true;

            if (viewModel.Items.Any())
            {
                foreach (var item in viewModel.Items)
                {
                    if (item.IsSelected && !item.UnitSelected.HasValue)
                    {
                        AddErrors(String.Format("Unit is required for {0}", item.Name));
                        flag = false;
                    }
                }
            }

            return flag;
        }

        #endregion Edit

        #region Delete
        //
        // GET: /Tank/SensorDelete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorDelete(Guid id)
        {
            var sensor = KEUnitOfWork.SensorRepository.Get(id);

            if (sensor == null)
            {
                AddErrors("Sensor does not exist");
                return View("SensorIndex");
            }

            sensor.DeletedDate = DateTime.UtcNow;

            foreach (var item in sensor.SensorItems)
            {
                item.DeletedDate = DateTime.UtcNow;
            }

            KEUnitOfWork.SensorRepository.Update(sensor);
            KEUnitOfWork.Complete();

            AddLog("Deleted a Sensor of Tank", LogTypeEnum.Info);
            return RedirectToAction("SensorIndex", "Tank", new { TankId = sensor.TankId });
        }

        #endregion Delete

        #endregion Sensor

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
