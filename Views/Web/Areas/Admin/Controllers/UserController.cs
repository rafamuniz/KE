using KarmicEnergy.Web.Areas.Admin.ViewModels.User;
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
    [Authorize]
    public class UserController : BaseController
    {
        #region Index
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Index()
        {
            List<ApplicationUser> users = GetUsersInRoles("Admin", "Operator");
            var viewModels = ListViewModel.Map(users);

            foreach (var vm in viewModels)
            {
                var id = vm.Id.ToString();
                var roles = await UserManager.GetRolesAsync(id);
                vm.Role = roles.Single();
            }

            return View(viewModels);
        }
        #endregion Index

        #region Create

        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Create()
        {
            CreateViewModel viewModel = new CreateViewModel();
            LoadAdminRoles();
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
                    LoadAdminRoles();
                    return View(viewModel);
                }

                user = new ApplicationUser { UserName = viewModel.UserName, Email = viewModel.Address.Email, Name = viewModel.Name };
                var result = await UserManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, viewModel.Role);

                    if (result.Succeeded)
                    {
                        Core.Entities.User userKE = viewModel.Map();
                        userKE.Id = Guid.Parse(user.Id);

                        Core.Entities.Address address = viewModel.MapAddress();
                        userKE.Address = address;

                        KEUnitOfWork.UserRepository.Add(userKE);
                        KEUnitOfWork.Complete();

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "User", new { area = "Admin" });
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

            LoadAdminRoles();
            return View(viewModel);
        }

        #endregion Create

        #region Edit
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Edit(Guid id)
        {
            LoadAdminRoles();
            var user = await UserManager.FindByIdAsync(id.ToString());
            var userKE = KEUnitOfWork.UserRepository.Get(id);

            if (user == null || userKE == null)
            {
                AddErrors("User does not exist");
                return View();
            }

            EditViewModel viewModel = EditViewModel.Map(user);
            viewModel.Address = EditViewModel.Map(userKE.Address);
            var roles = await UserManager.GetRolesAsync(user.Id);
            viewModel.Role = roles.Single();

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
                    LoadAdminRoles();
                    return View(viewModel);
                }

                user = await UserManager.FindByIdAsync(viewModel.Id.ToString());
                var userKE = KEUnitOfWork.UserRepository.Get(viewModel.Id);

                if (user == null || userKE == null)
                {
                    AddErrors("User does not exist");
                    LoadAdminRoles();
                    return View();
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
                            Core.Entities.Address address = viewModel.MapAddress(userKE.Address);
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

                            return RedirectToAction("Index", "User", new { area = "Admin" });
                        }
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
            LoadAdminRoles();
            return View();
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
                var user = await UserManager.FindByIdAsync(id.ToString());
                var userKE = KEUnitOfWork.UserRepository.Get(id);

                if (user == null || userKE == null)
                {
                    AddErrors("User does not exist");
                    return View();
                }

                var roleSuperAdmin = RoleManager.FindByNameAsync("SuperAdmin");

                // Dont delete SuperAdmin
                if (user.Roles.Where(x => x.RoleId == roleSuperAdmin.Result.Id).Any())
                {
                    AddErrors("User cannot be deleted");
                    return View();
                }

                KEUnitOfWork.UserRepository.Remove(userKE);
                var result = await UserManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    KEUnitOfWork.Complete();

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "User", new { area = "Admin" });
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
                    return RedirectToAction("Index", "User", new { area = "Admin" });
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