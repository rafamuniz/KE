using KarmicEnergy.Web.Models;
using KarmicEnergy.Web.ViewModels.Account;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Controllers
{
    public class ResetPasswordController : BaseController
    {
        #region Constructor

        public ResetPasswordController()
        {
        }

        #endregion Constructor

        //
        // POST: /ResetPassword
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            String uid = Request.QueryString["uid"];
            String code = Request.QueryString["code"];

            if (uid != null && code != null)
            {
                var user = await UserManager.FindByIdAsync(uid);

                if (user != null)
                {
                    ResetPasswordViewModel viewModel = new ResetPasswordViewModel();
                    viewModel.Email = user.Email;
                    viewModel.Code = code;
                    return View(viewModel);
                }
            }

            return View();
        }

        //
        // GET: /ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(String code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("Confirmation", "ResetPassword");
            }

            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Confirmation", "ResetPassword");
            }

            AddErrors(result);

            return View();
        }

        //
        // GET: /ResetPassword
        [AllowAnonymous]
        public ActionResult Confirmation()
        {
            return View();
        }
    }
}