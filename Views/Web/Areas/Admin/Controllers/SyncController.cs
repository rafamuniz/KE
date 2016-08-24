using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Services.Interface;
using KarmicEnergy.Web.Areas.Admin.ViewModels.Sync;
using KarmicEnergy.Web.Controllers;
using KarmicEnergy.Web.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class SyncController : BaseController
    {
        #region Fields
        private readonly ILogService _logService;
        #endregion Fields

        #region Constructor
        public SyncController(ILogService logService)
        {
            this._logService = logService;
        }
        #endregion Constructor

        #region Index

        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Index()
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();
            var entities = KEUnitOfWork.DataSyncRepository.GetAll().OrderByDescending(o => o.StartDate).Take(30);

            if (entities != null && entities.Any())
            {
                viewModels = ListViewModel.Map(entities.ToList());
            }

            AddLog("Navigated to Sync View", LogTypeEnum.Info);
            return View(viewModels);
        }

        #endregion Index    

        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Sync()
        {
            try
            {
                SyncData sync = new SyncData(this._logService);
                sync.Execute();
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return RedirectToAction("Index");
        }
    }
}