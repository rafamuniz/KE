using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor;
using KarmicEnergy.Web.Controllers;
using System;
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
            LoadStatuses();
            LoadTanks(CustomerId);
            LoadSensorTypes();
            return View();
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
                LoadTanks(CustomerId);
                LoadSensorTypes();
                LoadStatuses();
                return View(viewModel);
            }

            try
            {
                Sensor sensor = new Sensor()
                {
                    Name = viewModel.Name,
                    TankId = viewModel.TankId,
                    SensorTypeId = viewModel.SensorTypeId,
                    Status = viewModel.Status
                };

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
                LoadTanks(CustomerId);
                LoadSensorTypes();
                LoadStatuses();
                AddErrors("Sensor does not exist");
                return View("Index");
            }

            return View();
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(EditViewModel viewModel)
        {
            var sensor = KEUnitOfWork.SensorRepository.Get(viewModel.Id);

            if (sensor == null)
            {
                LoadTanks(CustomerId);
                LoadStatuses();
                LoadSensorTypes();
                AddErrors("Tank does not exist");
                return View("Index");
            }

            sensor.Name = viewModel.Name;
            sensor.SensorTypeId = viewModel.SensorTypeId;
            sensor.TankId = viewModel.TankId;
            sensor.Status = viewModel.Status;

            KEUnitOfWork.SensorRepository.Update(sensor);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Sensor");
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

            return RedirectToAction("Index", "Sensor");
        }

        #endregion Delete
    }
}
