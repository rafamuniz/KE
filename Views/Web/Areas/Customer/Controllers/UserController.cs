using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.User;
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
    public class UserController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
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
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Create()
        {
            CreateViewModel viewModel = new CreateViewModel();
            return View(LoadCreate(viewModel));
        }

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(LoadCreate(viewModel));
            }

            try
            {
                var user = new ApplicationUser { UserName = viewModel.Username, Email = viewModel.Address.Email, Name = viewModel.Name };
                var result = await UserManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, viewModel.Role);

                    if (result.Succeeded)
                    {
                        CustomerUser customerUser = new CustomerUser() { Id = Guid.Parse(user.Id), CustomerId = CustomerId };

                        if (!IsSite)
                        {
                            customerUser.Sites = viewModel.MapSites();
                        }
                        else
                        {
                            customerUser.Sites.Add(new CustomerUserSite { SiteId = SiteId });
                        }

                        Core.Entities.Address address = viewModel.MapAddress();
                        customerUser.Address = address;

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
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(LoadCreate(viewModel));
        }

        private CreateViewModel LoadCreate(CreateViewModel viewModel)
        {
            if (viewModel == null)
                viewModel = new CreateViewModel();

            LoadCustomerRoles();

            if (!IsSite)
            {
                List<Site> sites = LoadSites(CustomerId);
                viewModel.Sites = SiteViewModel.Map(sites);
            }
            else
            {
                viewModel.Sites.Add(new SiteViewModel() { Id = SiteId });
            }

            return viewModel;
        }
        #endregion Create

        #region Edit
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var customerUser = KEUnitOfWork.CustomerUserRepository.Get(id);

            if (customerUser == null)
            {
                AddErrors("User does not exist");
                return View("Index");
            }

            LoadCustomerRoles();
            EditViewModel viewModel = EditViewModel.Map(customerUser);
            var user = await UserManager.FindByIdAsync(viewModel.Id.ToString());
            viewModel.Name = user.Name;

            var roles = await UserManager.GetRolesAsync(viewModel.Id.ToString());
            viewModel.Role = roles.Single();

            // Address            
            viewModel.MapAddress(customerUser.Address);

            return View(viewModel);
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
                    LoadCustomerRoles();
                    return View(viewModel);
                }

                var user = await UserManager.FindByIdAsync(viewModel.Id.ToString());
                var customerUser = KEUnitOfWork.CustomerUserRepository.Get(viewModel.Id);

                if (customerUser == null || user == null)
                {
                    LoadCustomerRoles();
                    AddErrors("User does not exist");
                    return View("Index");
                }

                user.Name = viewModel.Name;
                user.Email = viewModel.Address.Email;
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
                            Core.Entities.Address address = viewModel.MapAddressVMToEntity(customerUser.Address);

                            KEUnitOfWork.AddressRepository.Update(address);
                            //KEUnitOfWork.CustomerRepository.Update(customer);
                            KEUnitOfWork.Complete();

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
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
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

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult ChangePassword(Guid id)
        {
            ChangePasswordViewModel viewModel = new ChangePasswordViewModel() { Id = id };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
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