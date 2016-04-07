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
        public Decimal Width { get; set; }

        [Display(Name = "Height")]
        public Decimal Height { get; set; }

        [Display(Name = "Length")]
        public Decimal Length { get; set; }

        [Display(Name = "Min Distance")]
        public Decimal MinDistance { get; set; }

        [Display(Name = "Max Distance")]
        public Decimal MaxDistance { get; set; }
        
        #endregion Property

        #region Map

        public Core.Entities.Tank Map()
        {
            Mapper.CreateMap<TankModelViewModel, Core.Entities.Tank>();
            return Mapper.Map<TankModelViewModel, Core.Entities.Tank>(this);
        }

        #endregion Map
    }
}