using KarmicEnergy.Web.Areas.Admin.ViewModels.Customer;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<Core.Entities.Customer> entities = KEUnitOfWork.CustomerRepository.GetAll().ToList();
            var viewModels = ListViewModel.Map(entities);
            return View(viewModels);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var user = new ApplicationUser { UserName = viewModel.Email, Email = viewModel.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, "Customer");

                    if (result.Succeeded)
                    {
                        Core.Entities.Customer customer = new Core.Entities.Customer() { Id = Guid.Parse(user.Id) };
                        KEUnitOfWork.CustomerRepository.Add(customer);
                        KEUnitOfWork.Complete();

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Customer");
                    }
                }

                AddErrors(result);
            }
            catch (DbEntityValidationException dbex)
            {
                AddErrors(dbex);
            }
            catch (Exception ex)
            {
                AddErrors(ex.Message);
            }

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            var customer = KEUnitOfWork.CustomerRepository.Get(id);

            if (customer == null)
            {
                AddErrors("Customer does not exist");
                return Index();
            }

            EditViewModel viewModel = EditViewModel.Map(customer);

            return View(viewModel);
        }

        //
        // POST: /Customer/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(EditViewModel viewModel)
        {
            var user = await UserManager.FindByIdAsync(viewModel.Id.ToString());

            if (user == null)
            {
                AddErrors("Customer does not exist");
                return Index();
            }

            user.Email = viewModel.Email;
            var result = await UserManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("Index", "Customer");
            }

            AddErrors(result);

            return Index();
        }

        //
        // GET: /Customer/Delete
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var customer = KEUnitOfWork.CustomerRepository.Get(id);

            if (customer == null)
            {
                AddErrors("Customer does not exist");
                return Index();
            }

            KEUnitOfWork.CustomerRepository.Remove(customer);

            var user = UserManager.FindByIdAsync(id.ToString()).Result;
            var result = await UserManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                KEUnitOfWork.Complete();

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("Index", "Customer");
            }

            AddErrors(result);

            return Index();
        }
    }
}