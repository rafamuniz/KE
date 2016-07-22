using KarmicEnergy.Web.Areas.Admin.ViewModels.User;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
            List<ApplicationUser> users = GetUsersInRoles("SuperAdmin", "Admin", "User");
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
                    try
                    {
                        Core.Entities.User userKE = viewModel.Map();
                        userKE.Id = Guid.Parse(user.Id);

                        Core.Entities.Address address = viewModel.MapAddress();
                        address.Id = Guid.NewGuid();
                        userKE.Address = address;
                        userKE.AddressId = address.Id;

                        KEUnitOfWork.UserRepository.Add(userKE);
                        KEUnitOfWork.Complete();

                        return RedirectToAction("Index", "User", new { area = "Admin" });
                    }
                    catch (Exception ex)
                    {
                        if (user != null)
                            await UserManager.DeleteAsync(user);

                        AddErrors(ex);
                    }
                }
            }
            else
            {
                AddErrors(result);
            }

            LoadAdminRoles();
            return View(viewModel);
        }

        #endregion Create

        #region Edit
        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Edit(Guid id)
        {
            var user = UserManager.FindById(id.ToString());
            var userKE = KEUnitOfWork.UserRepository.Get(id);

            if (user == null || userKE == null)
            {
                LoadAdminRoles();
                AddErrors("User does not exist");
                return View();
            }

            EditViewModel viewModel = EditViewModel.Map(user);

            // Address            
            viewModel.Map(userKE.Address);

            var roles = UserManager.GetRoles(user.Id);
            viewModel.Role = roles.Single();
            LoadAdminRoles();

            //AddLog("Navigated to User Edit View", LogTypeEnum.Info);
            return View(viewModel);
        }

        //
        // POST: /Customer/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Edit(EditViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadAdminRoles();
                    return View(viewModel);
                }

                var user = UserManager.FindById(viewModel.Id.ToString());
                var userKE = KEUnitOfWork.UserRepository.Get(viewModel.Id);

                if (user == null || userKE == null)
                {
                    LoadAdminRoles();
                    AddErrors("User does not exist");
                    return View();
                }

                user.Name = viewModel.Name;
                user.Email = viewModel.Address.Email;
                var result = UserManager.Update(user);

                if (result.Succeeded)
                {
                    var roles = UserManager.GetRoles(user.Id);
                    result = UserManager.RemoveFromRoles(user.Id, roles.ToArray());

                    if (result.Succeeded)
                    {
                        result = UserManager.AddToRole(user.Id, viewModel.Role);

                        if (result.Succeeded)
                        {
                            Core.Entities.Address address = viewModel.MapAddress(userKE.Address);

                            KEUnitOfWork.AddressRepository.Update(address);
                            KEUnitOfWork.Complete();

                            return RedirectToAction("Index", "User", new { area = "Admin" });
                        }
                    }
                }
                else
                {
                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadAdminRoles();
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

                user.DeletedDate = DateTime.UtcNow;
                var result = await UserManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    userKE.DeletedDate = DateTime.UtcNow;
                    KEUnitOfWork.UserRepository.Update(userKE);
                    KEUnitOfWork.Complete();

                    return RedirectToAction("Index", "User", new { area = "Admin" });
                }
                else
                {
                    AddErrors(result);
                }
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
                    var user = await UserManager.FindByIdAsync(viewModel.Id.ToString());
                    user.LastModifiedDate = DateTime.UtcNow;

                    var userResult = await UserManager.UpdateAsync(user);
                    if (userResult.Succeeded)
                    {
                        return RedirectToAction("Index", "User", new { area = "Admin" });
                    }
                }
                else
                {
                    AddErrors(result);
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