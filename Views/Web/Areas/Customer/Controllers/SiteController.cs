using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Models;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Site;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
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

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create()
        {
            LoadStatuses();
            return View();
        }
        #endregion Index

        #region Create
        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadStatuses();
                return View(viewModel);
            }

            try
            {
                Site site = new Site() { Name = viewModel.Name, IPAddress = viewModel.IPAddress, CustomerId = CustomerId, Status = viewModel.Status };
                KEUnitOfWork.SiteRepository.Add(site);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Site");
            }
            catch (DbEntityValidationException dbex)
            {
                AddErrors(dbex);
            }
            catch (Exception ex)
            {
                AddErrors(ex.Message);
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
            LoadStatuses();

            if (site == null)
            {
                AddErrors("Site does not exist");
                return View("Index");
            }

            EditViewModel viewModel = EditViewModel.Map(site);
            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(EditViewModel viewModel)
        {
            var site = KEUnitOfWork.SiteRepository.Get(viewModel.Id);

            if (site == null)
            {
                LoadStatuses();
                AddErrors("Site does not exist");
                return View("Index");
            }

            //site.Name = viewModel.Name;
            //site.IPAddress = viewModel.IPAddress;
            //site.Status = viewModel.Status;
            Site model = viewModel.MapUpdate(site);

            KEUnitOfWork.SiteRepository.Update(model);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Site");
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

            KEUnitOfWork.SiteRepository.Remove(site);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Site");
        }
        #endregion Delete
    }
}
