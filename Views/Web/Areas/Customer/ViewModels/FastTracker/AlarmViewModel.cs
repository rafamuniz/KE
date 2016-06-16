using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker
{
    public class AlarmViewModel
    {
        #region Constructor
        public AlarmViewModel()
        {

        }
        #endregion Constructor

        #region Property

        public Guid Id { get; set; }
        public Guid TriggerId { get; set; }

        public Int16 SeverityId { get; set; }

        public Int32 ItemId { get; set; }
        public String ItemName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        #endregion Property        

        #region Map

        public static List<AlarmViewModel> Map(List<Core.Entities.Alarm> entities)
        {
            List<AlarmViewModel> vms = new List<AlarmViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(AlarmViewModel.Map(c)));
            }

            return vms;
        }

        public static AlarmViewModel Map(Core.Entities.Alarm entity)
        {
            Mapper.CreateMap<Core.Entities.Alarm, AlarmViewModel>();
            var viewModel = Mapper.Map<Core.Entities.Alarm, AlarmViewModel>(entity);

            viewModel.ItemId = entity.Trigger.SensorItem.Item.Id;
            viewModel.ItemName = entity.Trigger.SensorItem.Item.Name;
            viewModel.SeverityId = entity.Trigger.SeverityId;

            return viewModel;
        }

        #endregion Map      
    }
}