using AutoMapper;
using KarmicEnergy.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.User
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

        public static EditViewModel Map(Core.Entities.CustomerUser entity)
        {
            //.ForMember(x => x.Address, opt => opt.Ignore());
            Mapper.CreateMap<Core.Entities.CustomerUser, EditViewModel>();
            return Mapper.Map<Core.Entities.CustomerUser, EditViewModel>(entity);
        }

        public static EditViewModel Map(Core.Entities.Address entity)
        {
            Mapper.CreateMap<Core.Entities.Address, EditViewModel>();
            return Mapper.Map<Core.Entities.Address, EditViewModel>(entity);
        }

        public AddressViewModel MapAddress(Core.Entities.Address entity)
        {
            Mapper.CreateMap<Core.Entities.Address, AddressViewModel>();
            return Mapper.Map<Core.Entities.Address, AddressViewModel>(entity, this.Address);
        }

        public Core.Entities.Address MapAddressVMToEntity(Core.Entities.Address entity)
        {
            Mapper.CreateMap<AddressViewModel, Core.Entities.Address>();
            return Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address, entity);
        }

        #endregion Map
    }
}