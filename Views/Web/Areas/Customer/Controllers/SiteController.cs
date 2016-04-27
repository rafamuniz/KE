using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Site;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class SiteController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Index()
        {
            List<Site> entities = KEUnitOfWork.SiteRepository.GetsByCustomerId(CustomerId).ToList();
            var viewModels = ListViewModel.Map(entities);
            return View(viewModels);
        }

        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create()
        {
            LoadStatuses();
            CreateViewModel viewModel = new CreateViewModel();
            return View(viewModel);
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
                    LoadStatuses();
                    return View(viewModel);
                }

                Site site = viewModel.Map();
                site.CustomerId = CustomerId;

                Core.Entities.Address address = viewModel.MapAddress();
                site.Address = address;

                KEUnitOfWork.SiteRepository.Add(site);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Site", new { area = "Customer" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadStatuses();
            return View(viewModel);
        }
        #endregion Create

        #region Edit

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(Guid id)
        {
            var site = KEUnitOfWork.SiteRepository.Get(id);

            if (site == null)
            {
                AddErrors("Site does not exist");
                return View("Index");
            }

            LoadStatuses();
            EditViewModel viewModel = new EditViewModel();
            viewModel.Map(site);
            viewModel.Map(site.Address);

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
                    LoadStatuses();
                    return View(viewModel);
                }

                var site = KEUnitOfWork.SiteRepository.Get(viewModel.Id);

                if (site == null)
                {
                    LoadStatuses();
                    AddErrors("Site does not exist");
                    return View("Index");
                }

                viewModel.MapVMToEntity(site);
                viewModel.MapVMToEntity(site.Address);

                KEUnitOfWork.SiteRepository.Update(site);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Site");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadStatuses();
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
            var site = KEUnitOfWork.SiteRepository.Get(id);

            if (site == null)
            {
                AddErrors("Site does not exist");
                return View("Index");
            }

            site.DeletedDate = DateTime.UtcNow;
            KEUnitOfWork.SiteRepository.Update(site);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Site");
        }
        #endregion Delete
    }
}
