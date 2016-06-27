using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Pond;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class PondController : BaseController
    {
        #region Index

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Index()
        {
            List<ViewModels.Pond.ListViewModel> viewModels = new List<ViewModels.Pond.ListViewModel>();

            if (!IsSite)
            {
                var ponds = KEUnitOfWork.PondRepository.GetsByCustomer(CustomerId).ToList();
                viewModels = ViewModels.Pond.ListViewModel.Map(ponds);
            }
            else
            {
                var ponds = KEUnitOfWork.PondRepository.GetsByCustomerAndSite(CustomerId, SiteId).ToList();
                viewModels = ViewModels.Pond.ListViewModel.Map(ponds);
            }

            AddLog("Navigated to Pond View", LogTypeEnum.Info);
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
        public ActionResult Create(ViewModels.Pond.CreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(InitCreate());
                }

                var pond = viewModel.Map();

                KEUnitOfWork.PondRepository.Add(pond);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Pond");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(InitCreate());
        }

        private ViewModels.Pond.CreateViewModel InitCreate()
        {
            ViewModels.Pond.CreateViewModel viewModel = new ViewModels.Pond.CreateViewModel();

            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            LoadStatuses();

            return viewModel;
        }

        #endregion Create

        #region Edit
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Edit(Guid id)
        {
            var pond = KEUnitOfWork.PondRepository.Get(id);

            if (pond == null)
            {
                AddErrors("Pond does not exist");
                return View("Index");
            }

            ViewModels.Pond.EditViewModel viewModel = InitEdit();
            viewModel.Map(pond);

            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Edit(ViewModels.Pond.EditViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    InitEdit();
                    return View(viewModel);
                }

                var pond = KEUnitOfWork.PondRepository.Get(viewModel.Id);

                if (pond == null)
                {
                    AddErrors("Pond does not exist");
                    return View("Index");
                }

                viewModel.MapVMToEntity(pond);
                //pond.WaterVolumeCapacity = pond.CalculateWaterCapacity();

                KEUnitOfWork.PondRepository.Update(pond);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Pond");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            InitEdit();
            return View(viewModel);
        }

        private ViewModels.Pond.EditViewModel InitEdit()
        {
            ViewModels.Pond.EditViewModel viewModel = new ViewModels.Pond.EditViewModel();

            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

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

        #region Sensor

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorIndex()
        {
            ViewModels.Sensor.ListViewModel viewModel = new ViewModels.Sensor.ListViewModel();
            List<SensorViewModel> sensorViewModels = new List<SensorViewModel>();
            List<Sensor> sensors = null;
            Guid? PondId = null;

            if (Request.QueryString["PondId"] != null)
            {
                Guid pId;
                if (Guid.TryParse(Request.QueryString["PondId"], out pId))
                {
                    PondId = pId;
                }
            }

            viewModel.PondId = PondId;

            if (viewModel.PondId.HasValue)
            {
                sensors = KEUnitOfWork.SensorRepository.GetsByCustomerAndPond(CustomerId, viewModel.PondId.Value).ToList();
                sensorViewModels = SensorViewModel.Map(sensors);
            }

            viewModel.Sensors = sensorViewModels;
            LoadSites();

            AddLog("Navigated to Sensor of Pond View", LogTypeEnum.Info);
            return View(viewModel);
        }

        #region Create

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorCreate(Guid pondId)
        {
            Pond pond = KEUnitOfWork.PondRepository.Get(pondId);

            ViewModels.Sensor.CreateViewModel viewModel = new ViewModels.Sensor.CreateViewModel()
            {
                SiteId = pond.SiteId,
                PondId = pondId
            };

            AddLog("Navigated to Create Sensor of Pond View", LogTypeEnum.Info);
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

            LoadPonds(CustomerId, viewModel.SiteId);
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
                    PondId = viewModel.PondId,
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

                AddLog("Created a Sensor of Pond", LogTypeEnum.Info);
                return RedirectToAction("SensorIndex", "Pond", new { PondId = sensor.PondId });
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

            viewModel.SiteId = sensor.Pond.SiteId;
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

            AddLog("Navigated to Edit Sensor of Pond View", LogTypeEnum.Info);
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

                AddLog("Updated a Sensor of Pond", LogTypeEnum.Info);
                return RedirectToAction("SensorIndex", "Pond", new { PondId = sensor.PondId });
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

            LoadPonds(CustomerId, viewModel.SiteId.Value);
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

            AddLog("Deleted a Sensor of Pond", LogTypeEnum.Info);
            return RedirectToAction("SensorIndex", "Pond", new { PondId = sensor.PondId });
        }

        #endregion Delete

        #endregion Sensor
    }
}
