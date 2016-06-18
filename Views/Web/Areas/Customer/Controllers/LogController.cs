using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Log;
using KarmicEnergy.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class LogController : BaseController
    {
        #region Index

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Index()
        {
            var logs = KEUnitOfWork.LogRepository.GetAll().ToList();
            var viewModels = ListViewModel.Map(logs);

            return View(viewModels);
        }

        #endregion Index       
    }
}