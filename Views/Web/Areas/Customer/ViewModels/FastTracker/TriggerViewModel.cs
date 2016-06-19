using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker
{
    public class TriggerViewModel
    {
        #region Constructor
        public TriggerViewModel()
        {

        }
        #endregion Constructor

        #region Property

        public Guid Id { get; set; }

        public Int16 SeverityId { get; set; }

        public Int32 ItemId { get; set; }
        public String ItemName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Boolean HasAlarm { get; set; }

        #endregion Property        

        #region Map

        public static List<TriggerViewModel> Map(List<Core.Entities.Trigger> entities)
        {
            List<TriggerViewModel> vms = new List<TriggerViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(TriggerViewModel.Map(c)));
            }

            return vms;
        }

        public static TriggerViewModel Map(Core.Entities.Trigger entity)
        {
            Mapper.CreateMap<Core.Entities.Trigger, TriggerViewModel>();
            var viewModel = Mapper.Map<Core.Entities.Trigger, TriggerViewModel>(entity);

            viewModel.Id = entity.Id;
            viewModel.ItemId = entity.SensorItem.Item.Id;
            viewModel.ItemName = entity.SensorItem.Item.Name;
            viewModel.SeverityId = entity.SeverityId;

            return viewModel;
        }

        #endregion Map      
    }
}