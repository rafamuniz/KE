using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class EditViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Description")]
        [MaxLength]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "SiteId")]
        [Required]
        public Guid SiteId { get; set; }

        [Display(Name = "TankModelId")]
        [Required]
        public Int32 TankModelId { get; set; }

        #region Map

        public static EditViewModel Map(Core.Entities.CustomerUser entity)
        {
            Mapper.CreateMap<Core.Entities.CustomerUser, EditViewModel>();
            return Mapper.Map<Core.Entities.CustomerUser, EditViewModel>(entity);
        }

        #endregion Map
    }
}