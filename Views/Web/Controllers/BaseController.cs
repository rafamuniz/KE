using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Web.Entities;
using KarmicEnergy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private KEUnitOfWork _KEUnitOfWork;

        public BaseController()
            : this(null, null, null)
        {
        }

        public BaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : this(userManager, null, signInManager)
        {
        }

        public BaseController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
        }

        protected ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set { _userManager = value; }
        }

        protected ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); ;
            }
            private set { _roleManager = value; }
        }

        protected String UserId
        {
            get { return HttpContext.User.Identity.GetUserId(); }
        }

        protected Guid SiteId
        {
            get
            {
                if (ConfigurationManager.AppSettings["Site:Id"] != null)
                {
                    Guid siteId;
                    if (Guid.TryParse(ConfigurationManager.AppSettings["Site:Id"], out siteId))
                        return siteId;
                }

                return Guid.Empty;
            }
        }

        protected Boolean IsSite
        {
            get
            {
                String siteId = ConfigurationManager.AppSettings["Site:Id"];

                Guid id;
                if (!String.IsNullOrEmpty(siteId) && Guid.TryParse(siteId, out id))
                    return true;
                return false;
            }
        }

        protected ApplicationUser AppUser
        {
            get { return UserManager.FindById(UserId); }
        }

        protected KEUnitOfWork KEUnitOfWork
        {
            get
            {
                return _KEUnitOfWork ?? HttpContext.GetOwinContext().Get<KEUnitOfWork>();
            }
            set
            {
                _KEUnitOfWork = value;
            }
        }

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        protected Guid CustomerId
        {
            get
            {
                var userId = HttpContext.User.Identity.GetUserId();

                if (UserManager.IsInRole(userId, "Customer"))
                {
                    return Guid.Parse(userId);
                }
                else
                {
                    var customerUser = KEUnitOfWork.CustomerUserRepository.Get(Guid.Parse(userId));
                    if (customerUser != null)
                        return customerUser.CustomerId;
                    else
                        return Guid.Empty;
                }
            }
        }

        protected Guid TankId
        {
            get
            {
                Guid tankId = default(Guid);

                if (Request.QueryString.AllKeys.Contains("TankId"))
                    if (Guid.TryParse(Request.QueryString["TankId"], out tankId))
                        return tankId;

                return tankId;
            }
        }

        #region Log
        protected void AddLog(String message, LogTypeEnum type = LogTypeEnum.Error)
        {
            var siteId = SiteId == Guid.Empty ? (Guid?)null : SiteId;
            var customerId = CustomerId == Guid.Empty ? (Guid?)null : CustomerId;

            Log log = new Log() { LogTypeId = (Int16)type, Message = message, CustomerId = customerId, SiteId = siteId, UserId = Guid.Parse(UserId) };
            KEUnitOfWork.LogRepository.Add(log);
            KEUnitOfWork.Complete();
        }
        #endregion Log

        #region Errors
        protected void AddErrors(IdentityResult result)
        {
            if (result.Errors.Any())
            {
                StringBuilder message = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                    message.Append(error);
                }

                AddLog(message.ToString());
            }
        }

        protected void AddErrors(Exception ex)
        {
            String message = String.Empty;

            if (ex is DbEntityValidationException)
            {
                AddErrors((DbEntityValidationException)ex);
            }
            else if (ex is DbUpdateException)
            {
                AddErrors((DbUpdateException)ex);
            }
            else
            {
                AddErrors(ex.Message);
            }
        }

        protected void AddErrors(String key, String message)
        {
            ModelState.AddModelError(key, message);
            AddLog(message);
        }

        protected void AddErrors(DbEntityValidationException dbex)
        {
            StringBuilder message = new StringBuilder();
            foreach (var error in dbex.EntityValidationErrors)
            {
                foreach (var valError in error.ValidationErrors)
                {
                    ModelState.AddModelError("", valError.ErrorMessage);
                    message.Append(valError.ErrorMessage);
                }
            }

            AddLog(message.ToString());
        }

        protected void AddErrors(DbUpdateException uex)
        {
            String message = uex.InnerException.InnerException.Message;
            ModelState.AddModelError("", message);
            AddLog(message);
        }

        protected void AddErrors(String message)
        {
            ModelState.AddModelError("", message);
            AddLog(message);
        }

        #endregion Errors

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        protected List<AdminRole> LoadAdminRoles()
        {
            List<AdminRole> roles = new List<AdminRole>()
            {
                new AdminRole() { Id = "Admin", Name = "Admin" },
                new AdminRole() { Id = "User", Name = "User" }
            };

            ViewBag.AdminRoles = roles;
            return roles;
        }

        protected List<CustomerRole> LoadCustomerRoles()
        {
            List<CustomerRole> roles = new List<CustomerRole>()
            {
                new CustomerRole() { Id = "General Manager", Name = "General Manager" },
                new CustomerRole() { Id = "Supervisor", Name = "Supervisor" },
                new CustomerRole() { Id = "Operator", Name = "Operator" }
            };

            ViewBag.CustomerRoles = roles;
            return roles;
        }

        protected List<Status> LoadStatuses()
        {
            List<Status> statuses = new List<Status>()
            {
                new Status() { Id = "A", Name = "Active" },
                new Status() { Id = "I", Name = "Inactive" }
            };

            ViewBag.Statuses = statuses;
            return statuses;
        }

        //protected IList<ImageSelectListItem> LoadTankModels()
        //{
        //    IList<TankModel> tankModels = KEUnitOfWork.TankModelRepository.GetAll().ToList();
        //    IList<ImageSelectListItem> imagesSelect = new List<ImageSelectListItem>();

        //    foreach (var tm in tankModels)
        //    {
        //        ImageSelectListItem item = new ImageSelectListItem();
        //        item.Text = tm.Name;
        //        item.Value = tm.Id.ToString();
        //        item.ImageFileName = tm.ImageFilename;
        //        imagesSelect.Add(item);
        //    }

        //    ViewBag.TankModels = imagesSelect;
        //    return imagesSelect;
        //}

        protected List<Operator> LoadOperators(OperatorTypeEnum type)
        {
            List<Operator> operators = KEUnitOfWork.OperatorRepository.GetsByOperatorType(type).ToList();
            ViewBag.Operators = operators;
            return operators;
        }

        protected List<Severity> LoadSeverities()
        {
            List<Severity> severities = KEUnitOfWork.SeverityRepository.GetAllActive().ToList();
            ViewBag.Severities = severities;
            return severities;
        }

        protected List<TankModel> LoadTankModels()
        {
            List<TankModel> tankModels = KEUnitOfWork.TankModelRepository.GetAllActive().ToList();
            ViewBag.TankModels = tankModels;
            return tankModels;
        }

        protected List<StickConversion> LoadStickConversions()
        {
            List<StickConversion> stickConversions = KEUnitOfWork.StickConversionRepository.GetAllActive().ToList();
            ViewBag.StickConversions = stickConversions;
            return stickConversions;
        }

        protected List<Item> LoadItems()
        {
            List<Item> items = KEUnitOfWork.ItemRepository.GetAllActive().ToList();
            ViewBag.Items = items;
            return items;
        }

        protected List<Contact> LoadContacts(Guid customerId)
        {
            List<Contact> contacts = KEUnitOfWork.ContactRepository.GetsByCustomer(customerId);
            ViewBag.Contacts = contacts;
            return contacts;
        }

        protected List<SensorItem> LoadSensorItems(Guid sensorId)
        {
            List<SensorItem> sensorItems = KEUnitOfWork.SensorItemRepository.GetsBySensor(sensorId);
            ViewBag.SensorItems = sensorItems;
            return sensorItems;
        }

        protected List<Unit> LoadUnits()
        {
            List<Unit> units = KEUnitOfWork.UnitRepository.GetAllActive().ToList();
            ViewBag.Units = units;
            return units;
        }

        protected List<CustomerUser> LoadCustomerUsers(Guid customerId)
        {
            List<CustomerUser> customerUsers = KEUnitOfWork.CustomerUserRepository.GetsByCustomer(customerId);
            return customerUsers;
        }

        protected List<Site> LoadSites()
        {
            List<Site> sites = new List<Site>();

            if (User.IsInRole("General Manager") || User.IsInRole("Customer"))
            {
                sites = KEUnitOfWork.SiteRepository.GetsByCustomer(CustomerId);
            }
            else
            {
                sites = KEUnitOfWork.CustomerUserSiteRepository.GetsSiteByUser(Guid.Parse(UserId));
            }

            sites = sites.Any() ? sites.OrderBy(x => x.Name).ToList() : sites;
            ViewBag.Sites = sites;
            return sites;
        }

        protected List<Site> LoadSites(Guid customerId)
        {
            List<Site> sites = KEUnitOfWork.SiteRepository.GetsByCustomer(customerId);
            ViewBag.Sites = sites;
            return sites;
        }

        protected List<Site> LoadSites(Guid customerId, Guid userId)
        {
            List<Site> sites = KEUnitOfWork.CustomerUserSiteRepository.GetsSiteByUser(userId);
            ViewBag.Sites = sites;
            return sites;
        }

        protected List<Tank> LoadTanks(Guid customerId)
        {
            List<Tank> tanks = KEUnitOfWork.TankRepository.GetsByCustomerId(customerId);
            ViewBag.Tanks = tanks;
            return tanks;
        }

        protected List<Tank> LoadTanks(Guid customerId, Guid siteId)
        {
            List<Tank> tanks = KEUnitOfWork.TankRepository.GetsByCustomerAndSite(customerId, siteId);
            ViewBag.Tanks = tanks;
            return tanks;
        }

        protected List<Pond> LoadPonds(Guid customerId, Guid siteId)
        {
            List<Pond> ponds = KEUnitOfWork.PondRepository.GetsByCustomerAndSite(customerId, siteId);
            ViewBag.Ponds = ponds;
            return ponds;
        }

        protected List<Sensor> LoadSiteSensors(Guid customerId, Guid siteId)
        {
            List<Sensor> sensors = KEUnitOfWork.SensorRepository.GetsByCustomerAndSite(customerId, siteId);
            ViewBag.Sensors = sensors;
            return sensors;
        }

        protected List<Sensor> LoadPondSensors(Guid customerId, Guid pondId)
        {
            List<Sensor> sensors = KEUnitOfWork.SensorRepository.GetsByCustomerAndPond(customerId, pondId);
            ViewBag.Sensors = sensors;
            return sensors;
        }

        protected List<Sensor> LoadTankSensors(Guid customerId, Guid tankId)
        {
            List<Sensor> sensors = KEUnitOfWork.SensorRepository.GetsByCustomerAndTank(customerId, tankId);
            ViewBag.Sensors = sensors;
            return sensors;
        }

        protected List<SensorType> LoadSensorTypes()
        {
            List<SensorType> sensorTypes = KEUnitOfWork.SensorTypeRepository.GetAll().ToList();
            ViewBag.SensorTypes = sensorTypes;
            return sensorTypes;
        }

        public List<ApplicationUser> GetUsersInRoles(params String[] roleNames)
        {
            List<String> roleIds = new List<String>();
            var roles = RoleManager.Roles.Where(r => roleNames.Any(n => n == r.Name)).ToList();
            roles.ForEach(r => roleIds.Add(r.Id));
            var usersInRole = UserManager.Users.Where(u => u.Roles.Any(r => roleIds.Contains(r.RoleId))).ToList();
            return usersInRole;
        }

        public List<ApplicationUser> GetUsersInRole(String roleName)
        {
            var role = RoleManager.FindByName(roleName);
            var usersInRole = UserManager.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.Id)).ToList();
            return usersInRole;
        }

        //public List<ApplicationUser> GetUsersInRole(string roleName)
        //{
        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        //    var role = roleManager.FindByName(roleName).Users.First();
        //    var usersInRole = Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)).ToList();
        //    return usersInRole;
        //}
    }
}