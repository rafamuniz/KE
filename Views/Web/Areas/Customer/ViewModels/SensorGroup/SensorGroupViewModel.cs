using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.SensorGroup
{
    public class SensorGroupViewModel
    {
        #region Property
        [Display(Name = "Site")]
        public Guid SiteId { get; set; }

        [Display(Name = "Tank")]
        public Guid TankId { get; set; }

        [Display(Name = "Tank")]
        public String TankName { get; set; }

        [Display(Name = "Sensor")]
        public Guid SensorId { get; set; }

        [Display(Name = "Sensor")]
        public String SensorName { get; set; }

        [Display(Name = "Weight")]
        public Int32 Weight { get; set; }
        #endregion Property

        #region Map
        public static List<SensorGroupViewModel> Map(List<Core.Entities.SensorGroup> entities)
        {
            List<SensorGroupViewModel> vms = new List<SensorGroupViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(SensorGroupViewModel.Map(c)));
            }

            return vms;
        }

        public static SensorGroupViewModel Map(Core.Entities.SensorGroup entity)
        {
            Mapper.CreateMap<Core.Entities.SensorGroup, SensorGroupViewModel>();
            var viewModel = Mapper.Map<Core.Entities.SensorGroup, SensorGroupViewModel>(entity);
            viewModel.TankId = entity.Sensor.TankId;
            viewModel.TankName = entity.Sensor.Tank.Name;
            return viewModel;
        }

        #endregion Map
    }
}