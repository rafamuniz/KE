using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor
{
    public class EditViewModel
    {
        #region Constructor

        public EditViewModel()
        {
            Items = new List<ItemViewModel>();
        }

        #endregion Constructor

        #region Property
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Reference")]
        [MaxLength(8)]
        public String Reference { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "Site")]
        public Guid? SiteId { get; set; }

        [Display(Name = "Tank")]
        public Guid? TankId { get; set; }

        [Display(Name = "Pond")]
        public Guid? PondId { get; set; }

        [Display(Name = "Sensor Type")]
        [Required]
        public Int16 SensorTypeId { get; set; }

        [Display(Name = "Spot GPS")]
        public String SpotGPS { get; set; }

        public IList<ItemViewModel> Items { get; set; }

        #endregion Property

        #region Map

        public static EditViewModel Map(Core.Entities.Sensor entity)
        {            
            return Mapper.Map<Core.Entities.Sensor, EditViewModel>(entity);
        }

        #endregion Map
    }
}