using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.SensorGroup;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class SensorGroupController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Index()
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();

            if (!IsSite)
            {
                var groups = KEUnitOfWork.GroupRepository.GetAllActive().ToList();
                viewModels = ListViewModel.Map(groups);
            }
            else
            {
                var groups = KEUnitOfWork.GroupRepository.GetsBySiteId(SiteId).ToList();
                viewModels = ListViewModel.Map(groups);
            }

            return View(viewModels);
        }

        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Create()
        {
            return View(LoadDefault());
        }

        #endregion Create

        #region Add

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Add(Guid groupId)
        {
            CreateViewModel viewModel = LoadDefault();
            var group = KEUnitOfWork.GroupRepository.Find(x => x.Id == groupId).Single();
            var sensorGroups = KEUnitOfWork.SensorGroupRepository.Find(x => x.GroupId == groupId).ToList();

            viewModel.SiteId = group.SiteId;
            viewModel.Sensors = SensorGroupViewModel.Map(sensorGroups);

            return View("Create", viewModel);
        }

        private CreateViewModel LoadDefault()
        {
            CreateViewModel viewModel = new CreateViewModel();

            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
                LoadTanks(CustomerId, SiteId);
            }

            return viewModel;
        }

        //
        // POST: /SensorGroup/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Add(CreateViewModel viewModel)
        {
            Group group = new Group();

            try
            {
                if (!ModelState.IsValid)
                {
                    LoadDefault();
                    return View("Create", viewModel);
                }

                if (viewModel.GroupId.HasValue) // Update Group
                {
                    group = KEUnitOfWork.GroupRepository.Get(viewModel.GroupId.Value);
                }

                if (!IsSite)
                {
                    group.SiteId = viewModel.SiteId.Value;
                }
                else
                {
                    group.SiteId = SiteId;
                }

                SensorGroup sensorGroup = new SensorGroup();
                sensorGroup.SensorId = viewModel.SensorId.Value;
                sensorGroup.Weight = viewModel.Weight.Value;

                group.SensorGroups.Add(sensorGroup);

                if (viewModel.GroupId.HasValue)
                {
                    KEUnitOfWork.GroupRepository.Update(group);
                }
                else
                {
                    KEUnitOfWork.GroupRepository.Add(group);
                }

                KEUnitOfWork.Complete();

                return RedirectToAction("Add", new { GroupId = sensorGroup.GroupId });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadDefault();
            return View("Create", viewModel);
        }

        #endregion Add

        #region Edit

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Edit(Guid id)
        {
            return RedirectToAction("Add", new { GroupId = id });
        }

        #endregion Edit

        #region Delete
        //
        // GET: /SensorGroup/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Delete(Guid id)
        {
            var group = KEUnitOfWork.GroupRepository.Get(id);

            if (group == null)
            {
                AddErrors("Group does not exist");
                return View("Index");
            }

            KEUnitOfWork.GroupRepository.Remove(group);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index");
        }

        //
        // GET: /SensorGroup/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult DeleteSensor(Guid id)
        {
            var sensor = KEUnitOfWork.SensorGroupRepository.Get(id);

            if (sensor == null)
            {
                AddErrors("Sensor does not exist");
                return View("Index");
            }

            var groupId = sensor.GroupId;
            KEUnitOfWork.SensorGroupRepository.Remove(sensor);
            KEUnitOfWork.Complete();

            return RedirectToAction("Add", new { GroupId = groupId });
        }

        #endregion Delete

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsTankBySiteId(Guid siteId)
        {
            var tanks = LoadTanks(CustomerId, siteId);
            SelectList obgTanks = new SelectList(tanks, "Id", "Name", 0);
            return Json(obgTanks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsSensorByTankId(Guid tankId)
        {
            var sensors = LoadSensors(CustomerId, tankId);
            SelectList obgSensors = new SelectList(sensors, "Id", "Name", 0);
            return Json(obgSensors, JsonRequestBehavior.AllowGet);
        }
    }
}
