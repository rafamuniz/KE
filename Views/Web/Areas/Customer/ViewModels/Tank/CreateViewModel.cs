using AutoMapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class CreateViewModel
    {
        #region Constructor
        public CreateViewModel()
        {
            TankModelViewModel = new TankModelViewModel();
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

        [Display(Name = "Tank Model")]
        [Required]
        public Int32 TankModelId { get; set; }

        [Display(Name = "Stick Conversion")]
        public Int32 StickConversionId { get; set; }

        [Display(Name = "Latitude")]
        public String Latitude { get; set; }

        [Display(Name = "Longitude")]
        public String Longitude { get; set; }

        [Display(Name = "Width")]
        public Decimal? Width { get; set; }

        [Display(Name = "Height")]
        public Decimal? Height { get; set; }

        [Display(Name = "Length")]
        public Decimal? Length { get; set; }

        [Display(Name = "Face Length")]
        public Decimal? FaceLength { get; set; }

        [Display(Name = "Bottom Width")]
        public Decimal? BottomWidth { get; set; }

        [Display(Name = "Dimension 1")]
        public Decimal? Dimension1 { get; set; }

        [Display(Name = "Dimension 2")]
        public Decimal? Dimension2 { get; set; }

        [Display(Name = "Dimension 3")]
        public Decimal? Dimension3 { get; set; }

        [Display(Name = "Minimum Distance")]
        public Decimal? MinimumDistance { get; set; }

        [Display(Name = "Maximum Distance")]
        public Decimal? MaximumDistance { get; set; }

        [Display(Name = "Water Volume Capacity")]
        public Decimal? WaterVolumeCapacity { get; set; }

        #endregion Property

        #region TankModeViewModel

        public TankModelViewModel TankModelViewModel { get; set; }

        #endregion TankModeViewModel

        #region Map

        public Core.Entities.Tank Map()
        {
            Mapper.CreateMap<CreateViewModel, Core.Entities.Tank>();
            return Mapper.Map<CreateViewModel, Core.Entities.Tank>(this);
        }

        #endregion Map
    }
}