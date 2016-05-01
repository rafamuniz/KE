using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class SensorController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Index()
        {
            List<Sensor> sensors = null;

            if (Request.QueryString.AllKeys.Contains("TankId"))
            {
                sensors = KEUnitOfWork.SensorRepository.GetsByTankIdAndCustomerId(CustomerId, TankId);
            }
            else
            {
                sensors = KEUnitOfWork.SensorRepository.GetsByCustomerId(CustomerId);
            }

            var viewModels = ListViewModel.Map(sensors);
            return View(viewModels);
        }

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Tank(Guid tankId)
        {
            var sensors = KEUnitOfWork.SensorRepository.GetsByTankIdAndCustomerId(CustomerId, TankId);
            var viewModels = ListViewModel.Map(sensors);
            return View("Index", viewModels);
        }
        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create()
        {
            return View(LoadCreateViewModel(null));
        }

        private CreateViewModel LoadCreateViewModel(CreateViewModel viewModel)
        {
            CreateViewModel vm = null;

            if (viewModel == null)
            {
                vm = new CreateViewModel();
                vm.TankId = TankId;
            }
            else
                vm = viewModel; 

            LoadTanks(CustomerId);
            LoadSensorTypes();
            LoadStatuses();

            return vm;
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
                    return View(LoadCreateViewModel(viewModel));
                }

                // Validate Item check=true
                if (!IsValidateItems(viewModel))
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

                return RedirectToAction("Index", "Sensor", new { TankId = sensor.TankId });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(LoadCreateViewModel(viewModel));
        }

        private Boolean IsValidateItems(CreateViewModel viewModel)
        {
            Boolean flag = true;

            if (viewModel.Items.Any())
            {
                foreach (var item in viewModel.Items)
                {
                    if (item.IsSelected && item.UnitSelected == null)
                    {
                        AddErrors("Unit is required");
                        flag = false;
                    }
                }
            }

            return flag;
        }
        #endregion Create

        #region Edit
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(Guid id)
        {
            var sensor = KEUnitOfWork.SensorRepository.Get(id);

            if (sensor == null)
            {
                AddErrors("Sensor does not exist");
                return View("Index");
            }

            EditViewModel viewModel = new EditViewModel();
            viewModel = EditViewModel.Map(sensor);

            LoadEditViewModel(viewModel, sensor);

            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(EditViewModel viewModel)
        {
            Sensor sensor = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    LoadEditViewModel(viewModel, sensor);
                    AddErrors("Tank does not exist");
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

                            if (hasSensorItem == null)
                            {
                                SensorItem sensorItem = new SensorItem()
                                {
                                    ItemId = item.Id,
                                    UnitId = item.UnitSelected.Value
                                };

                                sensor.SensorItems.Add(sensorItem);
                            }
                        }
                        else
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

                return RedirectToAction("Index", "Sensor", new { TankId = sensor.TankId });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadEditViewModel(viewModel, sensor);

            return View(viewModel);
        }

        private void LoadEditViewModel(EditViewModel viewModel, Sensor sensor)
        {
            var items = LoadItems();
            viewModel.Items = ItemViewModel.Map(items);

            var units = LoadUnits();

            foreach (var item in viewModel.Items)
            {
                item.Units = UnitViewModel.Map(units);
            }

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

            LoadStatuses();
            LoadTanks(CustomerId);
            LoadSensorTypes();
        }

        #endregion Edit

        #region Delete
        //
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Delete(Guid id)
        {
            var sensor = KEUnitOfWork.SensorRepository.Get(id);

            if (sensor == null)
            {
                AddErrors("Sensor does not exist");
                return View("Index");
            }

            sensor.DeletedDate = DateTime.UtcNow;
            KEUnitOfWork.SensorRepository.Update(sensor);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index");
        }

        #endregion Delete

        #region JSON

        [HttpGet]
        [Authorize]
        public ActionResult GetsItemBySensorTypeId(String sensorTypeId)
        {
            List<ItemViewModel> viewModels = new List<ItemViewModel>();
            List<Item> items = KEUnitOfWork.ItemRepository.GetsBySensorTypeId(Int16.Parse(sensorTypeId));
            List<Unit> units = KEUnitOfWork.UnitRepository.GetAllActive().ToList();

            if (items.Any())
            {
                foreach (var i in items)
                {
                    ItemViewModel vm = new ItemViewModel()
                    {
                        Id = i.Id,
                        Name = i.Name
                    };

                    var u = units.Where(x => x.UnitTypeId == i.UnitTypeId).ToList();
                    vm.Units = UnitViewModel.Map(u);

                    viewModels.Add(vm);
                }
            }

            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        #endregion JSON
    }
}
