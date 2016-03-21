using AutoMapper;
using Munizoft.MVC.Helpers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class EditViewModel
    {
        #region Property
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Description")]
        [MaxLength]
        public String Description { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "Site")]
        [Required]
        public Guid SiteId { get; set; }

        [Display(Name = "Tank Model")]
        [Required]
        public Int32 TankModelId { get; set; }

        public IEnumerable<ImageSelectListItem> TankModels { get; set; }

        #endregion Property

        #region Map

        public static EditViewModel Map(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<Core.Entities.Tank, EditViewModel>();
            return Mapper.Map<Core.Entities.Tank, EditViewModel>(entity);
        }

        #endregion Map
    }
}