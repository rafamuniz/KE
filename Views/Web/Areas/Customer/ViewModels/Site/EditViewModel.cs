using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site
{
    public class EditViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "IP Address")]
        [Required]
        public String IPAddress { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        #region Map

        public static EditViewModel Map(Core.Entities.Site entity)
        {
            Mapper.CreateMap<Core.Entities.Site, EditViewModel>();
            return Mapper.Map<Core.Entities.Site, EditViewModel>(entity);
        }

        #endregion Map
    }
}