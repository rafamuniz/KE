using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.ViewModels.Log;
using KarmicEnergy.Web.Controllers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Controllers
{
    [Authorize]
    public class LogController : BaseController
    {
        #region Index

        [Authorize(Roles = "SuperAdmin, Admin, User, Customer, General Manager, Supervisor, Operator")]
        public ActionResult Index()
        {
            List<Log> logs = new List<Log>();

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

            AddLog("Navigated to Log View", LogTypeEnum.Info);
            return View(viewModels);
        }

        #endregion Index       
    }
}