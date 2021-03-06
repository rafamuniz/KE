﻿using KarmicEnergy.Web.ViewModels.Account;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Owin.Extensions;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;

namespace KarmicEnergy.Web.Controllers
{
    public class LoginController : BaseController
    {
        #region Constructor
        public LoginController()
        {
        }

        public LoginController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(userManager, signInManager)
        {

        }

        #endregion Constructor

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [ActionName("IndexURL")]
        public ActionResult Index(String url)
        {
            ViewBag.ReturnUrl = url;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel viewModel, String ReturnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", viewModel);
                }

                var result = await SignInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return Success(viewModel.Email, ReturnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = ReturnUrl, RememberMe = viewModel.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid email or password");
                        return View("Index", viewModel);
                }
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View("Index", viewModel);
        }

        private ActionResult Success(String username, String url)
        {
            var user = UserManager.FindByName(username);
            var roles = UserManager.GetRoles(user.Id);

            if (IsSite && (roles.Contains("Customer") ||
                roles.Contains("General Manager") ||
                roles.Contains("Supervisor") ||
                roles.Contains("Operator")))
            {
                return RedirectToLocal("~/Customer/FastTracker");
            }

            return RedirectToLocal(url);
        }
    }
}