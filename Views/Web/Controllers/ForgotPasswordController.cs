using KarmicEnergy.Web.Entities;
using KarmicEnergy.Web.Models;
using KarmicEnergy.Web.ViewModels.Account;
using Munizoft.Extensions;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Controllers
{
    public class ForgotPasswordController : BaseController
    {
        #region Constructor

        public ForgotPasswordController()
        {
        }

        #endregion Constructor

        //
        // POST: /ForgotPassword
        [AllowAnonymous]
        public ActionResult Index()
        {
            String username = this.Request.QueryString["u"];
            ForgotPasswordViewModel viewModel = new ForgotPasswordViewModel()
            {
                Username = username
            };

            return View(viewModel);
        }

        //
        // POST: /ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(viewModel.Username);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("Confirmation");
                }

                await SendEmailForgotPassword(user);

                return RedirectToAction("Confirmation", "ForgotPassword");
            }

            // If we got this far, something failed, redisplay form
            return View(viewModel);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult Confirmation()
        {
            return View();
        }

        private async Task SendEmailForgotPassword(ApplicationUser user)
        {
            String code = await UserManager.UserTokenProvider.GenerateAsync("ForgotPassword", UserManager, user);

            String host = Request.Host();

            String url = String.Format("{0}/{1}", host, ConfigurationManager.AppSettings["EmailService:ForgotPasswordUrl"]);
            String callbackUrl = url.Replace("{UserId}", user.Id).Replace("{Code}", code);
            String subject = "Forgot Password";
            String body = String.Format("Please confirm your account by clicking <a href='{0}'>here</a>", callbackUrl);

            EmailMessage emailMessage = new EmailMessage()
            {
                Body = body,
                Subject = subject,
                Destination = user.Email
            };

            await UserManager.EmailService.SendAsync(emailMessage);
        }
    }
}