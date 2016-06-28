using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Monitoring
{
    public class ListAlarmViewModel
    {
        #region Constructor
        public ListAlarmViewModel()
        {
            Alarms = new List<AlarmDetailViewModel>();
        }
        #endregion Constructor

        #region Property

        public List<AlarmDetailViewModel> Alarms { get; set; }

        #endregion Property

        #region Map

        public static List<ListAlarmViewModel> Map(List<Core.Entities.Alarm> entities)
        {
            List<ListAlarmViewModel> vms = new List<ListAlarmViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListAlarmViewModel.Map(c)));
            }

            return vms;
        }

        public static ListAlarmViewModel Map(Core.Entities.Alarm entity)
        {
            Mapper.CreateMap<Core.Entities.Alarm, ListAlarmViewModel>();
            var viewModel = Mapper.Map<Core.Entities.Alarm, ListAlarmViewModel>(entity);
            
            return viewModel;
        }

        #endregion Map
    }
}