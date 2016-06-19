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

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Index()
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();

            if (!IsSite)
            {
                var events = KEUnitOfWork.SensorItemEventRepository.GetAllActive().ToList();
                viewModels = ListViewModel.Map(events);
            }
            else
            {
                var events = KEUnitOfWork.SensorItemEventRepository.GetsBySite(SiteId);
                viewModels = ListViewModel.Map(events);
            }

            return View(viewModels);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Site(Guid siteId)
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();

            //tankId
            var events = KEUnitOfWork.SensorItemEventRepository.GetsBySite(siteId).ToList();
            viewModels = ListViewModel.Map(events.OrderByDescending(x => x.EventDate).ToList());

            return View("Index", viewModels);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Tank(Guid tankId)
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();

            //tankId
            var events = KEUnitOfWork.SensorItemEventRepository.GetAllActive().ToList();
            viewModels = ListViewModel.Map(events.OrderByDescending(x => x.EventDate).ToList());

            return View("Index", viewModels);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Event(Guid eventId)
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();

            var evt = KEUnitOfWork.SensorItemEventRepository.Get(eventId);
            viewModels.Add(ListViewModel.Map(evt));

            return View("Index", viewModels);
        }

        #endregion Index

        #region Alarm

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public async Task<ActionResult> Alarms()
        {
            List<AlarmViewModel> viewModels = new List<AlarmViewModel>();

            if (!IsSite)
            {
                var alarms = KEUnitOfWork.AlarmRepository.GetAllActive().ToList();
                viewModels = AlarmViewModel.Map(alarms);
            }
            else
            {
                var alarms = KEUnitOfWork.AlarmRepository.GetsBySite(SiteId).ToList();
                viewModels = AlarmViewModel.Map(alarms);
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

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        [Route("alarm/site/{siteId?}")]
        public async Task<ActionResult> AlarmBySite(Guid siteId)
        {
            List<AlarmViewModel> viewModels = new List<AlarmViewModel>();

            var alarms = KEUnitOfWork.AlarmRepository.GetsBySite(siteId);
            viewModels = AlarmViewModel.Map(alarms.OrderByDescending(x => x.Trigger.SeverityId).ToList());

            if (viewModels.Any())
            {
                foreach (var vm in viewModels.Where(x => x.AckUserId.HasValue))
                {
                    var user = await UserManager.FindByIdAsync(vm.AckUserId.Value.ToString());
                    vm.AckUser = user.Name;
                }
            }

            return View("Alarms", viewModels);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        [Route("alarm/tank/{tankId?}")]
        public async Task<ActionResult> AlarmByTank(Guid tankId)
        {
            List<AlarmViewModel> viewModels = new List<AlarmViewModel>();

            var alarms = KEUnitOfWork.AlarmRepository.GetsByTank(tankId).ToList();
            viewModels = AlarmViewModel.Map(alarms.OrderByDescending(x => x.Trigger.SeverityId).ToList());

            if (viewModels.Any())
            {
                foreach (var vm in viewModels.Where(x => x.AckUserId.HasValue))
                {
                    var user = await UserManager.FindByIdAsync(vm.AckUserId.Value.ToString());
                    vm.AckUser = user.Name;
                }
            }

            return View("Alarms", viewModels);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public async Task<ActionResult> AlarmById(Guid alarmId)
        {
            List<AlarmViewModel> viewModels = new List<AlarmViewModel>();

            var alarm = KEUnitOfWork.AlarmRepository.Get(alarmId);
            viewModels.Add(AlarmViewModel.Map(alarm));

            if (viewModels.Any())
            {
                foreach (var vm in viewModels.Where(x => x.AckUserId.HasValue))
                {
                    var user = await UserManager.FindByIdAsync(vm.AckUserId.Value.ToString());
                    vm.AckUser = user.Name;
                }
            }

            return View("Alarms", viewModels);
        }

        #endregion Alarms

        #region Ack

        //
        // POST: /Monitoring/Acknowledge
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Acknowledge(Guid id)
        {
            try
            {
                var alarm = KEUnitOfWork.AlarmRepository.Get(id);

                alarm.LastAckDate = DateTime.UtcNow;
                alarm.LastAckUserId = Guid.Parse(UserId);

                AlarmHistory alarmHistory = new AlarmHistory()
                {
                    UserId = Guid.Parse(UserId),
                    Action = "ACK",
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

        #region Clear Alarm

        //
        // POST: /Monitoring/Acknowledge
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        [Route("alarm/clear/{id?}")]
        public ActionResult ClearAlarm(Guid id)
        {
            try
            {
                var alarm = KEUnitOfWork.AlarmRepository.Get(id);

                alarm.EndDate = DateTime.UtcNow;
                
                AlarmHistory alarmHistory = new AlarmHistory()
                {                    
                    UserId = Guid.Parse(UserId),
                    Action = "CLEAR",
                    Message = "Alarm cleared",
                    AlarmId = alarm.Id,
                    Value = alarm.Value,
                    CalculatedValue = alarm.CalculatedValue
                };

                KEUnitOfWork.AlarmHistoryRepository.Add(alarmHistory);
                KEUnitOfWork.AlarmRepository.Update(alarm);
                KEUnitOfWork.Complete();

                return View();
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View();
        }

        #endregion Clear Alarm

        #region Comments

        //
        // POST: /Monitoring/AddComment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult AddComment(Guid id, String message)
        {
            try
            {
                var alarm = KEUnitOfWork.AlarmRepository.Get(id);
                                
                AlarmHistory alarmHistory = new AlarmHistory()
                {
                    UserId = Guid.Parse(UserId),
                    Action = "COMMENT",
                    Message = message,
                    AlarmId = alarm.Id,
                    Value = alarm.Value,
                    CalculatedValue = alarm.CalculatedValue
                };

                KEUnitOfWork.AlarmHistoryRepository.Add(alarmHistory);                
                KEUnitOfWork.Complete();

                return View();
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View();
        }

        #endregion Comments

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsTankBySiteId(Guid siteId)
        {
            var tanks = LoadTankSensors(CustomerId, siteId);
            SelectList obgTanks = new SelectList(tanks, "Id", "Name", 0);
            return Json(obgTanks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsSensorByTankId(Guid tankId)
        {
            var sensors = LoadTankSensors(CustomerId, tankId);
            SelectList obgSensors = new SelectList(sensors, "Id", "Name", 0);
            return Json(obgSensors, JsonRequestBehavior.AllowGet);
        }
    }
}
