using AutoMapper;
using KarmicEnergy.Web.Entities;
using KarmicEnergy.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.User
{
    public class EditViewModel
    {
        #region Constructor
        public EditViewModel()
        {
            Address = new AddressViewModel();
        }

        #endregion Constructor

        #region Property

        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Role")]
        public String Role { get; set; }

        [IgnoreMap]
        public AddressViewModel Address { get; set; }

        #endregion Property

        #region Map

        public static EditViewModel Map(ApplicationUser entity)
        {

            return Mapper.Map<ApplicationUser, EditViewModel>(entity);
        }

        public AddressViewModel Map(Core.Entities.Address entity)
        {
            return Mapper.Map<Core.Entities.Address, AddressViewModel>(entity, this.Address);
        }

        public Core.Entities.Address MapAddress(Core.Entities.Address entity)
        {
            return Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address, entity);
        }

        #endregion Map
    }
}