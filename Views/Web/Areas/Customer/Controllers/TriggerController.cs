using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class TriggerController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Index()
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();

            if (!IsSite)
            {
                var triggers = KEUnitOfWork.TriggerRepository.GetAll().ToList();
                viewModels = ListViewModel.Map(triggers);
            }
            else
            {
                var triggers = KEUnitOfWork.TriggerRepository.GetAll().ToList();
                viewModels = ListViewModel.Map(triggers);
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

                Trigger trigger = new Trigger()
                {
                    Status = viewModel.Status,
                    SeverityId = viewModel.SeverityId,
                    SensorItemId = viewModel.SensorItemId,
                    MinValue = viewModel.MinValue,
                    MaxValue = viewModel.MaxValue
                };

                KEUnitOfWork.TriggerRepository.Add(trigger);
                KEUnitOfWork.Complete();

                return RedirectToAction("Gauge", "Tank", new { area = "Customer", tankId = viewModel.TankId });
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
                LoadTanks(CustomerId, SiteId);
            }

            if (Request.QueryString["TankId"] != null)
                viewModel.TankId = new Guid(Request.QueryString["TankId"]);

            LoadContacts(CustomerId);
            LoadUsers(CustomerId);
            LoadSeverities();
            LoadStatuses();

            return viewModel;
        }

        #endregion Create

        #region Delete
        //
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Delete(Guid id)
        {
            var trigger = KEUnitOfWork.TriggerRepository.Get(id);

            if (trigger == null)
            {
                AddErrors("Trigger does not exist");
                return View("Index");
            }

            trigger.DeletedDate = DateTime.UtcNow;
            KEUnitOfWork.TriggerRepository.Update(trigger);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Trigger");
        }

        #endregion Delete      

        [HttpGet]
        public ActionResult GetsTankBySiteId(Guid siteId)
        {
            var tanks = LoadTanks(CustomerId, siteId);
            SelectList obgTanks = new SelectList(tanks, "Id", "Name", 0);
            return Json(obgTanks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetsSensorByTankId(Guid tankId)
        {
            var sensors = LoadSensors(CustomerId, tankId);
            SelectList obgSensors = new SelectList(sensors, "Id", "Name", 0);
            return Json(obgSensors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetsSensorItemBySensorId(Guid sensorId)
        {
            var sensorItems = LoadSensorItems(sensorId);
            SelectList obgSensors = new SelectList(sensorItems, "Id", "Item.Name", 0);
            return Json(obgSensors, JsonRequestBehavior.AllowGet);
        }
    }
}