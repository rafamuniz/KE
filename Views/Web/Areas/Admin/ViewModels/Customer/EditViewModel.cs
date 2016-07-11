using AutoMapper;
using KarmicEnergy.Web.Entities;
using KarmicEnergy.Web.Models;
using KarmicEnergy.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.Customer
{
    public class EditViewModel
    {
        #region Constructor

        public EditViewModel()
        {
            Address = new AddressViewModel();
        }

        #endregion Property

        #region Property
        public Guid Id { get; set; }
        
        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        public AddressViewModel Address { get; set; }

        #endregion Property

        #region Map

        public static EditViewModel Map(Core.Entities.Customer entity)
        {
            Mapper.CreateMap<Core.Entities.Customer, EditViewModel>();
            return Mapper.Map<Core.Entities.Customer, EditViewModel>(entity);
        }

        public static EditViewModel Map(ApplicationUser entity)
        {
            Mapper.CreateMap<ApplicationUser, EditViewModel>();
            return Mapper.Map<ApplicationUser, EditViewModel>(entity);
        }

        public static AddressViewModel Map(Core.Entities.Address entity)
        {
            Mapper.CreateMap<Core.Entities.Address, AddressViewModel>();
            return Mapper.Map<Core.Entities.Address, AddressViewModel>(entity);
        }

        public Core.Entities.Address MapAddress(Core.Entities.Address entity)
        {
            Mapper.CreateMap<AddressViewModel, Core.Entities.Address>();
            return Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address, entity);
        }

        #endregion Map
    }
}