using AutoMapper;
using KarmicEnergy.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Contact
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

        [IgnoreMap]
        public AddressViewModel Address { get; set; }
        #endregion Property

        #region Map

        public void Map(Core.Entities.Contact entity)
        {
            //.ForMember(x => x.Address, opt => opt.Ignore());
            Mapper.CreateMap<Core.Entities.Contact, EditViewModel>();
            Mapper.Map<Core.Entities.Contact, EditViewModel>(entity);
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

        public void MapVMToEntity(Core.Entities.Contact entity)
        {
            Mapper.CreateMap<EditViewModel, Core.Entities.Contact>();
            Mapper.Map<EditViewModel, Core.Entities.Contact>(this, entity);
        }

        public void MapVMToEntity(Core.Entities.Address entity)
        {
            Mapper.CreateMap<AddressViewModel, Core.Entities.Address>();
            Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address, entity);
        }

        #endregion Map
    }
}