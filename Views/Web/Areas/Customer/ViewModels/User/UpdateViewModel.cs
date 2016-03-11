using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.User
{
    public class EditViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public String Email { get; set; }

        #region Map

        public static EditViewModel Map(Core.Entities.CustomerUser entity)
        {
            Mapper.CreateMap<Core.Entities.CustomerUser, EditViewModel>();
            return Mapper.Map<Core.Entities.CustomerUser, EditViewModel>(entity);
        }

        #endregion Map
    }
}