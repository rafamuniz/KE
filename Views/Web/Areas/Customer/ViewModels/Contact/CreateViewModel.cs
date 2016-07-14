using AutoMapper;
using KarmicEnergy.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Contact
{
    public class CreateViewModel
    {
        #region Constructor
        public CreateViewModel()
        {
            Address = new AddressViewModel();
        }

        #endregion Constructor

        #region Property

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [IgnoreMap]
        public AddressViewModel Address { get; set; }

        #endregion Property

        #region Map

        public Core.Entities.Contact Map()
        {
            return Mapper.Map<CreateViewModel, Core.Entities.Contact>(this);
        }

        public Core.Entities.Address MapAddress()
        {           
            return Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address);
        }

        #endregion Map
    }
}