using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Models;
using KarmicEnergy.Web.ViewModels.User;
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
            List<CustomerUser> entities = KEUnitOfWork.CustomerUserRepository.GetAll().ToList();
            var viewModels = ListViewModel.Map(entities);
            return View(viewModels);
        }

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create()
        {
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
                        CustomerUser customerUser = new CustomerUser() { Name = viewModel.Name, Id = Guid.Parse(user.Id), Email = viewModel.Email };
                        KEUnitOfWork.CustomerUserRepository.Add(customerUser);
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

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Edit(Guid id)
        {
            var customer = KEUnitOfWork.CustomerRepository.Get(id);

            if (customer == null)
            {
                AddErrors("Customer does not exist");
                return Index();
            }

            //EditViewModel viewModel = EditViewModel.Map(customer);

            return View();
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public async Task<ActionResult> Edit(EditViewModel viewModel)
        {
            var customerUser = KEUnitOfWork.CustomerUserRepository.Get(viewModel.Id);

            if (customerUser == null)
            {
                AddErrors("Customer does not exist");
                return Index();
            }

            customerUser.Name = viewModel.Name;
            customerUser.Email = viewModel.Email;

            KEUnitOfWork.CustomerUserRepository.Update(customerUser);

            var user = await UserManager.FindByIdAsync(customerUser.Id.ToString());
            user.Email = viewModel.Email;
            var result = await UserManager.UpdateAsync(user);

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

        //
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var customerUser = KEUnitOfWork.CustomerUserRepository.Get(id);

            if (customerUser == null)
            {
                AddErrors("Customer does not exist");
                return Index();
            }

            KEUnitOfWork.CustomerUserRepository.Remove(customerUser);

            var user = UserManager.FindByIdAsync(customerUser.Id.ToString()).Result;
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