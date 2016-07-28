using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Monitoring
{
    public class EventInfoViewModel
    {
        #region Constructor
        public EventInfoViewModel()
        {
            Histories = new List<EventDetailViewModel>();
        }
        #endregion Constructor

        #region Property

        public Guid Id { get; set; }
        public EventDetailViewModel Detail { get; set; }
        public List<EventDetailViewModel> Histories { get; set; }

        #endregion Property

        #region Map

        public static List<EventInfoViewModel> Map(List<Core.Entities.SensorItemEvent> entities)
        {
            List<EventInfoViewModel> vms = new List<EventInfoViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(EventInfoViewModel.Map(c)));
            }

            return vms;
        }

        public static EventInfoViewModel Map(Core.Entities.SensorItemEvent entity)
        {
            var viewModel = Mapper.Map<Core.Entities.SensorItemEvent, EventInfoViewModel>(entity);

            return viewModel;
        }

        #endregion Map
    }
}