using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Tank;
using KarmicEnergy.Web.Controllers;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    public class TankController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Index()
        {
            var tanks = KEUnitOfWork.TankRepository.GetsByCustomerId(CustomerId).ToList();
            var viewModels = ListViewModel.Map(tanks);
            return View(viewModels);
        }
        #endregion Index

        #region Dashoard
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Dashboard()
        {
            ViewBag.CustomerId = CustomerId;
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetTankHTML()
        {
            return PartialView("_TankData");
        }
        #endregion Dashoard

        #region Create

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create()
        {
            LoadSites(CustomerId);
            var tankModels = LoadTankModels();
            CreateViewModel viewModel = new CreateViewModel() { TankModels = tankModels };
            LoadStatuses();
            return View(viewModel);
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
                LoadSites(CustomerId);
                LoadTankModels();
                LoadStatuses();
                return View(viewModel);
            }

            try
            {
                //Tank tank = new Tank()
                //{
                //    Name = viewModel.Name,
                //    SiteId = viewModel.SiteId,
                //    TankModelId = viewModel.TankModelId,
                //    Description = viewModel.Description,
                //    Status = viewModel.Status
                //};
                var tank = viewModel.Map();
                KEUnitOfWork.TankRepository.Add(tank);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Tank");
            }
            catch (DbEntityValidationException dbex)
            {
                AddErrors(dbex);
            }
            catch (Exception ex)
            {
                AddErrors(ex.Message);
            }

            LoadSites(CustomerId);
            LoadTankModels();
            LoadStatuses();
            return View(viewModel);
        }
        #endregion Create

        #region Edit
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(Guid id)
        {
            var tank = KEUnitOfWork.TankRepository.Get(id);

            if (tank == null)
            {
                AddErrors("Tank does not exist");
                return View("Index");
            }

            LoadStatuses();
            LoadSites(CustomerId);
            var tankModels = LoadTankModels();
            EditViewModel viewModel = EditViewModel.Map(tank);
            viewModel.TankModels = tankModels;
            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(EditViewModel viewModel)
        {
            var tank = KEUnitOfWork.TankRepository.Get(viewModel.Id);

            if (tank == null)
            {
                LoadSites(CustomerId);
                LoadTankModels();
                LoadStatuses();
                AddErrors("Tank does not exist");
                return View("Index");
            }

            tank.Name = viewModel.Name;
            tank.Description = viewModel.Description;
            tank.Status = viewModel.Status;
            tank.SiteId = viewModel.SiteId;
            tank.TankModelId = viewModel.TankModelId;

            KEUnitOfWork.TankRepository.Update(tank);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Tank");
        }
        #endregion Edit

        #region Delete
        //
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Delete(Guid id)
        {
            var tank = KEUnitOfWork.TankRepository.Get(id);

            if (tank == null)
            {
                AddErrors("Tank does not exist");
                return View("Index");
            }

            KEUnitOfWork.TankRepository.Remove(tank);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Tank");
        }

        #endregion Delete
    }
}
