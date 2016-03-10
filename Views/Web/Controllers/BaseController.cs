using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using KarmicEnergy.Core.Persistence;
using System;
using System.Data.Entity.Validation;
using KarmicEnergy.Web.Areas.Customer.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KarmicEnergy.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private KEUnitOfWork _KEUnitOfWork;

        public BaseController()
        {
        }

        public BaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        protected ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            set
            {
                _signInManager = value;
            }
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
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

        protected void LoadCustomerRoles()
        {
            List<Role> roles = new List<Role>()
            {
            new Role() { Id = "CustomerAdmin", Name = "Admin" },
            new Role() { Id = "CustomerOperator", Name = "Operator" }
            };

            ViewBag.Roles = roles;
        }

    }
}