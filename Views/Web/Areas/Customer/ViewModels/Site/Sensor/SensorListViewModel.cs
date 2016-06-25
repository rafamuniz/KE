using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site.Sensor
{
    public class SensorListViewModel
    {
        #region Constructor
        public SensorListViewModel()
        {
            Sensors = new List<SensorViewModel>();
        }
        #endregion Constructor

        #region Property

        [Display(Name = "Site")]
        public Guid? SiteId { get; set; }

        [Display(Name = "Tank")]
        public Guid? TankId { get; set; }

        public List<SensorViewModel> Sensors { get; set; }

        #endregion Property

        #region Map

        public static List<SensorListViewModel> Map(List<Core.Entities.Sensor> entities)
        {
            List<SensorListViewModel> vms = new List<SensorListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(SensorListViewModel.Map(c)));
            }

            return vms;
        }

        public static SensorListViewModel Map(Core.Entities.Sensor entity)
        {
            Mapper.CreateMap<Core.Entities.Sensor, SensorListViewModel>();
            return Mapper.Map<Core.Entities.Sensor, SensorListViewModel>(entity);
        }

        #endregion Map
    }
}