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

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
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

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(Guid id)
        {
            var site = KEUnitOfWork.SiteRepository.Get(id);

            if (site == null)
            {
                LoadStatuses();
                AddErrors("Site does not exist");
                return View("Index");
            }

            return View();
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public async Task<ActionResult> Edit(EditViewModel viewModel)
        {
            var site = KEUnitOfWork.SiteRepository.Get(viewModel.Id);

            if (site == null)
            {
                LoadStatuses();
                AddErrors("Site does not exist");
                return View("Index");
            }

            site.Name = viewModel.Name;
            site.IPAddress = viewModel.IPAddress;

            KEUnitOfWork.SiteRepository.Update(site);
            KEUnitOfWork.Complete();

            return RedirectToAction("Index", "Site");
        }

        //
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public async Task<ActionResult> Delete(Guid id)
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
    }
}
