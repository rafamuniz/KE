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

        public AddressViewModel Address { get; set; }
        #endregion Property

        #region Map

        public static EditViewModel Map(Core.Entities.CustomerUser entity)
        {
            Mapper.CreateMap<Core.Entities.CustomerUser, EditViewModel>();
            return Mapper.Map<Core.Entities.CustomerUser, EditViewModel>(entity);
        }

        public static EditViewModel Map(Core.Entities.Address entity)
        {
            Mapper.CreateMap<Core.Entities.Address, EditViewModel>();
            return Mapper.Map<Core.Entities.Address, EditViewModel>(entity);
        }

        public Core.Entities.Address MapAddress()
        {
            Mapper.CreateMap<AddressViewModel, Core.Entities.Address>();
            return Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address);
        }

        #endregion Map
    }
}