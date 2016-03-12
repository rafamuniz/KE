using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected void AddErrors(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
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

        protected List<CustomerRole> LoadCustomerRoles()
        {
            List<CustomerRole> roles = new List<CustomerRole>()
            {
                new CustomerRole() { Id = "CustomerAdmin", Name = "Admin" },
                new CustomerRole() { Id = "CustomerOperator", Name = "Operator" }
            };

            ViewBag.Roles = roles;
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

    }
}