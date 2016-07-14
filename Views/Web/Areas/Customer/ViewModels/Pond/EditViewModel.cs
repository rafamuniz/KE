using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Pond
{
    public class EditViewModel
    {
        #region Constructor
        public EditViewModel()
        {

        }
        #endregion Constructor

        #region Property
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Reference")]
        [Required]
        [MaxLength(8)]
        public String Reference { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "Site")]
        [Required]
        public Guid SiteId { get; set; }

        [Display(Name = "Latitude")]
        public String Latitude { get; set; }

        [Display(Name = "Longitude")]
        public String Longitude { get; set; }

        [Display(Name = "Water Volume Capacity")]
        public Decimal? WaterVolumeCapacity { get; set; }

        #endregion Property

        #region Map

        public void Map(Core.Entities.Pond entity)
        {
            Mapper.Map<Core.Entities.Pond, EditViewModel>(entity, this);
        }

        public void MapVMToEntity(Core.Entities.Pond entity)
        {
            Mapper.Map<EditViewModel, Core.Entities.Pond>(this, entity);
        }

        #endregion Map
    }
}