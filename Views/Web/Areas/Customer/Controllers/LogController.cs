using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Services.Interface;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Log;
using KarmicEnergy.Web.Controllers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class LogController : BaseController
    {
        #region Fields
        private readonly ILogService _logService;
        #endregion Fields

        #region Constructor
        public LogController(ILogService logService)
        {
            this._logService = logService;
        }
        #endregion Constructor

        #region Index

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Index()
        {
            List<Log> logs = new List<Log>();

            if (UserManager.IsInRole(UserId, "Operator"))
            {
                logs = KEUnitOfWork.LogRepository.GetsByUser(Guid.Parse(UserId)).ToList();
            }
            else if (IsSite)
            {
                logs = KEUnitOfWork.LogRepository.GetsBySite(SiteId);
            }
            else
            {
                logs = KEUnitOfWork.LogRepository.GetsByCustomer(CustomerId);
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