using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.User;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Munizoft.Extensions;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Index()
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();
            List<CustomerUser> customerUsers = KEUnitOfWork.CustomerUserRepository.GetsByCustomer(CustomerId).ToList();
            List<Contact> contacts = KEUnitOfWork.ContactRepository.GetsByCustomer(CustomerId).ToList();

            viewModels.AddRange(ListViewModel.Map(customerUsers));

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

            viewModels.AddRange(ListViewModel.Map(contacts));

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
            Core.Entities.Address address = viewModel.MapAddress();
            address.Id = Guid.NewGuid();

            if (viewModel.Role != "Contact")
            {
                if (!ModelState.IsValid)
                {
                    return View(LoadCreate(viewModel));
                }

                ApplicationUser user = new ApplicationUser { UserName = viewModel.User.Username, Email = viewModel.Address.Email, Name = viewModel.Name };
                var result = await UserManager.CreateAsync(user, viewModel.User.Password);

                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, viewModel.Role);

                    if (result.Succeeded)
                    {
                        try
                        {
                            CustomerUser customerUser = new CustomerUser() { Id = Guid.Parse(user.Id), CustomerId = CustomerId };
                            customerUser.AddressId = address.Id;
                            customerUser.Address = address;

                            KEUnitOfWork.CustomerUserRepository.Add(customerUser);
                            KEUnitOfWork.Complete();
                        }
                        catch (Exception ex)
                        {
                            if (user != null)
                                await UserManager.DeleteAsync(user);

                            AddErrors(ex);
                            return View(LoadCreate(viewModel));
                        }
                    }
                }
                else
                {
                    AddErrors(result);
                    return View(LoadCreate(viewModel));
                }
            }
            else if (viewModel.Role == "Contact")
            {
                try
                {
                    var userModelState = ModelState.Where(x => x.Key.StartsWith("User.")).ToList();

                    foreach (var ums in userModelState)
                    {
                        ModelState[ums.Key].Errors.Clear();
                    }

                    if (!ModelState.IsValid)
                    {
                        return View(LoadCreate(viewModel));
                    }

                    Core.Entities.Contact contact = viewModel.Map();
                    contact.Id = Guid.NewGuid();
                    contact.CustomerId = CustomerId;

                    contact.Address = address;
                    contact.AddressId = address.Id;

                    KEUnitOfWork.ContactRepository.Add(contact);
                    KEUnitOfWork.Complete();
                }
                catch (Exception ex)
                {
                    AddErrors(ex);
                    return View(LoadCreate(viewModel));
                }
            }
            else
            {
                ModelState.Clear();
                AddErrors("Role is required");
                return View(LoadCreate(viewModel));
            }

            AddLog("User Created", LogTypeEnum.Info);
            return RedirectToAction("Index", "User", new { area = "Customer" });
        }

        private CreateViewModel LoadCreate(CreateViewModel viewModel)
        {
            if (viewModel == null)
                viewModel = new CreateViewModel();

            LoadCustomerRoles();

            if (!IsSite)
            {
                List<Site> sites = LoadSites();
                List<SiteViewModel> siteSelected = new List<SiteViewModel>();

                if (viewModel.User.Sites.Any())
                {
                    siteSelected = viewModel.User.Sites;
                }

                viewModel.User.Sites = SiteViewModel.Map(sites);

                foreach (var ss in siteSelected)
                {
                    if (ss.IsSelected)
                    {
                        viewModel.User.Sites.Where(x => x.Id == ss.Id).SingleOrDefault().IsSelected = true;
                    }
                }
            }
            else
            {
                viewModel.User.Sites.Add(new SiteViewModel() { Id = SiteId });
            }

            return viewModel;
        }

        #endregion Create

        #region Edit

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Edit(Guid id)
        {
            EditViewModel viewModel = new EditViewModel();

            using (var unitOfWork = Core.Persistence.KEUnitOfWork.Create())
            {
                var customerUser = unitOfWork.CustomerUserRepository.Get(id);
                var contact = unitOfWork.ContactRepository.Get(id);

                if (customerUser == null && contact == null)
                {
                    return View("Index");
                }

                if (customerUser != null) // CustomerUser
                {
                    viewModel.Map(customerUser);
                    var user = await UserManager.FindByIdAsync(viewModel.Id.ToString());
                    viewModel.Map(user);

                    var roles = await UserManager.GetRolesAsync(viewModel.Id.ToString());
                    viewModel.Role = roles.Single();

                    // Address            
                    viewModel.MapAddress(customerUser.Address);
                    LoadCustomerRolesWithoutContact();

                    if (!IsSite)
                    {
                        List<Site> sites = LoadSites();
                        if (customerUser != null)
                        {
                            viewModel.MapSites(sites, customerUser.Sites.Where(x => x.DeletedDate == null).ToList());
                        }
                        else
                        {
                            viewModel.Sites = SiteViewModel.Map(sites);
                        }
                    }
                    else
                    {
                        viewModel.Sites.Add(new SiteViewModel() { Id = SiteId });
                    }
                }
                else // Contact
                {
                    viewModel = EditViewModel.Map(contact);
                    // Address            
                    viewModel.MapAddress(contact.Address);
                }
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
                var user = await UserManager.FindByIdAsync(viewModel.Id.ToString());
                var customerUser = KEUnitOfWork.CustomerUserRepository.Get(viewModel.Id);
                var contact = KEUnitOfWork.ContactRepository.Get(viewModel.Id);

                if ((customerUser == null || user == null) && contact == null)
                {
                    InitEditView(viewModel, customerUser);
                    AddErrors("User does not exist");
                    return View(viewModel);
                }

                if (user != null && viewModel.Role != "Contact") // User - Dont change Role
                {
                    if (!ModelState.IsValid)
                    {
                        LoadCustomerRoles();
                        return View(viewModel);
                    }

                    await UpdateUser(viewModel, user, customerUser);
                }
                else if (contact != null && viewModel.Role == "Contact") // Contact - Dont change Role
                {
                    var userModelState = ModelState.Where(x => x.Key.StartsWith("User.")).ToList();

                    foreach (var ums in userModelState)
                    {
                        ModelState[ums.Key].Errors.Clear();
                    }

                    if (!ModelState.IsValid)
                    {
                        LoadCustomerRoles();
                        return View(viewModel);
                    }

                    UpdateContact(viewModel, contact);
                }
                else if (user != null && viewModel.Role == "Contact") // User - change Role
                {
                    await UpdateUserToContact(viewModel, user, customerUser);
                }
                else if (contact != null && viewModel.Role != "Contact") // Contact - change Role
                {
                    UpdateContactToUser(viewModel, contact);
                }
            }
            catch (Exception ex)
            {
                AddErrors(ex);
                LoadCustomerRoles();
                return View(viewModel);
            }

            AddLog("User Updated", LogTypeEnum.Info);
            return RedirectToAction("Index", "User", new { area = "Customer" });
        }

        private async Task UpdateUser(EditViewModel viewModel, ApplicationUser user, CustomerUser customerUser)
        {
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
                    }
                }
            }
        }

        private async Task UpdateUserToContact(EditViewModel viewModel, ApplicationUser user, CustomerUser customerUser)
        {
        }

        private void UpdateContact(EditViewModel viewModel, Contact contact)
        {
            contact.Name = viewModel.Name;

            Byte[] rowVersion = contact.Address.RowVersion;
            viewModel.MapVMToEntity(contact);
            viewModel.MapVMToEntity(contact.Address);
            contact.Address.RowVersion = rowVersion;

            KEUnitOfWork.ContactRepository.Update(contact);
            KEUnitOfWork.Complete();
        }

        private void UpdateContactToUser(EditViewModel viewModel, Contact contact)
        {
        }

        private EditViewModel InitEditView(EditViewModel viewModel, CustomerUser customerUser)
        {
            LoadCustomerRolesWithoutContact();

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
            var contact = KEUnitOfWork.ContactRepository.Get(id);

            if (customerUser == null && contact == null)
            {
                LoadCustomerRoles();
                AddErrors("User does not exist");
                return View("Index");
            }

            if (customerUser != null)
            {
                var user = UserManager.FindByIdAsync(customerUser.Id.ToString()).Result;

                user.DeletedDate = DateTime.UtcNow;
                var result = await UserManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    customerUser.DeletedDate = DateTime.UtcNow;
                    KEUnitOfWork.CustomerUserRepository.Update(customerUser);
                    KEUnitOfWork.Complete();

                    AddLog("User Deleted", LogTypeEnum.Info);
                    return RedirectToAction("Index", "User", new { area = "Customer" });
                }

                AddErrors(result);
            }
            else
            {
                contact.DeletedDate = DateTime.UtcNow;
                KEUnitOfWork.ContactRepository.Update(contact);
                KEUnitOfWork.Complete();

                AddLog("User Deleted", LogTypeEnum.Info);
                return RedirectToAction("Index", "User", new { area = "Customer" });
            }

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