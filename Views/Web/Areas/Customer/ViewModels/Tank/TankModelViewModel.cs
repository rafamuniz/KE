using AutoMapper;
using Munizoft.MVC.Helpers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class TankModelViewModel
    {
        #region Property

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

        public String DimensionTitle1 { get; set; }

        [Display(Name = "Dimension 1")]
        public Decimal? Dimension1 { get; set; }

        [Display(Name = "Dimension 2")]
        public Decimal? Dimension2 { get; set; }

        [Display(Name = "Dimension 3")]
        public Decimal? Dimension3 { get; set; }

        [Display(Name = "Minimum Distance")]
        [Required]
        public Int32 MinimumDistance { get; set; }

        [Display(Name = "Maximum Distance")]
        [Required]
        public Int32 MaximumDistance { get; set; }

        [Display(Name = "Water Volume Capacity", ShortName = "WVC")]
        public Decimal? WaterVolumeCapacity { get; set; }

        #endregion Property

        #region Map

        public Core.Entities.TankModel Map()
        {         
            return Mapper.Map<TankModelViewModel, Core.Entities.TankModel>(this);
        }

        #endregion Map
    }
}