using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard
{
    public class SiteDashboardViewModel
    {
        #region Constructor
        public SiteDashboardViewModel()
        {
            Tanks = new List<TankViewModel>();
            FlowMeters = new List<FlowMeterViewModel>();
        }
        #endregion Constructor

        #region Property

        public Guid? SiteId { get; set; }

        public List<TankViewModel> Tanks { get; set; }

        public List<FlowMeterViewModel> FlowMeters { get; set; }

        public List<AlarmViewModel> Alarms { get; set; }

        #endregion Property

        #region Map

        public static List<SiteDashboardViewModel> Map(List<Core.Entities.Site> entities)
        {
            List<SiteDashboardViewModel> vms = new List<SiteDashboardViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(SiteDashboardViewModel.Map(c)));
            }

            return vms;
        }

        public static SiteDashboardViewModel Map(Core.Entities.Site entity)
        {
            Mapper.CreateMap<Core.Entities.Site, SiteDashboardViewModel>();
            return Mapper.Map<Core.Entities.Site, SiteDashboardViewModel>(entity);
        }

        #endregion Map
    }
}