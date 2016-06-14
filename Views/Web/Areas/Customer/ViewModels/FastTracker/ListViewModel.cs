using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker
{
    public class ListViewModel
    {
        #region Constructor
        public ListViewModel()
        {
            Tanks = new List<TankViewModel>();            
            FlowMeters = new List<FlowMeterViewModel>();
            Alarms = new List<AlarmViewModel>();
        }
        #endregion Constructor

        #region Property

        public Guid? SiteId { get; set; }

        public String IPAddress { get; set; }

        public String Latitude { get; set; }

        public String Longitude { get; set; }

        public List<TankViewModel> Tanks { get; set; }

        public List<FlowMeterViewModel> FlowMeters { get; set; }

        public List<AlarmViewModel> Alarms { get; set; }

        #endregion Property

        #region Map

        //public static List<ListViewModel> Map(List<Core.Entities.Tank> entities)
        //{
        //    List<ListViewModel> vms = new List<ListViewModel>();

        //    if (entities != null && entities.Any())
        //    {
        //        entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
        //    }

        //    return vms;
        //}

        //public static ListViewModel Map(Core.Entities.Site entity)
        //{
        //    Mapper.CreateMap<Core.Entities.Site, ListViewModel>();
        //    return Mapper.Map<Core.Entities.Site, ListViewModel>(entity);
        //}

        public ListViewModel Map(Core.Entities.Site entity)
        {
            Mapper.CreateMap<Core.Entities.Site, ListViewModel>();
            return Mapper.Map<Core.Entities.Site, ListViewModel>(entity);
        }

        #endregion Map
    }
}