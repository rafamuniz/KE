using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.SensorGroup
{
    public class EditViewModel
    {
        #region Constructor
        public EditViewModel()
        {
            AvailableItems = new List<ItemViewModel>();
            SelectedItems = new List<ItemViewModel>();
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

        [Display(Name = "Tank")]
        [Required]
        public Guid TankId { get; set; }

        [Display(Name = "Sensor Type")]
        [Required]
        public Int16 SensorTypeId { get; set; }

        [Display(Name = "Spot GPS")]
        public String SpotGPS { get; set; }

        [Display(Name = "Items")]
        public String[] Items { get; set; }

        public IList<ItemViewModel> AvailableItems { get; set; }
        public IList<ItemViewModel> SelectedItems { get; set; }

        #endregion Property

        #region Map

        public static EditViewModel Map(Core.Entities.Sensor entity)
        {
            Mapper.CreateMap<Core.Entities.Sensor, EditViewModel>();
            return Mapper.Map<Core.Entities.Sensor, EditViewModel>(entity);
        }

        #endregion Map
    }
}