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
            List<SensorItemEvent> events = new List<SensorItemEvent>();

            if (!IsSite)
            {
                 events = KEUnitOfWork.SensorItemEventRepository.GetAllActive().ToList();                
            }
            else
            {
                events = KEUnitOfWork.SensorItemEventRepository.GetsBySite(SiteId);            
            }

            var evts = events.Any() ? events.OrderByDescending(d => d.EventDate).ToList() : events;
            viewModels = ListViewModel.Map(evts);

            AddLog("Navigated to Monitoring View", LogTypeEnum.Info);

            return View(viewModels);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Site(Guid siteId)
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();
            var events = KEUnitOfWork.SensorItemEventRepository.GetsBySite(siteId).ToList();
            viewModels = ListViewModel.Map(events.OrderByDescending(x => x.EventDate).ToList());
            return View("Index", viewModels);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Pond(Guid pondId)
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();
            var events = KEUnitOfWork.SensorItemEventRepository.GetsByPond(pondId).ToList();
            viewModels = ListViewModel.Map(events.OrderByDescending(x => x.EventDate).ToList());
            return View("Index", viewModels);
        }


        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Tank(Guid tankId)
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();
            var events = KEUnitOfWork.SensorItemEventRepository.GetsByTank(tankId).ToList();
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
            ListAlarmViewModel viewModel = new ListAlarmViewModel();
            List<AlarmDetailViewModel> viewModels = new List<AlarmDetailViewModel>();

            if (!IsSite)
            {
                var alarms = KEUnitOfWork.AlarmRepository.GetsActiveByCustomer(CustomerId).ToList();
                viewModels = AlarmDetailViewModel.Map(alarms);
            }
            else
            {
                var alarms = KEUnitOfWork.AlarmRepository.GetsBySite(SiteId).ToList();
                viewModels = AlarmDetailViewModel.Map(alarms);
            }

            viewModel.Alarms = viewModels;

            return View(viewModel);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        [Route("alarm/site/{siteId?}")]
        public async Task<ActionResult> AlarmBySite(Guid siteId)
        {
            ListAlarmViewModel viewModel = new ListAlarmViewModel();
            List<AlarmDetailViewModel> viewModels = new List<AlarmDetailViewModel>();
            var alarms = KEUnitOfWork.AlarmRepository.GetsBySite(siteId);
            viewModels = AlarmDetailViewModel.Map(alarms.OrderByDescending(x => x.Trigger.SeverityId).ToList());
            viewModel.Alarms = viewModels;
            return View("Alarms", viewModel);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        [Route("alarm/tank/{tankId?}")]
        public async Task<ActionResult> AlarmByTank(Guid tankId)
        {
            ListAlarmViewModel viewModel = new ListAlarmViewModel();
            List<AlarmDetailViewModel> viewModels = new List<AlarmDetailViewModel>();
            var alarms = KEUnitOfWork.AlarmRepository.GetsByTank(tankId).ToList();
            viewModels = AlarmDetailViewModel.Map(alarms.OrderByDescending(x => x.Trigger.SeverityId).ToList());
            viewModel.Alarms = viewModels;
            return View("Alarms", viewModel);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public async Task<ActionResult> AlarmById(Guid alarmId)
        {
            ListAlarmViewModel viewModel = new ListAlarmViewModel();
            List<AlarmDetailViewModel> viewModels = new List<AlarmDetailViewModel>();
            var alarm = KEUnitOfWork.AlarmRepository.Get(alarmId);
            viewModels.Add(AlarmDetailViewModel.Map(alarm));
            viewModel.Alarms = viewModels;
            return View("Alarms", viewModel);
        }

        #endregion Alarms

        #region Ack

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Acknowledge(Guid id)
        {
            try
            {
                var alarm = KEUnitOfWork.AlarmRepository.Get(id);

                alarm.LastAckDate = DateTime.UtcNow;
                alarm.LastAckUserId = Guid.Parse(UserId);
                alarm.LastAckUserName = User.Identity.Name;

                AlarmHistory alarmHistory = new AlarmHistory()
                {
                    UserId = Guid.Parse(UserId),
                    UserName = alarm.LastAckUserName,
                    ActionTypeId = (Int16)ActionTypeEnum.Ack,
                    AlarmId = alarm.Id,
                    Value = alarm.Value,
                };

                KEUnitOfWork.AlarmHistoryRepository.Add(alarmHistory);
                KEUnitOfWork.AlarmRepository.Update(alarm);
                KEUnitOfWork.Complete();

                return RedirectToAction("Alarms");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View();
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public async Task<ActionResult> AlarmACK(Guid id)
        {
            try
            {
                var alarm = KEUnitOfWork.AlarmRepository.Get(id);

                alarm.LastAckDate = DateTime.UtcNow;
                alarm.LastAckUserId = Guid.Parse(UserId);
                alarm.LastAckUserName = User.Identity.Name;

                AlarmHistory alarmHistory = new AlarmHistory()
                {
                    UserId = Guid.Parse(UserId),
                    UserName = alarm.LastAckUserName,
                    ActionTypeId = (Int16)ActionTypeEnum.Ack,
                    AlarmId = alarm.Id,
                    Value = alarm.Value
                };

                KEUnitOfWork.AlarmHistoryRepository.Add(alarmHistory);
                KEUnitOfWork.AlarmRepository.Update(alarm);
                KEUnitOfWork.Complete();

                AlarmDetailViewModel viewModel = new AlarmDetailViewModel();
                viewModel = AlarmDetailViewModel.Map(alarm);

                return RedirectToAction("AlarmInfo/" + id);
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View();
        }

        #endregion Ack

        #region Clear Alarm

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult AlarmClear(Guid id)
        {
            ClearAlarmViewModel viewModel = new ClearAlarmViewModel();
            viewModel.AlarmId = id;
            viewModel.Detail = GetAlarmDetail(id);
            return View(viewModel);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Clear(ClearAlarmViewModel viewModel)
        {
            try
            {
                var alarm = KEUnitOfWork.AlarmRepository.Get(viewModel.AlarmId);

                alarm.EndDate = DateTime.UtcNow;

                AlarmHistory alarmHistory = new AlarmHistory()
                {
                    UserId = Guid.Parse(UserId),
                    ActionTypeId = (Int16)ActionTypeEnum.Clear,
                    Message = viewModel.Message,
                    AlarmId = alarm.Id,
                    Value = alarm.Value
                };

                KEUnitOfWork.AlarmHistoryRepository.Add(alarmHistory);
                KEUnitOfWork.AlarmRepository.Update(alarm);
                KEUnitOfWork.Complete();

                return RedirectToAction("Alarms");
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View();
        }

        #endregion Clear Alarm

        #region Comments

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult CreateComment(Guid id)
        {
            CreateCommentViewModel viewModel = new CreateCommentViewModel();
            viewModel.AlarmId = id;
            viewModel.Detail = GetAlarmDetail(id);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult CreateComment(CreateCommentViewModel viewModel)
        {
            try
            {
                var alarm = KEUnitOfWork.AlarmRepository.Get(viewModel.AlarmId);

                AlarmHistory alarmHistory = new AlarmHistory()
                {
                    UserId = Guid.Parse(UserId),
                    UserName = User.Identity.Name,
                    ActionTypeId = (Int16)ActionTypeEnum.Comment,
                    Message = viewModel.Comment,
                    AlarmId = alarm.Id,
                    Value = alarm.Value
                };

                KEUnitOfWork.AlarmHistoryRepository.Add(alarmHistory);
                KEUnitOfWork.Complete();

                return RedirectToAction("AlarmInfo/" + alarm.Id);
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View();
        }

        private List<CommentViewModel> GetsComment(Guid triggerId)
        {
            List<CommentViewModel> viewModels = new List<CommentViewModel>();
            var comments = KEUnitOfWork.AlarmHistoryRepository.GetsByActionType(triggerId, ActionTypeEnum.Comment);
            var cs = comments.Any() ? comments.OrderByDescending(d => d.CreatedDate).ToList() : comments;
            viewModels = CommentViewModel.Map(cs);
            return viewModels;
        }

        #endregion Comments

        #region Detail

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult AlarmInfo(Guid id)
        {
            AlarmInfoViewModel viewModel = new AlarmInfoViewModel();
            viewModel.Id = id;
            viewModel.Detail = GetAlarmDetail(id);
            viewModel.Comments = GetsComment(viewModel.Detail.TriggerId);
            viewModel.Histories = GetsHistory(viewModel.Detail.TriggerId);

            AddLog("Navigated to Alarm Info View", LogTypeEnum.Info);
            return View(viewModel);
        }

        private AlarmDetailViewModel GetAlarmDetail(Guid alarmId)
        {
            AlarmDetailViewModel viewModel = new AlarmDetailViewModel();
            var alarm = KEUnitOfWork.AlarmRepository.Get(alarmId);
            viewModel = AlarmDetailViewModel.Map(alarm);
            return viewModel;
        }

        private List<AlarmHistoryViewModel> GetsHistory(Guid triggerId)
        {
            List<AlarmHistoryViewModel> viewModels = new List<AlarmHistoryViewModel>();
            var histories = KEUnitOfWork.AlarmRepository.GetsCloseByTrigger(triggerId);
            var hs = histories.Any() ? histories.OrderByDescending(d => d.CreatedDate).ToList() : histories;
            viewModels = AlarmHistoryViewModel.Map(histories);
            return viewModels;
        }

        #endregion Detail

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
