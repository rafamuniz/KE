using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.User;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Entities;
using System;
using System.Collections.Generic;
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
            List<CustomerUser> entities = KEUnitOfWork.CustomerUserRepository.GetsByCustomer(CustomerId).ToList();
            var viewModels = ListViewModel.Map(entities);

            foreach (var vm in viewModels)
            {
                var id = vm.Id.ToString();
                var user = await UserManager.FindByIdAsync(id);
                if (user != null)
                {
                    vm.Name = user.Name;
                    vm.UserName = user.UserName;
                    vm.Email = user.Email;

                    var roles = await UserManager.GetRolesAsync(id);
                    vm.Role = roles.Single();
                }
            }

            AddLog("Navigated to User View", LogTypeEnum.Info);
            return View(viewModels);
        }
        #endregion Index

        #region Create
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Create()
        {
            CreateViewModel viewModel = new CreateViewModel();
            AddLog("Navigated to User Create View", LogTypeEnum.Info);
            return View(LoadCreate(viewModel));
        }

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            ApplicationUser user = null;

            if (!ModelState.IsValid)
            {
                return View(LoadCreate(viewModel));
            }

            user = new ApplicationUser { UserName = viewModel.Username, Email = viewModel.Address.Email, Name = viewModel.Name };
            var result = await UserManager.CreateAsync(user, viewModel.Password);

            if (result.Succeeded)
            {
                result = await UserManager.AddToRoleAsync(user.Id, viewModel.Role);

                if (result.Succeeded)
                {
                    try
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
                        address.Id = Guid.NewGuid();
                        customerUser.AddressId = address.Id;
                        customerUser.Address = address;

                        KEUnitOfWork.CustomerUserRepository.Add(customerUser);
                        KEUnitOfWork.Complete();
                        AddLog("User Created", LogTypeEnum.Info);
                        return RedirectToAction("Index", "User", new { area = "Customer" });
                    }
                    catch (Exception ex)
                    {
                        if (user != null)
                            await UserManager.DeleteAsync(user);

                        AddErrors(ex);
                    }
                }
            }

            AddErrors(result);
            return View(LoadCreate(viewModel));
        }

        private CreateViewModel LoadCreate(CreateViewModel viewModel)
        {
            if (viewModel == null)
                viewModel = new CreateViewModel();

            LoadCustomerRoles();

            if (!IsSite)
            {
                List<Site> sites = LoadSites();
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

            EditViewModel viewModel = EditViewModel.Map(customerUser);
            var user = await UserManager.FindByIdAsync(viewModel.Id.ToString());
            viewModel.Name = user.Name;

            var roles = await UserManager.GetRolesAsync(viewModel.Id.ToString());
            viewModel.Role = roles.Single();

            // Address            
            viewModel.MapAddress(customerUser.Address);

            LoadCustomerRoles();

            if (!IsSite)
            {
                List<Site> sites = LoadSites();
                viewModel.MapSites(sites, customerUser.Sites.Where(x => x.DeletedDate == null).ToList());
            }
            else
            {
                viewModel.Sites.Add(new SiteViewModel() { Id = SiteId });
            }

            AddLog("Navigated to User Edit View", LogTypeEnum.Info);
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

                            if (!IsSite)
                            {
                                if (viewModel.Sites.Any())
                                {
                                    foreach (var item in viewModel.Sites)
                                    {
                                        var siteItem = customerUser.Sites.Where(x => x.SiteId == item.Id && x.DeletedDate == null).SingleOrDefault();

                                        if (item.IsSelected)
                                        {
                                            if (siteItem == null) // ADD
                                            {
                                                CustomerUserSite customerUserSite = new CustomerUserSite()
                                                {
                                                    CustomerUserId = customerUser.Id,
                                                    SiteId = item.Id
                                                };

                                                customerUser.Sites.Add(customerUserSite);
                                            }
                                        }
                                        else // DELETE
                                        {
                                            if (siteItem != null)
                                            {
                                                siteItem.DeletedDate = DateTime.UtcNow;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                customerUser.Sites.Add(new CustomerUserSite { SiteId = SiteId });
                            }

                            customerUser.Address = address;
                            KEUnitOfWork.CustomerUserRepository.Update(customerUser);
                            KEUnitOfWork.Complete();

                            AddLog("User Updated", LogTypeEnum.Info);
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

            LoadCustomerRoles();
            return View(viewModel);
        }

        private EditViewModel LoadEdit(EditViewModel viewModel, CustomerUser customerUser)
        {
            if (viewModel == null)
                viewModel = new EditViewModel();

            LoadCustomerRoles();

            if (!IsSite)
            {
                List<Site> sites = LoadSites();
                viewModel.MapSites(sites, customerUser.Sites);
            }
            else
            {
                viewModel.Sites.Add(new SiteViewModel() { Id = SiteId });
            }

            return viewModel;
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

            user.DeletedDate = DateTime.UtcNow;
            var result = await UserManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                KEUnitOfWork.Complete();

                AddLog("User Deleted", LogTypeEnum.Info);
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
            AddLog("Navigated to User Change Password View", LogTypeEnum.Info);
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
                    var user = await UserManager.FindByIdAsync(viewModel.Id.ToString());
                    user.LastModifiedDate = DateTime.UtcNow;

                    var userResult = await UserManager.UpdateAsync(user);
                    if (userResult.Succeeded)
                    {
                        AddLog("User Password changed", LogTypeEnum.Info);
                        return RedirectToAction("Index", "User", new { area = "Customer" });
                    }
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