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

        [Display(Name = "Minimum Distance")]
        public Decimal? MinimumDistance { get; set; }

        [Display(Name = "Maximum Distance")]
        public Decimal? MaximumDistance { get; set; }

        [Display(Name = "Water Volume Capacity")]
        public Decimal? WaterVolumeCapacity { get; set; }

        #endregion Property

        #region Map

        public Core.Entities.TankModel Map()
        {
            Mapper.CreateMap<TankModelViewModel, Core.Entities.TankModel>();
            return Mapper.Map<TankModelViewModel, Core.Entities.TankModel>(this);
        }

        #endregion Map
    }
}