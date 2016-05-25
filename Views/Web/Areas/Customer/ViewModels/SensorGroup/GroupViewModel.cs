using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.SensorGroup
{
    public class CreateViewModel
    {
        #region Constructor
        public CreateViewModel()
        {
            Sensors = new List<SensorGroupViewModel>();
        }
        #endregion Constructor

        #region Property

        [Display(Name = "Group")]
        public Guid? GroupId { get; set; }

        [Display(Name = "Site")]
        public Guid? SiteId { get; set; }

        [Display(Name = "Site")]
        public Guid SiteName { get; set; }

        [Display(Name = "Tank")]
        [Required]
        public Guid? TankId { get; set; }

        [Display(Name = "Tank")]
        public Guid TankName { get; set; }

        [Display(Name = "Sensor")]
        [Required]
        public Guid? SensorId { get; set; }

        [Display(Name = "Sensor")]
        public Guid SensorName { get; set; }

        [Display(Name = "Weight")]
        [Required]
        public Int32? Weight { get; set; }

        public List<SensorGroupViewModel> Sensors { get; set; }

        #endregion Property

        #region Map

        public Core.Entities.SensorGroup Map()
        {
            Mapper.CreateMap<CreateViewModel, Core.Entities.Contact>();
            return Mapper.Map<CreateViewModel, Core.Entities.SensorGroup>(this);
        }

        #endregion Map
    }
}