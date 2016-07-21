using KarmicEnergy.Core.Entities;
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
                SyncData sync = new SyncData();
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