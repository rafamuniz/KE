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
            List<ListViewModel> viewModels = new List<ListViewModel>();

            if (!IsSite)
            {
                var triggers = KEUnitOfWork.TriggerRepository.GetAll().ToList();
                viewModels = ListViewModel.Map(triggers);
            }
            else
            {
                var triggers = KEUnitOfWork.TriggerRepository.GetsAllBySite(SiteId).ToList();
                viewModels = ListViewModel.Map(triggers);
            }

            AddLog("Navigated to Trigger View", LogTypeEnum.Info);
            return View(viewModels);
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
    }
}