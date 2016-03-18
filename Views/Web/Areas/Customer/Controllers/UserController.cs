using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Models;
using KarmicEnergy.Web.Areas.Customer.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    public class UserController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public async Task<ActionResult> Index()
        {
            List<CustomerUser> entities = KEUnitOfWork.CustomerUserRepository.GetsByCustomerId(CustomerId).ToList();
            var viewModels = ListViewModel.Map(entities);

            foreach (var vm in viewModels)
            {
                var id = vm.Id.ToString();
                var user = await UserManager.FindByIdAsync(id);
                vm.Name = user.Name;
                vm.UserName = user.UserName;
                vm.Email = user.Email;

                var roles = await UserManager.GetRolesAsync(id);
                vm.Role = roles.Single();
            }

            return View(viewModels);
        }
        #endregion Index

        #region Create
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult Create()
        {
            LoadCustomerRoles();
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
                LoadCustomerRoles();
                return View(viewModel);
            }

            try
            {
                var user = new ApplicationUser { UserName = viewModel.Email, Email = viewModel.Email, Name = viewModel.Name };
                var result = await UserManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, viewModel.Role);

                    if (result.Succeeded)
                    {
                        CustomerUser customerUser = new CustomerUser() { Id = Guid.Parse(user.Id), CustomerId = CustomerId };
                        KEUnitOfWork.CustomerUserRepository.Add(customerUser);
                        KEUnitOfWork.Complete();

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "User", new { area = "Customer" });
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

            LoadCustomerRoles();
            return View(viewModel);
        }
        #endregion Create

        #region Edit
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public async Task<ActionResult> Edit(Guid id)
        {
            LoadCustomerRoles();
            var customerUser = KEUnitOfWork.CustomerUserRepository.Get(id);

            if (customerUser == null)
            {
                AddErrors("User does not exist");
                return View("Index");
            }

            EditViewModel viewModel = EditViewModel.Map(customerUser);
            var user = await UserManager.FindByIdAsync(viewModel.Id.ToString());
            viewModel.Name = user.Name;
            viewModel.Email = user.Email;
            var roles = await UserManager.GetRolesAsync(viewModel.Id.ToString());
            viewModel.Role = roles.Single();

            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public async Task<ActionResult> Edit(EditViewModel viewModel)
        {
            var user = await UserManager.FindByIdAsync(viewModel.Id.ToString());

            if (user == null)
            {
                LoadCustomerRoles();
                AddErrors("User does not exist");
                return View("Index");
            }

            user.Name = viewModel.Name;
            user.Email = viewModel.Email;
            var result = await UserManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var roles = await UserManager.GetRolesAsync(user.Id);
                result = await UserManager.RemoveFromRolesAsync(user.Id, roles.ToArray());

                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, viewModel.Role);

                    if (result.Succeeded)
                    {
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "User", new { area = "Customer" });
                    }
                }
            }

            AddErrors(result);
            return View();
        }
        #endregion Edit

        #region Delete
        //
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var customerUser = KEUnitOfWork.CustomerUserRepository.Get(id);

            if (customerUser == null)
            {
                LoadCustomerRoles();
                AddErrors("Customer does not exist");
                return View("Index");
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

                return RedirectToAction("Index", "User", new { area = "Customer" });
            }

            AddErrors(result);

            return View("Index");
        }
        #endregion Delete

        #region Change Password

        [Authorize(Roles = "Customer, CustomerAdmin")]
        public ActionResult ChangePassword(Guid id)
        {
            ChangePasswordViewModel viewModel = new ChangePasswordViewModel() { Id = id };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin")]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                String resetToken = await UserManager.GeneratePasswordResetTokenAsync(viewModel.Id.ToString());
                var result = await UserManager.ResetPasswordAsync(viewModel.Id.ToString(), resetToken, viewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User", new { area = "Customer" });
                }
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(viewModel);
        }

        #endregion Change Password
    }
}