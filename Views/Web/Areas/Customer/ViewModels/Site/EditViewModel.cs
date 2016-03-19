using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site
{
    public class EditViewModel
    {
        #region Property
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "IP Address")]
        [Required]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")]
        public String IPAddress { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }
        #endregion Property

        #region Map

        public Core.Entities.Site MapUpdate(Core.Entities.Site entity)
        {
            Mapper.CreateMap<EditViewModel, Core.Entities.Site>();
            return Mapper.Map<EditViewModel, Core.Entities.Site>(this, entity);
        }

        public static EditViewModel Map(Core.Entities.Site entity)
        {
            Mapper.CreateMap<Core.Entities.Site, EditViewModel>();
            return Mapper.Map<Core.Entities.Site, EditViewModel>(entity);
        }

        #endregion Map
    }
}