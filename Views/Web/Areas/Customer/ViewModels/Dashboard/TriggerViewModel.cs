using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard
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

        public String Name { get; set; }

        public String MinValue { get; set; }
        public String MaxValue { get; set; }

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
   
            var viewModel = Mapper.Map<Core.Entities.Trigger, TriggerViewModel>(entity);

            viewModel.Name = entity.SensorItem.Item.Name;

            return viewModel;
        }

        #endregion Map      
    }
}