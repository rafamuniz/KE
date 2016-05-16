using KarmicEnergy.Web.Areas.Admin.ViewModels.Customer;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {
        #region Index
        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Index()
        {
            List<ApplicationUser> users = GetUsersInRole("Customer");
            var viewModels = ListViewModel.Map(users);
            return View(viewModels);
        }
        #endregion Index

        #region Create

        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Create()
        {
            CreateViewModel viewModel = new CreateViewModel();
            return View(viewModel);
        }

        //
        // POST: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            ApplicationUser user = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                user = new ApplicationUser { UserName = viewModel.UserName, Email = viewModel.Address.Email, Name = viewModel.Name };
                var result = await UserManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, "Customer");

                    if (result.Succeeded)
                    {
                        Core.Entities.Customer customer = viewModel.Map();
                        customer.Id = Guid.Parse(user.Id);

                        Core.Entities.Address address = viewModel.MapAddress();
                        customer.Address = address;

                        KEUnitOfWork.CustomerRepository.Add(customer);
                        KEUnitOfWork.Complete();

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Customer", new { area = "Admin" });
                    }
                }

                AddErrors(result);
            }
            catch (Exception ex)
            {
                if (user != null)
                    await UserManager.DeleteAsync(user);

                AddErrors(ex);
            }

            return View(viewModel);
        }

        #endregion Create

        #region Edit

        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var user = await UserManager.FindByIdAsync(id.ToString());
            var customer = KEUnitOfWork.CustomerRepository.Get(id);

            if (customer == null || user == null)
            {
                AddErrors("Customer does not exist");
                return View();
            }

            EditViewModel viewModel = EditViewModel.Map(user);
            viewModel.Address = EditViewModel.Map(customer.Address);

            return View(viewModel);
        }

        //
        // POST: /Customer/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Edit(EditViewModel viewModel)
        {
            ApplicationUser user = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                user = await UserManager.FindByIdAsync(viewModel.Id.ToString());
                var customer = KEUnitOfWork.CustomerRepository.Get(viewModel.Id);

                if (customer == null || user == null)
                {
                    AddErrors("Customer does not exist");
                    return View(viewModel);
                }

                user.Name = viewModel.Name;
                user.Email = viewModel.Address.Email;
                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    Core.Entities.Address address = viewModel.MapAddress(customer.Address);
                    //address.Id = customer.Address.Id;
                    //address.RowVersion = customer.Address.RowVersion;

                    KEUnitOfWork.AddressRepository.Update(address);
                    //KEUnitOfWork.CustomerRepository.Update(customer);
                    KEUnitOfWork.Complete();

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Customer");
                }

                AddErrors(result);
            }
            catch (Exception ex)
            {
                if (user != null)
                    await UserManager.DeleteAsync(user);

                AddErrors(ex);
            }

            return View(viewModel);
        }
        #endregion Edit

        #region Delete
        //
        // GET: /Customer/Delete
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var customer = KEUnitOfWork.CustomerRepository.Get(id);
                var user = await UserManager.FindByIdAsync(id.ToString());

                if (customer == null || user == null)
                {
                    AddErrors("Customer does not exist");
                    return View();
                }

                KEUnitOfWork.CustomerRepository.Remove(customer);
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
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View();
        }
        #endregion Delete

        #region Change Password

        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult ChangePassword(Guid id)
        {
            ChangePasswordViewModel viewModel = new ChangePasswordViewModel() { Id = id };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Admin")]
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
                    return RedirectToAction("Index", "Customer", new { area = "Admin" });
                }

                AddErrors(result);
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