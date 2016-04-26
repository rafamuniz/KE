using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class TankWaterCapacityViewModel
    {
        #region Constructor
        public TankWaterCapacityViewModel()
        {

        }
        #endregion Constructor

        #region Property

        public Int32 TankModelId { get; set; }

        public Decimal WaterVolumeCapacity { get; set; }

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
        public Int32? MinimumDistance { get; set; }

        [Display(Name = "Maximum Distance")]
        public Int32? MaximumDistance { get; set; }

        #endregion Property        
    }
}