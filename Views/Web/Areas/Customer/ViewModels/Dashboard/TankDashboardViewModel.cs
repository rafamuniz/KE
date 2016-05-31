using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard
{
    public class TankDashboardViewModel
    {
        #region Constructor
        public TankDashboardViewModel()
        {
            Tanks = new List<TankViewModel>();
        }
        #endregion Constructor

        #region Property

        public Guid SiteId { get; set; }

        public List<TankViewModel> Tanks { get; set; }

        #endregion Property

        #region Map

        public static List<TankDashboardViewModel> Map(List<Core.Entities.Tank> entities)
        {
            List<TankDashboardViewModel> vms = new List<TankDashboardViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(TankDashboardViewModel.Map(c)));
            }

            return vms;
        }

        public static TankDashboardViewModel Map(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<Core.Entities.Tank, TankDashboardViewModel>();
            return Mapper.Map<Core.Entities.Tank, TankDashboardViewModel>(entity);
        }

        #endregion Map
    }
}