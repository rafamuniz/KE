using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    public class SensorController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Index()
        {
            var sensors = KEUnitOfWork.SensorRepository.GetsByCustomerId(CustomerId);
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
            CreateViewModel viewModel = new CreateViewModel();

            LoadStatuses();
            var items = LoadItems();
            LoadTanks(CustomerId);
            LoadSensorTypes();

            viewModel.AvailableItems = ItemViewModel.Map(items);

            return View(viewModel);
        }

        //[Authorize(Roles = "Customer, CustomerAdmin")]
        //public ActionResult Create(Guid tankId)
        //{
        //    LoadStatuses();
        //    LoadTanks(CustomerId);
        //    LoadSensorTypes();
        //    return View();
        //}

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadTanks(CustomerId);
                LoadSensorTypes();
                LoadStatuses();
                LoadItems();
                return View(viewModel);
            }

            try
            {
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
                    sensor.SensorItems = new List<SensorItem>();

                    foreach (var item in viewModel.Items)
                    {
                        SensorItem sensorItem = new SensorItem()
                        {
                            ItemId = Int32.Parse(item)
                        };

                        sensor.SensorItems.Add(sensorItem);
                    }
                }

                KEUnitOfWork.SensorRepository.Add(sensor);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Sensor");
            }
            catch (DbEntityValidationException dbex)
            {
                AddErrors(dbex);
            }
            catch (Exception ex)
            {
                AddErrors(ex.Message);
            }

            LoadSensorTypes();
            LoadItems();
            LoadTanks(CustomerId);
            LoadStatuses();
            return View(viewModel);
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

            var items = LoadItems();
            viewModel.AvailableItems = ItemViewModel.Map(items);

            if (sensor.SensorItems.Any())
            {
                viewModel.SelectedItems = new List<ItemViewModel>();

                foreach (var item in sensor.SensorItems)
                {
                    foreach (var avalItem in viewModel.AvailableItems)
                    {
                        if (item.ItemId == avalItem.Id)
                            avalItem.IsSelected = true;
                    }

                }
            }

            LoadStatuses();
            LoadTanks(CustomerId);
            LoadSensorTypes();

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
                    LoadTanks(CustomerId);
                    LoadStatuses();
                    LoadSensorTypes();
                    var itemsVal = LoadItems();
                    viewModel.AvailableItems = ItemViewModel.Map(itemsVal);

                    AddErrors("Tank does not exist");
                    return View(viewModel);
                }

                Sensor sensor = KEUnitOfWork.SensorRepository.Get(viewModel.Id);

                sensor = new Sensor()
                {
                    Name = viewModel.Name,
                    TankId = viewModel.TankId,
                    SensorTypeId = viewModel.SensorTypeId,
                    Status = viewModel.Status,
                    Reference = viewModel.Reference
                };

                if (viewModel.Items.Any())
                {
                    sensor.SensorItems = new List<SensorItem>();

                    foreach (var item in viewModel.Items)
                    {
                        SensorItem sensorItem = new SensorItem()
                        {
                            ItemId = Int32.Parse(item)
                        };

                        sensor.SensorItems.Add(sensorItem);
                    }
                }

                KEUnitOfWork.SensorRepository.Update(sensor);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException dbex)
            {
                AddErrors(dbex);
            }
            catch (Exception ex)
            {
                AddErrors(ex.Message);
            }

            LoadTanks(CustomerId);
            LoadStatuses();
            LoadSensorTypes();
            var items = LoadItems();
            viewModel.AvailableItems = ItemViewModel.Map(items);
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
            var sensor = KEUnitOfWork.SensorRepository.Get(id);

            if (sensor == null)
            {
                AddErrors("Sensor does not exist");
                return View("Index");
            }

            KEUnitOfWork.SensorRepository.Remove(sensor);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index");
        }

        #endregion Delete
    }
}
