using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Monitoring
{
    public class AlarmInfoViewModel
    {
        #region Constructor
        public AlarmInfoViewModel()
        {
            Histories = new List<AlarmHistoryViewModel>();
            Comments = new List<CommentViewModel>();
        }
        #endregion Constructor

        #region Property

        public Guid Id { get; set; }
        public AlarmDetailViewModel Detail { get; set; }
        public List<AlarmHistoryViewModel> Histories { get; set; }
        public List<CommentViewModel> Comments { get; set; }

        #endregion Property

        #region Map

        public static List<AlarmInfoViewModel> Map(List<Core.Entities.Alarm> entities)
        {
            List<AlarmInfoViewModel> vms = new List<AlarmInfoViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(AlarmInfoViewModel.Map(c)));
            }

            return vms;
        }

        public static AlarmInfoViewModel Map(Core.Entities.Alarm entity)
        {
         
            var viewModel = Mapper.Map<Core.Entities.Alarm, AlarmInfoViewModel>(entity);

            return viewModel;
        }

        #endregion Map
    }
}