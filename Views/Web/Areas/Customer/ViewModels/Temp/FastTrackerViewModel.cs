using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class FastTrackerViewModel
    {
        #region Property
        
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        public Guid SiteId { get; set; }

        public Guid TankId { get; set; }

        #endregion Property

        #region Map

        public static List<FastTrackerViewModel> Map(List<Core.Entities.Tank> entities)
        {
            List<FastTrackerViewModel> vms = new List<FastTrackerViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(FastTrackerViewModel.Map(c)));
            }

            return vms;
        }

        public static FastTrackerViewModel Map(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<Core.Entities.Tank, FastTrackerViewModel>();
            return Mapper.Map<Core.Entities.Tank, FastTrackerViewModel>(entity);
        }

        #endregion Map
    }
}