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
            var tanks = KEUnitOfWork.TankRepository.GetAll().ToList();
            var viewModels = ListViewModel.Map(tanks);
            return View(viewModels);
        }
        #endregion Index

        #region Dashoard
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Dashboard()
        {
            return View();
        }
        #endregion Dashoard

        #region Create

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create()
        {
            LoadSites();
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
                LoadSites();
                LoadTankModels();
                LoadStatuses();
                return View(viewModel);
            }

            try
            {
                Tank tank = new Tank() { Name = viewModel.Name, SiteId = viewModel.SiteId, TankModelId = viewModel.TankModelId, Description = viewModel.Description, Status = viewModel.Status };
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

            LoadSites();
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
                LoadSites();
                LoadTankModels();
                LoadStatuses();
                AddErrors("Tank does not exist");
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
            var tank = KEUnitOfWork.TankRepository.Get(viewModel.Id);

            if (tank == null)
            {
                LoadSites();
                LoadTankModels();
                LoadStatuses();
                AddErrors("Tank does not exist");
                return View("Index");
            }

            //site.Name = viewModel.Name;
            //site.IPAddress = viewModel.IPAddress;

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
