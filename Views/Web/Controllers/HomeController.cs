using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;

namespace KarmicEnergy.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize()]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Customer, CustomerAdmin, CustomerOperator")]
        public ActionResult GetsAlarm(Int32 quantity = 5)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();
            List<AlarmViewModel> viewModels = new List<AlarmViewModel>();
            List<Alarm> alarms = new List<Alarm>();

            if (IsSite)
            {
                alarms = KEUnitOfWork.AlarmRepository.GetsActiveBySite(SiteId);
            }
            else
            {
                alarms = KEUnitOfWork.AlarmRepository.GetsActiveByCustomer(CustomerId);
            }

            notificationViewModel.Alarms = AlarmViewModel.Map(alarms);

            if (alarms.Any())
            {
                notificationViewModel.HasAlarmCritical = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Critical).Any();
                notificationViewModel.HasAlarmMedium = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Medium).Any();
                notificationViewModel.HasAlarmLow = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Low).Any();
            }

            return Json(notificationViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}