using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Contact;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class ContactController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Index()
        {
            List<Contact> entities = KEUnitOfWork.ContactRepository.GetsByCustomerId(CustomerId).ToList();
            var viewModels = ListViewModel.Map(entities);

            AddLog("Navigated to Contact View", LogTypeEnum.Info);
            return View(viewModels);
        }
        #endregion Index

        #region Create
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Create()
        {
            CreateViewModel viewModel = new CreateViewModel();
            AddLog("Navigated to Create Contact View", LogTypeEnum.Info);
            return View(viewModel);
        }

        //
        // POST: /Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                Core.Entities.Contact contact = viewModel.Map();
                contact.Id = Guid.NewGuid();
                contact.CustomerId = CustomerId;

                Core.Entities.Address address = viewModel.MapAddress();
                contact.Address = address;

                KEUnitOfWork.ContactRepository.Add(contact);
                KEUnitOfWork.Complete();

                AddLog("Created a contact", LogTypeEnum.Info);

                return RedirectToAction("Index", "Contact", new { area = "Customer" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(viewModel);
        }
        #endregion Create

        #region Edit
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var contact = KEUnitOfWork.ContactRepository.Get(id);

                if (contact == null)
                {
                    AddErrors("Contact does not exist");
                    return View("Index");
                }

                EditViewModel viewModel = new EditViewModel();
                viewModel.MapEntityToVM(contact);

                // Address
                viewModel.MapEntityToVM(contact.Address);
                AddLog("Navigated to Edit Contact View", LogTypeEnum.Info);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return RedirectToAction("Index");
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Edit(EditViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var contact = KEUnitOfWork.ContactRepository.Get(viewModel.Id);

                if (contact == null)
                {
                    AddErrors("Contact does not exist");
                    return RedirectToAction("Index");
                }

                viewModel.MapVMToEntity(contact);
                viewModel.MapVMToEntity(contact.Address);

                KEUnitOfWork.ContactRepository.Update(contact);
                KEUnitOfWork.Complete();

                AddLog("Updated a Contact", LogTypeEnum.Info);

                return RedirectToAction("Index", "Contact", new { area = "Customer" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(viewModel);
        }
        #endregion Edit

        #region Delete
        //
        // GET: /Contact/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var contact = KEUnitOfWork.ContactRepository.Get(id);

                if (contact == null)
                {
                    AddErrors("Contact does not exist");
                    return View("Index");
                }

                contact.DeletedDate = DateTime.UtcNow;
                KEUnitOfWork.ContactRepository.Update(contact);
                KEUnitOfWork.Complete();

                AddLog("Deleted a Contact", LogTypeEnum.Info);

                return RedirectToAction("Index", "Contact", new { area = "Customer" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View("Index");
        }
        #endregion Delete      
    }
}