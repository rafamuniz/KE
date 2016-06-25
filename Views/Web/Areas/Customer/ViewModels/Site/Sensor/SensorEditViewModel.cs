using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site.Sensor
{
    public class SensorEditViewModel
    {
        #region Constructor

        public SensorEditViewModel()
        {
            Items = new List<SensorItemViewModel>();
        }

        #endregion Constructor

        #region Property
        [Required]
        public Guid Id { get; set; }
        
        [Display(Name = "Site")]
        [Required]
        public Guid SiteId { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Reference")]
        [MaxLength(8)]
        public String Reference { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "Sensor Type")]
        [Required]
        public Int16 SensorTypeId { get; set; }

        [Display(Name = "Spot GPS")]
        public String SpotGPS { get; set; }

        public IList<SensorItemViewModel> Items { get; set; }

        #endregion Property

        #region Map

        public static SensorEditViewModel Map(Core.Entities.Sensor entity)
        {
            Mapper.CreateMap<Core.Entities.Sensor, SensorEditViewModel>();
            return Mapper.Map<Core.Entities.Sensor, SensorEditViewModel>(entity);
        }

        #endregion Map
    }
}