using AutoMapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Pond
{
    public class CreateViewModel
    {
        #region Constructor
        public CreateViewModel()
        {
        }

        #endregion Constructor

        #region Property

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "Reference")]
        [Required]
        [MaxLength(8)]
        public String Reference { get; set; }

        [Display(Name = "Status")]
        [DefaultValue("A")]
        [Required]
        public String Status { get; set; } = "A";

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

        public Core.Entities.Pond Map()
        {
            Mapper.CreateMap<CreateViewModel, Core.Entities.Pond>();
            return Mapper.Map<CreateViewModel, Core.Entities.Pond>(this);
        }

        #endregion Map
    }
}