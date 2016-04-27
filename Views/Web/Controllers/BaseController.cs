using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Web;
using System.Web.Mvc;
using KarmicEnergy.Core.Entities;
using Munizoft.MVC.Helpers.Models;
using AutoMapper;
using System.Data.Entity.Infrastructure;
using System.Configuration;

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

        protected Boolean IsSite
        {
            get
            {
                String siteId = ConfigurationManager.AppSettings["Site:Idxxx"];

                if (siteId.Trim() == String.Empty)
                    return false;
                else
                    return true;
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
                    return customerUser.CustomerId;
                }
            }
        }

        protected Guid TankId
        {
            get
            {
                Guid tankId = default(Guid);

                if (Request.QueryString.AllKeys.Contains("tankId"))
                    if (Guid.TryParse(Request.QueryString["tankId"], out tankId))
                        return tankId;

                return tankId;
            }
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected void AddErrors(Exception ex)
        {
            if (ex is DbEntityValidationException)
                AddErrors((DbEntityValidationException)ex);
            else if (ex is DbUpdateException)
                AddErrors((DbUpdateException)ex);
            else
                ModelState.AddModelError("", ex.Message);
        }

        protected void AddErrors(String key, String message)
        {
            ModelState.AddModelError(key, message);
        }

        protected void AddErrors(DbEntityValidationException dbex)
        {
            foreach (var error in dbex.EntityValidationErrors)
            {
                foreach (var valError in error.ValidationErrors)
                {
                    ModelState.AddModelError("", valError.ErrorMessage);
                }
            }
        }

        protected void AddErrors(DbUpdateException uex)
        {
            ModelState.AddModelError("", uex.InnerException.InnerException.Message);
        }

        protected void AddErrors(String message)
        {
            ModelState.AddModelError("", message);
        }

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
                new AdminRole() { Id = "Operator", Name = "Operator" }
            };

            ViewBag.AdminRoles = roles;
            return roles;
        }

        protected List<CustomerRole> LoadCustomerRoles()
        {
            List<CustomerRole> roles = new List<CustomerRole>()
            {
                new CustomerRole() { Id = "CustomerAdmin", Name = "Admin" },
                new CustomerRole() { Id = "CustomerOperator", Name = "Operator" }
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

        protected List<TankModel> LoadTankModels()
        {
            List<TankModel> tankModels = KEUnitOfWork.TankModelRepository.GetAllActive().ToList();
            ViewBag.TankModels = tankModels;
            return tankModels;
        }
        protected List<Item> LoadItems()
        {
            List<Item> items = KEUnitOfWork.ItemRepository.GetAllActive().ToList();
            ViewBag.Items = items;
            return items;
        }

        //protected List<Site> LoadSites()
        //{
        //    List<Site> sites = KEUnitOfWork.SiteRepository.GetAll().ToList();
        //    ViewBag.Sites = sites;
        //    return sites;
        //}

        protected List<Site> LoadSites(Guid customerId)
        {
            List<Site> sites = KEUnitOfWork.SiteRepository.GetsByCustomerId(customerId);
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
            List<Tank> tanks = KEUnitOfWork.TankRepository.GetsByCustomerIdAndSiteId(customerId, siteId);
            ViewBag.Tanks = tanks;
            return tanks;
        }
        protected List<Sensor> LoadSensors(Guid customerId, Guid tankId)
        {
            List<Sensor> sensors = KEUnitOfWork.SensorRepository.GetsByTankIdAndCustomerId(customerId, tankId);
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