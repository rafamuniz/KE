using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site
{
    public class ReportViewModel
    {
        #region Constructor
        public ReportViewModel()
        {
            Tanks = new List<TankViewModel>();
            FlowMeters = new List<FlowMeterViewModel>();
        }
        #endregion Constructor

        #region Property

        public Guid SiteId { get; set; }

        public List<TankViewModel> Tanks { get; set; }
        public List<FlowMeterViewModel> FlowMeters { get; set; }

        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.Site> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.Site entity)
        {
            Mapper.CreateMap<Core.Entities.Site, ListViewModel>();
            return Mapper.Map<Core.Entities.Site, ListViewModel>(entity);
        }

        #endregion Map
    }
}