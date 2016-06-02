using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Munizoft.Extensions;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class SensorController : BaseController
    {
        #region Index

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Index()
        {
            ListViewModel viewModel = new ListViewModel();
            List<SensorViewModel> sensorViewModels = new List<SensorViewModel>();
            List<Sensor> sensors = null;

            if (!IsSite)
            {
                //sensors = KEUnitOfWork.SensorRepository.GetsByCustomer(CustomerId).ToList();
                //sensorViewModels = SensorViewModel.Map(sensors);
            }
            else
            {
                sensors = KEUnitOfWork.SensorRepository.GetsByCustomerAndSite(CustomerId, SiteId).ToList();
                sensorViewModels = SensorViewModel.Map(sensors);
                viewModel.SiteId = SiteId;
            }

            viewModel.Sensors = sensorViewModels;
            LoadSites(CustomerId);
            return View("Index", viewModel);
        }

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult SiteSiteSelected(Guid siteId)
        {
            ListViewModel viewModel = new ListViewModel();

            List<Sensor> sensors = KEUnitOfWork.SensorRepository.GetsBySite(siteId).ToList();

            List<SensorViewModel> sensorViewModels = SensorViewModel.Map(sensors);
            viewModel.SiteId = siteId;
            viewModel.Sensors = sensorViewModels;
            LoadSites(CustomerId);
            return View("Index", viewModel);
        }

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Tank(Guid tankId)
        {
            var sensors = KEUnitOfWork.SensorRepository.GetsByCustomerAndTank(CustomerId, TankId);
            var sensorViewModels = SensorViewModel.Map(sensors);

            ListViewModel viewModel = new ListViewModel();
            viewModel.TankId = tankId;
            viewModel.Sensors = sensorViewModels;

            return View("Index", viewModel);
        }

        #endregion Index

        #region Create

        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create(Guid? tankId)
        {
            CreateViewModel viewModel = new CreateViewModel()
            {
                TankId = tankId
            };

            return View(LoadCreateViewModel(viewModel));
        }

        private CreateViewModel LoadCreateViewModel(CreateViewModel viewModel)
        {
            if (viewModel == null)
                viewModel = new CreateViewModel();

            if (viewModel.TankId.HasValue && viewModel.TankId != default(Guid))
            {
                var tank = KEUnitOfWork.TankRepository.Get(viewModel.TankId.Value);
                viewModel.SiteId = tank.SiteId;
                LoadTanks(CustomerId);
            }

            if (!IsSite)
            {
                LoadSites(CustomerId);
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            LoadSensorTypes();
            LoadStatuses();

            return viewModel;
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
                if (!IsValidateCreateItems(viewModel))
                {
                    return View(LoadCreateViewModel(viewModel));
                }

                Sensor sensor = new Sensor()
                {
                    Name = viewModel.Name,
                    TankId = viewModel.TankId,
                    SiteId = viewModel.SiteId,
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

        private Boolean IsValidateCreateItems(CreateViewModel viewModel)
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
                    LoadEditViewModel(viewModel);
                    AddErrors("Tank does not exist");
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

                return RedirectToAction("Index", "Sensor", new { TankId = sensor.TankId });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadEditViewModel(viewModel);

            return View(viewModel);
        }

        private void LoadEditViewModel(EditViewModel viewModel)
        {
            //var items = LoadItems();
            //viewModel.Items = ItemViewModel.Map(items);

            //var units = LoadUnits();

            //foreach (var item in viewModel.Items)
            //{
            //    item.Units = UnitViewModel.Map(units);
            //}

            //if (sensor.SensorItems.Any())
            //{
            //    List<ItemViewModel> selectedItems = new List<ItemViewModel>();

            //    foreach (var item in sensor.SensorItems)
            //    {
            //        foreach (var avalItem in viewModel.Items)
            //        {
            //            if (item.ItemId == avalItem.Id)
            //            {
            //                avalItem.IsSelected = true;
            //                avalItem.UnitSelected = item.UnitId;
            //            }
            //        }
            //    }
            //}

            LoadStatuses();
            LoadTanks(CustomerId);
            LoadSensorTypes();
        }

        private Boolean IsValidateEditItems(EditViewModel viewModel)
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

        [HttpGet]
        public ActionResult GetsTankBySite(Guid siteId)
        {
            var tanks = LoadTanks(CustomerId, siteId);
            SelectList obgTanks = new SelectList(tanks, "Id", "Name", 0);
            return Json(obgTanks, JsonRequestBehavior.AllowGet);
        }

        #endregion JSON
    }
}
