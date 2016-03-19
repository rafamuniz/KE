using KarmicEnergy.Web.Areas.Admin.ViewModels.TankModel;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Munizoft.Extensions;

namespace KarmicEnergy.Web.Areas.Admin.Controllers
{
    public class TankModelController : BaseController
    {
        #region Index
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {            
            var tankModels = KEUnitOfWork.TankModelRepository.GetAll().ToList();
            var viewModels = ListViewModel.Map(tankModels);
            return View(viewModels);
        }
        #endregion Index

        #region Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            LoadStatuses();
            return View();
        }

        //
        // POST: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadStatuses();
                return View(viewModel);
            }

            try
            {
                return RedirectToAction("Index", "TankModel", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(viewModel);
        }
        #endregion Create

        #region Edit
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Int32 id)
        {
            LoadStatuses();
            return View();
        }

        //
        // POST: /Customer/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(EditViewModel viewModel)
        {
            return View();
        }
        #endregion Edit

        #region Delete
        //
        // GET: /Customer/Delete
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Int32 id)
        {
            var tankModel = KEUnitOfWork.TankModelRepository.Get(id);

            if (tankModel == null)
            {
                AddErrors("Tank Model does not exist");
                return View("Index");
            }

            KEUnitOfWork.TankModelRepository.Remove(tankModel);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "TankModel", new { area = "Admin" });
        }
        #endregion Delete        
    }
}