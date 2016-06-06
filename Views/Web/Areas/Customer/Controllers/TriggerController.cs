using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Models;
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
                var triggers = KEUnitOfWork.TriggerRepository.GetAll().ToList();
                viewModels = ListViewModel.Map(triggers);
            }

            return View(viewModels);
        }
        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public async Task<ActionResult> Create()
        {
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
                    //, new { TankId = viewModel.TankId.Value };
                    return View(await InitCreate(viewModel));
                }

                Decimal min, max;

                if (!Decimal.TryParse(viewModel.MinValue, out min))
                {
                    AddErrors("Min value must be a decimal number");
                    return View(await InitCreate(viewModel));
                }

                if (!Decimal.TryParse(viewModel.MaxValue, out max))
                {
                    AddErrors("Max value must be a decimal number");
                    return View(await InitCreate(viewModel));
                }

                if (min > max ||
                    min == max)
                {
                    AddErrors("Max value must be greater than Min value");
                    return View(await InitCreate(viewModel));
                }

                Trigger trigger = new Trigger()
                {
                    Status = viewModel.Status,
                    SeverityId = viewModel.SeverityId.Value,
                    SensorItemId = viewModel.SensorItemId.Value,
                    MinValue = viewModel.MinValue,
                    MaxValue = viewModel.MaxValue
                };

                trigger.Contacts.AddRange(viewModel.Contacts.Where(x => x.IsSelected == true).Select(s => new TriggerContact() { ContactId = s.Id }).ToList());
                trigger.Contacts.AddRange(viewModel.Users.Where(x => x.IsSelected == true).Select(s => new TriggerContact() { UserId = s.Id }).ToList());

                KEUnitOfWork.TriggerRepository.Add(trigger);
                KEUnitOfWork.Complete();

                return RedirectToAction("Gauge", "Tank", new { area = "Customer", tankId = viewModel.TankId });
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
                LoadSites(CustomerId);
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
                LoadSensors(CustomerId, viewModel.TankId.Value);
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

            LoadSeverities();
            LoadStatuses();

            return viewModel;
        }

        #endregion Create

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

            return RedirectToAction("Index", "Trigger");
        }

        #endregion Delete      

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsTankBySiteId(Guid siteId)
        {
            var tanks = LoadTanks(CustomerId, siteId);
            SelectList obgTanks = new SelectList(tanks, "Id", "Name", 0);
            return Json(obgTanks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsSensorByTankId(Guid tankId)
        {
            var sensors = LoadSensors(CustomerId, tankId);
            SelectList obgSensors = new SelectList(sensors, "Id", "Name", 0);
            return Json(obgSensors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult GetsSensorItemBySensorId(Guid sensorId)
        {
            var sensorItems = LoadSensorItems(sensorId);
            SelectList obgSensors = new SelectList(sensorItems, "Id", "Item.Name", 0);
            return Json(obgSensors, JsonRequestBehavior.AllowGet);
        }
    }
}