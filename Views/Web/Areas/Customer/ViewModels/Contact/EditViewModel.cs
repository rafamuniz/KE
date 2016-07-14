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

        public void MapEntityToVM(Core.Entities.Contact entity)
        {
            Mapper.Map<Core.Entities.Contact, EditViewModel>(entity, this);
        }
        
        public AddressViewModel MapEntityToVM(Core.Entities.Address entity)
        {            
            return Mapper.Map<Core.Entities.Address, AddressViewModel>(entity, this.Address);
        }

        public void MapVMToEntity(Core.Entities.Contact entity)
        {
            Mapper.Map<EditViewModel, Core.Entities.Contact>(this, entity);
        }

        public void MapVMToEntity(Core.Entities.Address entity)
        {         
            Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address, entity);
        }

        #endregion Map
    }
}