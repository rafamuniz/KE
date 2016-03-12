using KarmicEnergy.Web.Models;
using KarmicEnergy.Web.ViewModels.Account;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Munizoft.Extensions;

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
            String email = this.Request.QueryString["u"];
            ForgotPasswordViewModel viewModel = new ForgotPasswordViewModel()
            {
                Email = email
            };

            return View(viewModel);
        }

        //
        // POST: /ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("Confirmation");
                }

                await SendEmailForgotPassword(user);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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
            String templateId = "7f518da0-17af-46b4-b6cd-7b964b06df03";

            EmailMessage emailMessage = new EmailMessage()
            {
                Body = body,
                Subject = subject,
                Destination = user.Email,
                TemplateId = templateId
            };

            await UserManager.EmailService.SendAsync(emailMessage);
        }
    }
}