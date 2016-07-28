using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Admin.ViewModels.Log;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using KarmicEnergy.Web.Controllers;

namespace KarmicEnergy.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class LogController : BaseController
    {
        #region Index

        [Authorize(Roles = "SuperAdmin, Admin, User, Customer, General Manager, Supervisor, Operator")]
        public ActionResult Index()
        {
            List<Core.Entities.Log> logs = new List<Core.Entities.Log>();

            if (UserManager.IsInRole(UserId, "Operator"))
            {
                logs = KEUnitOfWork.LogRepository.GetsByUser(Guid.Parse(UserId)).ToList();
            }
            else
            {
                logs = KEUnitOfWork.LogRepository.GetAll().ToList();
            }

            var viewModels = ListViewModel.Map(logs);

            if (viewModels.Any())
            {
                foreach (var vm in viewModels.Where(x => x.UserId.HasValue))
                {
                    var u = UserManager.FindById(vm.UserId.Value.ToString());
                    if (u != null)
                        vm.Username = u.UserName;
                }
            }

            return View(viewModels);
        }

        #endregion Index       
    }
}