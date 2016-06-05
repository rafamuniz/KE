using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Monitoring;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class MonitoringController : BaseController
    {
        #region Index

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public async Task<ActionResult> Index()
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();

            if (!IsSite)
            {
                var alarms = KEUnitOfWork.AlarmRepository.GetAllActive().ToList();
                viewModels = ListViewModel.Map(alarms);
            }
            else
            {
                var alarms = KEUnitOfWork.AlarmRepository.GetsBySite(SiteId).ToList();
                viewModels = ListViewModel.Map(alarms);
            }

            if (viewModels.Any())
            {
                foreach (var vm in viewModels.Where(x => x.AckUserId.HasValue))
                {
                    var user = await UserManager.FindByIdAsync(vm.AckUserId.Value.ToString());
                    vm.AckUser = user.Name;
                }
            }

            return View(viewModels);
        }

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public async Task<ActionResult> Tank(Guid tankId)
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();

            var alarms = KEUnitOfWork.AlarmRepository.GetsByTank(tankId).ToList();
            viewModels = ListViewModel.Map(alarms.OrderByDescending(x => x.Trigger.SeverityId).ToList());

            if (viewModels.Any())
            {
                foreach (var vm in viewModels.Where(x => x.AckUserId.HasValue))
                {
                    var user = await UserManager.FindByIdAsync(vm.AckUserId.Value.ToString());
                    vm.AckUser = user.Name;
                }
            }

            return View("Index", viewModels);
        }

        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public async Task<ActionResult> Alarm(Guid alarmId)
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();

            var alarm = KEUnitOfWork.AlarmRepository.Get(alarmId);
            viewModels.Add(ListViewModel.Map(alarm));

            if (viewModels.Any())
            {
                foreach (var vm in viewModels.Where(x => x.AckUserId.HasValue))
                {
                    var user = await UserManager.FindByIdAsync(vm.AckUserId.Value.ToString());
                    vm.AckUser = user.Name;
                }
            }

            return View("Index", viewModels);
        }

        #endregion Index

        #region Ack

        //
        // POST: /Monitoring/Acknowledge
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult Acknowledge(Guid id)
        {
            try
            {
                var alarm = KEUnitOfWork.AlarmRepository.Get(id);

                alarm.LastAckDate = DateTime.UtcNow;
                alarm.LastAckUserId = Guid.Parse(UserId);

                AlarmHistory alarmHistory = new AlarmHistory()
                {
                    AckDate = alarm.LastAckDate.Value,
                    AckUserId = alarm.LastAckUserId.Value,
                    AlarmId = alarm.Id,
                    Value = alarm.Value,
                    CalculatedValue = alarm.CalculatedValue
                };

                KEUnitOfWork.AlarmHistoryRepository.Add(alarmHistory);
                KEUnitOfWork.AlarmRepository.Update(alarm);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View();
        }

        #endregion Ack

        [HttpGet]
        public ActionResult GetsTankBySiteId(Guid siteId)
        {
            var tanks = LoadTanks(CustomerId, siteId);
            SelectList obgTanks = new SelectList(tanks, "Id", "Name", 0);
            return Json(obgTanks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetsSensorByTankId(Guid tankId)
        {
            var sensors = LoadSensors(CustomerId, tankId);
            SelectList obgSensors = new SelectList(sensors, "Id", "Name", 0);
            return Json(obgSensors, JsonRequestBehavior.AllowGet);
        }
    }
}
