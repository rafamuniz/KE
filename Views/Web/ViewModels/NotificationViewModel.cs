using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.ViewModels
{
    public class NotificationViewModel
    {
        #region Constructor
        public NotificationViewModel()
        {
            Alarms = new List<AlarmViewModel>();
        }
        #endregion Constructor

        #region Property

        public Boolean HasAlarmCritical { get; set; }
        public Boolean HasAlarmMedium { get; set; }
        public Boolean HasAlarmLow { get; set; }
        public List<AlarmViewModel> Alarms { get; set; }

        #endregion Property        
    }
}