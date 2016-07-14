using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger;
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
    public class TriggerController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Index()
        {
            ListViewModel viewModel = new ListViewModel();
            List<TriggerViewModel> triggerViewModels = new List<TriggerViewModel>();
            List<Trigger> entities = new List<Trigger>();

            if (!IsSite)
            {
                entities = KEUnitOfWork.TriggerRepository.GetAll().ToList();
            }
            else
            {
                entities = KEUnitOfWork.TriggerRepository.GetsBySite(SiteId).ToList();
            }

            triggerViewModels = TriggerViewModel.Map(entities);
            viewModel.Triggers = triggerViewModels;

            AddLog("Navigated to Trigger View", LogTypeEnum.Info);
            return View(viewModel);
        }
        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Create()
        {
            AddLog("Navigate to Create Trigger View", LogTypeEnum.Info);
            return View(await InitCreate(null));
        }

        //
        // POST: /Trigger/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(await InitCreate(viewModel));
                }

                Decimal value;

                if (!Decimal.TryParse(viewModel.Value, out value))
                {
                    AddErrors("Value must be a decimal number");
                    return View(await InitCreate(viewModel));
                }

                Trigger trigger = new Trigger()
                {
                    Status = viewModel.Status,
                    SeverityId = viewModel.SeverityId,
                    OperatorId = viewModel.OperatorId,
                    SensorItemId = viewModel.SensorItemId.Value,
                    Value = viewModel.Value,
                };

                trigger.Contacts.AddRange(viewModel.Contacts.Where(x => x.IsSelected == true).Select(s => new TriggerContact() { ContactId = s.Id }).ToList());
                trigger.Contacts.AddRange(viewModel.Users.Where(x => x.IsSelected == true).Select(s => new TriggerContact() { UserId = s.Id }).ToList());

                KEUnitOfWork.TriggerRepository.Add(trigger);
                KEUnitOfWork.Complete();

                AddLog("Trigger Created", LogTypeEnum.Info);
                return RedirectToAction("Index", "Trigger");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(await InitCreate(viewModel));
        }

        private async Task<CreateViewModel> InitCreate(CreateViewModel viewModel)
        {
            List<ContactViewModel> contactVM = null;
            List<UserViewModel> userVM = null;

            if (viewModel == null)
                viewModel = new CreateViewModel();
            else
            {
                contactVM = viewModel.Contacts.ToList();
                userVM = viewModel.Users.ToList();
            }

            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
                LoadTanks(CustomerId, SiteId);
            }

            if (Request.QueryString["TankId"] != null)
            {
                viewModel.TankId = new Guid(Request.QueryString["TankId"]);
            }

            if (viewModel.TankId.HasValue)
            {
                LoadTankSensors(CustomerId, viewModel.TankId.Value);
            }

            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);

            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = await UserManager.FindByIdAsync(id);
                cuvm.Name = user.Name;
            }

            viewModel.Users = customerUserViewModels;

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            if (viewModel.Contacts.Any() && contactVM != null)
            {
                foreach (var contact in contactVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Contacts.Where(x => x.Id == contact.Id).Single().IsSelected = true;
                }
            }

            if (viewModel.Users.Any() && userVM != null)
            {
                foreach (var user in userVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Users.Where(x => x.Id == user.Id).Single().IsSelected = true;
                }
            }

            LoadOperators(OperatorTypeEnum.Relational);
            LoadSeverities();
            LoadStatuses();

            return viewModel;
        }

        #endregion Create

        #region Edit

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var trigger = KEUnitOfWork.TriggerRepository.Get(id);

            if (trigger == null)
            {
                AddErrors("Trigger does not exist");
                return View("Index");
            }

            AddLog("Navigate to Edit Trigger View", LogTypeEnum.Info);
            return View(await InitEdit(trigger));
        }

        private async Task<EditViewModel> InitEdit(Trigger trigger)
        {
            EditViewModel viewModel = new EditViewModel();
            List<ContactViewModel> contactVM = null;
            List<UserViewModel> userVM = null;

            // Sites
            if (!IsSite)
            {
                LoadSites();
                viewModel.SiteId = trigger.SensorItem.Sensor.Site != null ? trigger.SensorItem.Sensor.Site.Id : trigger.SensorItem.Sensor.Tank.SiteId;
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            // Tanks
            LoadTanks(CustomerId, viewModel.SiteId.Value);
            if (trigger.SensorItem.Sensor.Tank != null)
            {
                viewModel.TankId = trigger.SensorItem.Sensor.TankId;
            }

            // Sensors
            if (viewModel.SiteId.HasValue)
            {
                LoadSiteSensors(CustomerId, viewModel.SiteId.Value);
            }
            else if (viewModel.PondId.HasValue)
            {
                LoadPondSensors(CustomerId, viewModel.PondId.Value);
            }
            else if (viewModel.TankId.HasValue)
            {
                LoadTankSensors(CustomerId, viewModel.TankId.Value);
            }

            viewModel.SensorId = trigger.SensorItem.SensorId;

            // Sensor Items
            if (viewModel.SensorId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            viewModel.SensorItemId = trigger.SensorItem.Id;

            // Contacts And Users
            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);
            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = await UserManager.FindByIdAsync(id);
                cuvm.Name = user.Name;
            }
            viewModel.Users = customerUserViewModels;

            if (trigger.Contacts.Any())
            {
                foreach (var item in contacts)
                {
                    foreach (var avalItem in viewModel.Contacts)
                    {
                        if (item.Id == avalItem.Id)
                        {
                            avalItem.IsSelected = true;
                        }
                    }
                }

                foreach (var item in customerUsers)
                {
                    foreach (var avalItem in viewModel.Users)
                    {
                        if (item.Id == avalItem.Id)
                        {
                            avalItem.IsSelected = true;
                        }
                    }
                }
            }

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            //if (trigger.Contacts.Any())
            //{
            //    List<ContactViewModel> contactViewModels = new List<ContactViewModel>();
            //    List<UserViewModel> userViewModels = new List<UserViewModel>();

            //    foreach (var item in trigger.Contacts)
            //    {
            //        foreach (var avalItem in viewModel.Items)
            //        {
            //            if (item.ItemId == avalItem.Id)
            //            {
            //                avalItem.IsSelected = true;
            //                avalItem.UnitSelected = item.UnitId;
            //            }
            //        }
            //    }
            //}


            //if (viewModel.Contacts.Any() && contactVM != null)
            //{
            //    foreach (var contact in contactVM.Where(x => x.IsSelected == true))
            //    {
            //        viewModel.Contacts.Where(x => x.Id == contact.Id).Single().IsSelected = true;
            //    }
            //}

            //if (viewModel.Users.Any() && userVM != null)
            //{
            //    foreach (var user in userVM.Where(x => x.IsSelected == true))
            //    {
            //        viewModel.Users.Where(x => x.Id == user.Id).Single().IsSelected = true;
            //    }
            //}

            LoadOperators(OperatorTypeEnum.Relational);
            viewModel.OperatorId = trigger.OperatorId;

            LoadSeverities();
            viewModel.SeverityId = trigger.SeverityId;

            LoadStatuses();
            viewModel.Status = trigger.Status;

            viewModel.Value = trigger.Value;

            return viewModel;
        }

        //
        // POST: /Trigger/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Edit(EditViewModel viewModel)
        {
            Trigger trigger = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(await InitEditSubmit(viewModel, null));
                }

                Decimal value = 0;
                if (!Decimal.TryParse(viewModel.Value, out value))
                {
                    AddErrors("Value must be a decimal number");
                    return View(await InitEditSubmit(viewModel, null));
                }

                trigger = KEUnitOfWork.TriggerRepository.Get(viewModel.Id);

                trigger.Id = viewModel.Id;
                trigger.Status = viewModel.Status;
                trigger.SeverityId = viewModel.SeverityId;
                trigger.OperatorId = viewModel.OperatorId;
                trigger.SensorItemId = viewModel.SensorItemId.Value;
                trigger.Value = viewModel.Value;

                if (viewModel.Contacts.Any())
                {
                    foreach (var item in viewModel.Contacts)
                    {
                        if (item.IsSelected)
                        {
                            var hasContact = trigger.Contacts.Where(x => x.Id == item.Id && x.DeletedDate == null).SingleOrDefault();

                            if (hasContact == null) // ADD
                            {
                                TriggerContact triggerContact = new TriggerContact()
                                {
                                    //TriggerId = viewModel.Id,
                                    ContactId = item.Id
                                };

                                trigger.Contacts.Add(triggerContact);
                            }
                        }
                        else // DELETE
                        {
                            var hasContact = trigger.Contacts.Where(x => x.Id == item.Id && x.DeletedDate == null).SingleOrDefault();

                            if (hasContact != null)
                            {
                                hasContact.DeletedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }

                if (viewModel.Users.Any())
                {
                    foreach (var item in viewModel.Users)
                    {
                        if (item.IsSelected)
                        {
                            var hasContact = trigger.Contacts.Where(x => x.Id == item.Id && x.DeletedDate == null).SingleOrDefault();

                            if (hasContact == null) // ADD
                            {
                                TriggerContact triggerContact = new TriggerContact()
                                {
                                    //TriggerId = viewModel.Id,
                                    UserId = item.Id
                                };

                                trigger.Contacts.Add(triggerContact);
                            }
                        }
                        else // DELETE
                        {
                            var hasContact = trigger.Contacts.Where(x => x.Id == item.Id && x.DeletedDate == null).SingleOrDefault();

                            if (hasContact != null)
                            {
                                hasContact.DeletedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }

                KEUnitOfWork.TriggerRepository.Update(trigger);
                KEUnitOfWork.Complete();

                AddLog("Trigger Updated", LogTypeEnum.Info);
                return RedirectToAction("Index", "Trigger");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(await InitEditSubmit(viewModel, null));
        }

        private async Task<EditViewModel> InitEditSubmit(EditViewModel viewModel, Trigger trigger)
        {
            List<ContactViewModel> contactVM = null;
            List<UserViewModel> userVM = null;

            if (viewModel == null)
            {
                viewModel = new EditViewModel();
            }
            else
            {
                contactVM = viewModel.Contacts.ToList();
                userVM = viewModel.Users.ToList();
            }

            if (!IsSite)
            {
                LoadSites();
                if (viewModel.SiteId.HasValue && trigger != null)
                    viewModel.SiteId = trigger.SensorItem.Sensor.SiteId;
            }
            else
            {
                viewModel.SiteId = SiteId;
                LoadTanks(CustomerId, SiteId);
            }

            // Sensors
            if (viewModel.SiteId.HasValue)
            {
                LoadSiteSensors(CustomerId, viewModel.SiteId.Value);
            }
            else if (viewModel.PondId.HasValue)
            {
                LoadPondSensors(CustomerId, viewModel.PondId.Value);
            }
            else if (viewModel.TankId.HasValue)
            {
                LoadTankSensors(CustomerId, viewModel.TankId.Value);
            }

            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);

            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = await UserManager.FindByIdAsync(id);
                cuvm.Name = user.Name;
            }

            viewModel.Users = customerUserViewModels;

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            if (viewModel.Contacts.Any() && contactVM != null)
            {
                foreach (var contact in contactVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Contacts.Where(x => x.Id == contact.Id).Single().IsSelected = true;
                }
            }

            if (viewModel.Users.Any() && userVM != null)
            {
                foreach (var user in userVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Users.Where(x => x.Id == user.Id).Single().IsSelected = true;
                }
            }

            LoadOperators(OperatorTypeEnum.Relational);
            LoadSeverities();
            LoadStatuses();

            return viewModel;
        }
        #endregion Edit

        #region Delete
        //
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Delete(Guid id)
        {
            var trigger = KEUnitOfWork.TriggerRepository.Get(id);

            if (trigger == null)
            {
                AddErrors("Trigger does not exist");
                return View("Index");
            }

            trigger.DeletedDate = DateTime.UtcNow;
            KEUnitOfWork.TriggerRepository.Update(trigger);
            KEUnitOfWork.Complete();

            AddLog("Trigger Deleted", LogTypeEnum.Info);
            return RedirectToAction("Index", "Trigger");
        }

        #endregion Delete      

        #region Site

        #region Index
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Site(Guid siteId)
        {
            ListViewModel viewModel = new ListViewModel();
            List<TriggerViewModel> triggerViewModels = new List<TriggerViewModel>();
            List<Trigger> entities = new List<Trigger>();

            if (!IsSite)
            {
                entities = KEUnitOfWork.TriggerRepository.GetsBySite(siteId).ToList();
            }
            else
            {
                entities = KEUnitOfWork.TriggerRepository.GetsBySite(SiteId).ToList();
            }

            viewModel.Triggers = TriggerViewModel.Map(entities);
            viewModel.SiteId = siteId;

            AddLog("Navigated to Trigger of Site View", LogTypeEnum.Info);
            return View(viewModel);
        }
        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> SiteCreate(Guid siteId)
        {
            CreateViewModel viewModel = new CreateViewModel();
            viewModel.SiteId = siteId;
            AddLog("Navigate to Create Trigger of Site View", LogTypeEnum.Info);
            return View(await InitSiteCreate(viewModel));
        }

        //
        // POST: /Trigger/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> SiteCreate(CreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(await InitSiteCreate(viewModel));
                }

                Decimal value;
                if (!Decimal.TryParse(viewModel.Value, out value))
                {
                    AddErrors("Value must be a decimal number");
                    return View(await InitSiteCreate(viewModel));
                }

                Trigger trigger = new Trigger()
                {
                    Status = viewModel.Status,
                    SeverityId = viewModel.SeverityId,
                    OperatorId = viewModel.OperatorId,
                    SensorItemId = viewModel.SensorItemId.Value,
                    Value = viewModel.Value,
                };

                trigger.Contacts.AddRange(viewModel.Contacts.Where(x => x.IsSelected == true).Select(s => new TriggerContact() { ContactId = s.Id }).ToList());
                trigger.Contacts.AddRange(viewModel.Users.Where(x => x.IsSelected == true).Select(s => new TriggerContact() { UserId = s.Id }).ToList());

                KEUnitOfWork.TriggerRepository.Add(trigger);
                KEUnitOfWork.Complete();

                AddLog("Trigger of Site Created", LogTypeEnum.Info);
                return RedirectToAction("Site", "Trigger", new { SiteId = viewModel.SiteId.Value });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(await InitSiteCreate(viewModel));
        }

        private async Task<CreateViewModel> InitSiteCreate(CreateViewModel viewModel)
        {
            List<ContactViewModel> contactVM = null;
            List<UserViewModel> userVM = null;

            if (viewModel == null)
            {
                viewModel = new CreateViewModel();
            }

            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            if (viewModel.SiteId.HasValue)
            {
                LoadSiteSensors(CustomerId, viewModel.SiteId.Value);
            }

            if (viewModel.SensorId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);

            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = await UserManager.FindByIdAsync(id);
                cuvm.Name = user.Name;
            }

            viewModel.Users = customerUserViewModels;

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            if (viewModel.Contacts.Any() && contactVM != null)
            {
                foreach (var contact in contactVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Contacts.Where(x => x.Id == contact.Id).Single().IsSelected = true;
                }
            }

            if (viewModel.Users.Any() && userVM != null)
            {
                foreach (var user in userVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Users.Where(x => x.Id == user.Id).Single().IsSelected = true;
                }
            }

            LoadOperators(OperatorTypeEnum.Relational);
            LoadSeverities();
            LoadStatuses();

            return viewModel;
        }

        #endregion Create

        #region Edit

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> SiteEdit(Guid id, Guid siteId)
        {
            var trigger = KEUnitOfWork.TriggerRepository.Get(id);

            if (trigger == null)
            {
                AddErrors("Trigger does not exist");
                return View("Index");
            }

            EditViewModel viewModel = new EditViewModel();
            viewModel.Id = id;
            viewModel.SiteId = siteId;
            AddLog("Navigate to Edit Trigger of Site View", LogTypeEnum.Info);
            return View(await InitSiteEdit(viewModel, trigger));
        }

        private async Task<EditViewModel> InitSiteEdit(EditViewModel viewModel, Trigger trigger)
        {
            // Sites
            if (!IsSite)
            {
                LoadSites();
                viewModel.SiteId = trigger.SensorItem.Sensor.Site != null ? trigger.SensorItem.Sensor.Site.Id : trigger.SensorItem.Sensor.Tank.SiteId;
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            // Sensors
            if (viewModel.SiteId.HasValue)
            {
                LoadSiteSensors(CustomerId, viewModel.SiteId.Value);
            }

            viewModel.SensorId = trigger.SensorItem.SensorId;

            // Sensor Items
            if (viewModel.SensorId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            viewModel.SensorItemId = trigger.SensorItem.Id;

            // Contacts
            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            // Users
            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);
            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = await UserManager.FindByIdAsync(id);
                cuvm.Name = user.Name;
            }
            viewModel.Users = customerUserViewModels;

            foreach (var contact in trigger.Contacts.Where(x => x.DeletedDate == null))
            {
                var conts = viewModel.Contacts.Where(x => x.Id == contact.ContactId);

                foreach (var contactVM in conts)
                {
                    contactVM.IsSelected = true;
                }

                var us = viewModel.Users.Where(x => x.Id == contact.UserId);

                foreach (var userVM in us)
                {
                    userVM.IsSelected = true;
                }
            }

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            LoadOperators(OperatorTypeEnum.Relational);
            viewModel.OperatorId = trigger.OperatorId;

            LoadSeverities();
            viewModel.SeverityId = trigger.SeverityId;

            LoadStatuses();
            viewModel.Status = trigger.Status;

            viewModel.Value = trigger.Value;

            return viewModel;
        }

        //
        // POST: /Trigger/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> SiteEdit(EditViewModel viewModel)
        {
            Trigger trigger = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(await InitSiteEditSubmit(viewModel, null));
                }

                Decimal value = 0;
                if (!Decimal.TryParse(viewModel.Value, out value))
                {
                    AddErrors("Value must be a decimal number");
                    return View(await InitSiteEditSubmit(viewModel, null));
                }

                trigger = KEUnitOfWork.TriggerRepository.Get(viewModel.Id);

                trigger.Id = viewModel.Id;
                trigger.Status = viewModel.Status;
                trigger.SeverityId = viewModel.SeverityId;
                trigger.OperatorId = viewModel.OperatorId;
                trigger.SensorItemId = viewModel.SensorItemId.Value;
                trigger.Value = viewModel.Value;

                SetContacts(viewModel, trigger);
                SetUsers(viewModel, trigger);

                KEUnitOfWork.TriggerRepository.Update(trigger);
                KEUnitOfWork.Complete();

                AddLog("Trigger of Site Updated", LogTypeEnum.Info);
                return RedirectToAction("Site", "Trigger", new { SiteId = viewModel.SiteId.Value });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(await InitSiteEditSubmit(viewModel, null));
        }

        private async Task<EditViewModel> InitSiteEditSubmit(EditViewModel viewModel, Trigger trigger)
        {
            List<ContactViewModel> contactVM = null;
            List<UserViewModel> userVM = null;

            if (viewModel == null)
            {
                viewModel = new EditViewModel();
            }
            else
            {
                contactVM = viewModel.Contacts.ToList();
                userVM = viewModel.Users.ToList();
            }

            if (!IsSite)
            {
                LoadSites();
                if (viewModel.SiteId.HasValue && trigger != null)
                    viewModel.SiteId = trigger.SensorItem.Sensor.SiteId;
            }
            else
            {
                viewModel.SiteId = SiteId;
                LoadTanks(CustomerId, SiteId);
            }

            // Sensors
            if (viewModel.SiteId.HasValue)
            {
                LoadSiteSensors(CustomerId, viewModel.SiteId.Value);
            }

            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);

            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = await UserManager.FindByIdAsync(id);
                cuvm.Name = user.Name;
            }

            viewModel.Users = customerUserViewModels;

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            if (viewModel.Contacts.Any() && contactVM != null)
            {
                foreach (var contact in contactVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Contacts.Where(x => x.Id == contact.Id).Single().IsSelected = true;
                }
            }

            if (viewModel.Users.Any() && userVM != null)
            {
                foreach (var user in userVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Users.Where(x => x.Id == user.Id).Single().IsSelected = true;
                }
            }

            LoadOperators(OperatorTypeEnum.Relational);
            LoadSeverities();
            LoadStatuses();

            return viewModel;
        }
        #endregion Edit

        #region Delete
        //
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SiteDelete(Guid id, Guid siteId)
        {
            var trigger = KEUnitOfWork.TriggerRepository.Get(id);

            if (trigger == null)
            {
                AddErrors("Trigger does not exist");
                return View("Site");
            }

            trigger.DeletedDate = DateTime.UtcNow;
            KEUnitOfWork.TriggerRepository.Update(trigger);
            KEUnitOfWork.Complete();

            AddLog("Trigger of Site Deleted", LogTypeEnum.Info);
            return RedirectToAction("Site", "Trigger", new { siteId = siteId });
        }

        #endregion Delete      

        #endregion Site

        #region Pond

        #region Index
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Pond(Guid pondId)
        {
            ListViewModel viewModel = new ListViewModel();
            List<TriggerViewModel> triggerViewModels = new List<TriggerViewModel>();
            List<Trigger> entities = KEUnitOfWork.TriggerRepository.GetsByPond(pondId).ToList();
            viewModel.PondId = pondId;
            viewModel.Triggers = TriggerViewModel.Map(entities);

            AddLog("Navigated to Trigger of Pond View", LogTypeEnum.Info);
            return View(viewModel);
        }
        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> PondCreate(Guid pondId)
        {
            CreateViewModel viewModel = new CreateViewModel();
            viewModel.PondId = pondId;
            AddLog("Navigate to Create Trigger View", LogTypeEnum.Info);
            return View(await InitPondCreate(viewModel));
        }

        //
        // POST: /Trigger/Pond/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> PondCreate(CreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(await InitPondCreate(viewModel));
                }

                Decimal value;
                if (!Decimal.TryParse(viewModel.Value, out value))
                {
                    AddErrors("Value must be a decimal number");
                    return View(await InitPondCreate(viewModel));
                }

                Trigger trigger = new Trigger()
                {
                    Status = viewModel.Status,
                    SeverityId = viewModel.SeverityId,
                    OperatorId = viewModel.OperatorId,
                    SensorItemId = viewModel.SensorItemId.Value,
                    Value = viewModel.Value,
                };

                trigger.Contacts.AddRange(viewModel.Contacts.Where(x => x.IsSelected == true).Select(s => new TriggerContact() { ContactId = s.Id }).ToList());
                trigger.Contacts.AddRange(viewModel.Users.Where(x => x.IsSelected == true).Select(s => new TriggerContact() { UserId = s.Id }).ToList());

                KEUnitOfWork.TriggerRepository.Add(trigger);
                KEUnitOfWork.Complete();

                AddLog("Trigger of Pond Created", LogTypeEnum.Info);
                return RedirectToAction("Pond", "Trigger", new { PondId = viewModel.PondId.Value });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(await InitPondCreate(viewModel));
        }

        private async Task<CreateViewModel> InitPondCreate(CreateViewModel viewModel)
        {
            List<ContactViewModel> contactVM = null;
            List<UserViewModel> userVM = null;

            if (viewModel == null)
                viewModel = new CreateViewModel();
            else
            {
                contactVM = viewModel.Contacts.ToList();
                userVM = viewModel.Users.ToList();
            }

            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
                LoadPonds(CustomerId, SiteId);
            }

            if (viewModel.PondId.HasValue)
            {
                LoadPondSensors(CustomerId, viewModel.PondId.Value);
            }

            if (viewModel.SensorId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);

            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = await UserManager.FindByIdAsync(id);
                cuvm.Name = user.Name;
            }

            viewModel.Users = customerUserViewModels;

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            if (viewModel.Contacts.Any() && contactVM != null)
            {
                foreach (var contact in contactVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Contacts.Where(x => x.Id == contact.Id).Single().IsSelected = true;
                }
            }

            if (viewModel.Users.Any() && userVM != null)
            {
                foreach (var user in userVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Users.Where(x => x.Id == user.Id).Single().IsSelected = true;
                }
            }

            LoadOperators(OperatorTypeEnum.Relational);
            LoadSeverities();
            LoadStatuses();

            return viewModel;
        }

        #endregion Create

        #region Edit

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> PondEdit(Guid id, Guid pondId)
        {
            var trigger = KEUnitOfWork.TriggerRepository.Get(id);

            if (trigger == null)
            {
                AddErrors("Trigger does not exist");
                return View("Index");
            }

            EditViewModel viewModel = new EditViewModel();
            viewModel.Id = id;
            viewModel.PondId = pondId;

            AddLog("Navigate to Edit Trigger of Pond View", LogTypeEnum.Info);
            return View(await InitPondEdit(viewModel, trigger));
        }

        private async Task<EditViewModel> InitPondEdit(EditViewModel viewModel, Trigger trigger)
        {
            // Sites
            if (!IsSite)
            {
                LoadSites();
                viewModel.SiteId = trigger.SensorItem.Sensor.Pond.SiteId;
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            // Ponds
            viewModel.PondId = trigger.SensorItem.Sensor.PondId;
            LoadPonds(CustomerId, viewModel.SiteId.Value);

            // Sensors
            if (viewModel.PondId.HasValue)
            {
                LoadPondSensors(CustomerId, viewModel.PondId.Value);
            }

            viewModel.SensorId = trigger.SensorItem.SensorId;

            // Sensor Items
            if (viewModel.SensorId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            viewModel.SensorItemId = trigger.SensorItem.Id;

            // Contacts
            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            // Users
            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);
            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = await UserManager.FindByIdAsync(id);
                cuvm.Name = user.Name;
            }
            viewModel.Users = customerUserViewModels;

            foreach (var contact in trigger.Contacts.Where(x => x.DeletedDate == null))
            {
                var conts = viewModel.Contacts.Where(x => x.Id == contact.ContactId);

                foreach (var contactVM in conts)
                {
                    contactVM.IsSelected = true;
                }

                var us = viewModel.Users.Where(x => x.Id == contact.UserId);

                foreach (var userVM in us)
                {
                    userVM.IsSelected = true;
                }
            }

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            LoadOperators(OperatorTypeEnum.Relational);
            viewModel.OperatorId = trigger.OperatorId;

            LoadSeverities();
            viewModel.SeverityId = trigger.SeverityId;

            LoadStatuses();
            viewModel.Status = trigger.Status;

            viewModel.Value = trigger.Value;

            return viewModel;
        }

        //
        // POST: /Trigger/Pond/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> PondEdit(EditViewModel viewModel)
        {
            Trigger trigger = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(await InitPondEditSubmit(viewModel, null));
                }

                Decimal value = 0;
                if (!Decimal.TryParse(viewModel.Value, out value))
                {
                    AddErrors("Value must be a decimal number");
                    return View(await InitPondEditSubmit(viewModel, null));
                }

                trigger = KEUnitOfWork.TriggerRepository.Get(viewModel.Id);

                trigger.Id = viewModel.Id;
                trigger.Status = viewModel.Status;
                trigger.SeverityId = viewModel.SeverityId;
                trigger.OperatorId = viewModel.OperatorId;
                trigger.SensorItemId = viewModel.SensorItemId.Value;
                trigger.Value = viewModel.Value;

                SetContacts(viewModel, trigger);
                SetUsers(viewModel, trigger);

                KEUnitOfWork.TriggerRepository.Update(trigger);
                KEUnitOfWork.Complete();

                AddLog("Trigger of Pond Updated", LogTypeEnum.Info);
                return RedirectToAction("Pond", "Trigger", new { PondId = viewModel.PondId.Value });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(await InitPondEditSubmit(viewModel, null));
        }

        private async Task<EditViewModel> InitPondEditSubmit(EditViewModel viewModel, Trigger trigger)
        {
            List<ContactViewModel> contactVM = null;
            List<UserViewModel> userVM = null;

            if (viewModel == null)
            {
                viewModel = new EditViewModel();
            }
            else
            {
                contactVM = viewModel.Contacts.ToList();
                userVM = viewModel.Users.ToList();
            }

            if (!IsSite)
            {
                LoadSites();
                if (viewModel.SiteId.HasValue && trigger != null)
                    viewModel.SiteId = trigger.SensorItem.Sensor.SiteId;
            }
            else
            {
                viewModel.SiteId = SiteId;
                LoadTanks(CustomerId, SiteId);
            }

            // Sensors
            if (viewModel.SiteId.HasValue)
            {
                LoadSiteSensors(CustomerId, viewModel.SiteId.Value);
            }

            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);

            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = await UserManager.FindByIdAsync(id);
                cuvm.Name = user.Name;
            }

            viewModel.Users = customerUserViewModels;

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            if (viewModel.Contacts.Any() && contactVM != null)
            {
                foreach (var contact in contactVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Contacts.Where(x => x.Id == contact.Id).Single().IsSelected = true;
                }
            }

            if (viewModel.Users.Any() && userVM != null)
            {
                foreach (var user in userVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Users.Where(x => x.Id == user.Id).Single().IsSelected = true;
                }
            }

            LoadOperators(OperatorTypeEnum.Relational);
            LoadSeverities();
            LoadStatuses();

            return viewModel;
        }
        #endregion Edit

        #region Delete
        //
        // GET: /Trigger/Pond/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult PondDelete(Guid id, Guid pondId)
        {
            var trigger = KEUnitOfWork.TriggerRepository.Get(id);

            if (trigger == null)
            {
                AddErrors("Trigger does not exist");
                return View("Pond");
            }

            trigger.DeletedDate = DateTime.UtcNow;
            KEUnitOfWork.TriggerRepository.Update(trigger);
            KEUnitOfWork.Complete();

            AddLog("Trigger of Pond Deleted", LogTypeEnum.Info);
            return RedirectToAction("Pond", "Trigger", new { PondId = pondId });
        }

        #endregion Delete   

        #endregion Pond

        #region Tank

        #region Index
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Tank(Guid tankId)
        {
            ListViewModel viewModel = new ListViewModel();
            List<TriggerViewModel> triggerViewModels = new List<TriggerViewModel>();
            List<Trigger> entities = KEUnitOfWork.TriggerRepository.GetsByTank(tankId).ToList();
            viewModel.Triggers = TriggerViewModel.Map(entities);
            viewModel.TankId = tankId;

            AddLog("Navigated to Trigger of Tank View", LogTypeEnum.Info);
            return View(viewModel);
        }
        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> TankCreate(Guid tankId)
        {
            CreateViewModel viewModel = new CreateViewModel();
            viewModel.TankId = tankId;
            AddLog("Navigate to Create Trigger of Tank View", LogTypeEnum.Info);
            return View(await InitTankCreate(viewModel));
        }

        //
        // POST: /Trigger/Tank/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> TankCreate(CreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(await InitTankCreate(viewModel));
                }

                Decimal value;

                if (!Decimal.TryParse(viewModel.Value, out value))
                {
                    AddErrors("Value must be a decimal number");
                    return View(await InitTankCreate(viewModel));
                }

                Trigger trigger = new Trigger()
                {
                    Status = viewModel.Status,
                    SeverityId = viewModel.SeverityId,
                    OperatorId = viewModel.OperatorId,
                    SensorItemId = viewModel.SensorItemId.Value,
                    Value = viewModel.Value,
                };

                trigger.Contacts.AddRange(viewModel.Contacts.Where(x => x.IsSelected == true).Select(s => new TriggerContact() { ContactId = s.Id }).ToList());
                trigger.Contacts.AddRange(viewModel.Users.Where(x => x.IsSelected == true).Select(s => new TriggerContact() { UserId = s.Id }).ToList());

                KEUnitOfWork.TriggerRepository.Add(trigger);
                KEUnitOfWork.Complete();

                AddLog("Trigger of Tank Created", LogTypeEnum.Info);
                return RedirectToAction("Tank", "Trigger", new { TankId = viewModel.TankId.Value });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(await InitTankCreate(viewModel));
        }

        private async Task<CreateViewModel> InitTankCreate(CreateViewModel viewModel)
        {
            List<ContactViewModel> contactVM = null;
            List<UserViewModel> userVM = null;

            if (viewModel == null)
                viewModel = new CreateViewModel();
            else
            {
                contactVM = viewModel.Contacts.ToList();
                userVM = viewModel.Users.ToList();
            }

            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
                LoadTanks(CustomerId, SiteId);
            }

            if (viewModel.TankId.HasValue)
            {
                LoadTankSensors(CustomerId, viewModel.TankId.Value);
            }

            if (viewModel.SensorId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);

            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = await UserManager.FindByIdAsync(id);
                cuvm.Name = user.Name;
            }

            viewModel.Users = customerUserViewModels;

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            if (viewModel.Contacts.Any() && contactVM != null)
            {
                foreach (var contact in contactVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Contacts.Where(x => x.Id == contact.Id).Single().IsSelected = true;
                }
            }

            if (viewModel.Users.Any() && userVM != null)
            {
                foreach (var user in userVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Users.Where(x => x.Id == user.Id).Single().IsSelected = true;
                }
            }

            LoadOperators(OperatorTypeEnum.Relational);
            LoadSeverities();
            LoadStatuses();

            return viewModel;
        }

        #endregion Create

        #region Edit

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> TankEdit(Guid id, Guid tankId)
        {
            var trigger = KEUnitOfWork.TriggerRepository.Get(id);

            if (trigger == null)
            {
                AddErrors("Trigger does not exist");
                return View("Index");
            }

            EditViewModel viewModel = new EditViewModel();
            viewModel.Id = id;
            viewModel.TankId = tankId;

            AddLog("Navigate to Edit Trigger of Tank View", LogTypeEnum.Info);
            return View(await InitTankEdit(viewModel, trigger));
        }

        private async Task<EditViewModel> InitTankEdit(EditViewModel viewModel, Trigger trigger)
        {
            // Sites
            if (!IsSite)
            {
                LoadSites();
                viewModel.SiteId = trigger.SensorItem.Sensor.Tank.SiteId;
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            // Tanks
            viewModel.TankId = trigger.SensorItem.Sensor.TankId;
            LoadTanks(CustomerId, viewModel.SiteId.Value);

            // Sensors
            if (viewModel.TankId.HasValue)
            {
                LoadTankSensors(CustomerId, viewModel.TankId.Value);
            }

            viewModel.SensorId = trigger.SensorItem.SensorId;

            // Sensor Items
            if (viewModel.SensorId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            viewModel.SensorItemId = trigger.SensorItem.Id;

            // Contacts
            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            // Users
            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);
            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = await UserManager.FindByIdAsync(id);
                cuvm.Name = user.Name;
            }
            viewModel.Users = customerUserViewModels;

            foreach (var contact in trigger.Contacts.Where(x => x.DeletedDate == null))
            {
                var conts = viewModel.Contacts.Where(x => x.Id == contact.ContactId);

                foreach (var contactVM in conts)
                {
                    contactVM.IsSelected = true;
                }

                var us = viewModel.Users.Where(x => x.Id == contact.UserId);

                foreach (var userVM in us)
                {
                    userVM.IsSelected = true;
                }
            }

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            LoadOperators(OperatorTypeEnum.Relational);
            viewModel.OperatorId = trigger.OperatorId;

            LoadSeverities();
            viewModel.SeverityId = trigger.SeverityId;

            LoadStatuses();
            viewModel.Status = trigger.Status;

            viewModel.Value = trigger.Value;

            return viewModel;
        }

        //
        // POST: /Trigger/Pond/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult TankEdit(EditViewModel viewModel)
        {
            Trigger trigger = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(InitTankEditSubmit(viewModel, null));
                }

                Decimal value = 0;
                if (!Decimal.TryParse(viewModel.Value, out value))
                {
                    AddErrors("Value must be a decimal number");
                    return View(InitTankEditSubmit(viewModel, null));
                }

                trigger = KEUnitOfWork.TriggerRepository.Get(viewModel.Id);

                trigger.Id = viewModel.Id;
                trigger.Status = viewModel.Status;
                trigger.SeverityId = viewModel.SeverityId;
                trigger.OperatorId = viewModel.OperatorId;
                trigger.SensorItemId = viewModel.SensorItemId.Value;
                trigger.Value = viewModel.Value;

                SetContacts(viewModel, trigger);
                SetUsers(viewModel, trigger);

                KEUnitOfWork.TriggerRepository.Update(trigger);
                KEUnitOfWork.Complete();

                AddLog("Trigger of Tank Updated", LogTypeEnum.Info);
                return RedirectToAction("Tank", "Trigger", new { TankId = viewModel.TankId.Value });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(InitTankEditSubmit(viewModel, null));
        }

        private EditViewModel InitTankEditSubmit(EditViewModel viewModel, Trigger trigger)
        {
            List<ContactViewModel> contactVM = null;
            List<UserViewModel> userVM = null;

            if (viewModel == null)
            {
                viewModel = new EditViewModel();
            }
            else
            {
                contactVM = viewModel.Contacts.ToList();
                userVM = viewModel.Users.ToList();
            }

            if (!IsSite)
            {
                LoadSites();
                if (viewModel.SiteId.HasValue && trigger != null)
                    viewModel.SiteId = trigger.SensorItem.Sensor.SiteId;
            }
            else
            {
                viewModel.SiteId = SiteId;
                LoadTanks(CustomerId, SiteId);
            }

            // Sensors
            if (viewModel.SiteId.HasValue)
            {
                LoadSiteSensors(CustomerId, viewModel.SiteId.Value);
            }

            List<Contact> contacts = LoadContacts(CustomerId);
            List<ContactViewModel> contactViewModels = ContactViewModel.Map(contacts);
            viewModel.Contacts = contactViewModels;

            List<CustomerUser> customerUsers = LoadCustomerUsers(CustomerId);
            List<UserViewModel> customerUserViewModels = UserViewModel.Map(customerUsers);

            foreach (var cuvm in customerUserViewModels)
            {
                var id = cuvm.Id.ToString();
                ApplicationUser user = UserManager.FindByIdAsync(id).Result;
                cuvm.Name = user.Name;
            }

            viewModel.Users = customerUserViewModels;

            if (viewModel.SensorItemId.HasValue)
            {
                LoadSensorItems(viewModel.SensorId.Value);
            }

            if (viewModel.Contacts.Any() && contactVM != null)
            {
                foreach (var contact in contactVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Contacts.Where(x => x.Id == contact.Id).Single().IsSelected = true;
                }
            }

            if (viewModel.Users.Any() && userVM != null)
            {
                foreach (var user in userVM.Where(x => x.IsSelected == true))
                {
                    viewModel.Users.Where(x => x.Id == user.Id).Single().IsSelected = true;
                }
            }

            LoadOperators(OperatorTypeEnum.Relational);
            LoadSeverities();
            LoadStatuses();

            return viewModel;
        }
        #endregion Edit

        #region Delete
        //
        // GET: /Trigger/Tank/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult TankDelete(Guid id, Guid tankId)
        {
            var trigger = KEUnitOfWork.TriggerRepository.Get(id);

            if (trigger == null)
            {
                AddErrors("Trigger does not exist");
                return View("Pond");
            }

            trigger.DeletedDate = DateTime.UtcNow;
            KEUnitOfWork.TriggerRepository.Update(trigger);
            KEUnitOfWork.Complete();

            AddLog("Trigger of Tank Deleted", LogTypeEnum.Info);
            return RedirectToAction("Tank", "Trigger", new { tankId = tankId });
        }

        #endregion Delete   

        #endregion Tank

        #region Functions

        private void SetUsers(EditViewModel viewModel, Trigger trigger)
        {
            // USERS
            if (viewModel.Users.Any())
            {
                foreach (var item in viewModel.Users)
                {
                    if (item.IsSelected)
                    {
                        var hasContact = trigger.Contacts.Where(x => x.UserId == item.Id && x.DeletedDate == null).SingleOrDefault();

                        if (hasContact == null) // ADD
                        {
                            TriggerContact triggerContact = new TriggerContact()
                            {
                                UserId = item.Id
                            };

                            trigger.Contacts.Add(triggerContact);
                        }
                    }
                    else // DELETE
                    {
                        var usersDelete = trigger.Contacts.Where(x => x.UserId == item.Id && x.DeletedDate == null);

                        if (usersDelete.Any())
                        {
                            foreach (var user in usersDelete)
                            {
                                user.DeletedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }
            }
        }

        private void SetContacts(EditViewModel viewModel, Trigger trigger)
        {
            // CONTACTS
            if (viewModel.Contacts.Any())
            {
                foreach (var item in viewModel.Contacts)
                {
                    if (item.IsSelected) // ADD
                    {
                        var hasContact = trigger.Contacts.Where(x => x.ContactId == item.Id && x.DeletedDate == null).SingleOrDefault();

                        if (hasContact == null) // ADD
                        {
                            TriggerContact triggerContact = new TriggerContact()
                            {
                                ContactId = item.Id
                            };

                            trigger.Contacts.Add(triggerContact);
                        }
                    }
                    else // DELETE
                    {
                        var contactsDelete = trigger.Contacts.Where(x => x.ContactId == item.Id && x.DeletedDate == null);

                        if (contactsDelete.Any())
                        {
                            foreach (var contact in contactsDelete)
                            {
                                contact.DeletedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }
            }
        }

        #endregion Functions

        #region Json
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsPondBySite(Guid siteId)
        {
            var ponds = LoadPonds(CustomerId, siteId);
            SelectList objPonds = new SelectList(ponds, "Id", "Name", 0);
            return Json(objPonds, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsTankBySite(Guid siteId)
        {
            var tanks = LoadTanks(CustomerId, siteId);
            SelectList objTanks = new SelectList(tanks, "Id", "Name", 0);
            return Json(objTanks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsSiteSensorBySite(Guid siteId)
        {
            var sensors = KEUnitOfWork.SensorRepository.GetsByCustomerAndSite(CustomerId, siteId);
            SelectList objSensors = new SelectList(sensors, "Id", "Name", 0);
            return Json(objSensors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsSiteSensorByPond(Guid siteId)
        {
            var sensors = LoadSiteSensors(CustomerId, siteId);
            SelectList objSensors = new SelectList(sensors, "Id", "Name", 0);
            return Json(objSensors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsPondSensorByPond(Guid pondId)
        {
            var sensors = LoadPondSensors(CustomerId, pondId);
            SelectList objSensors = new SelectList(sensors, "Id", "Name", 0);
            return Json(objSensors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsTankSensorByTank(Guid tankId)
        {
            var sensors = LoadTankSensors(CustomerId, tankId);
            SelectList objSensors = new SelectList(sensors, "Id", "Name", 0);
            return Json(objSensors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsSensorItemBySensor(Guid sensorId)
        {
            var sensorItems = LoadSensorItems(sensorId);
            SelectList objSensors = new SelectList(sensorItems, "Id", "Item.Name", 0);
            return Json(objSensors, JsonRequestBehavior.AllowGet);
        }
        #endregion Json
    }
}