using AutoMapper;
using KarmicEnergy.Web.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.User
{
    public class CreateViewModel
    {
        #region Constructor
        public CreateViewModel()
        {
            Address = new AddressViewModel();
            User = new UserViewModel();
        }

        #endregion Constructor

        #region Property

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Role")]
        public String Role { get; set; }

        public UserViewModel User { get; set; }

        public AddressViewModel Address { get; set; }

        #endregion Property

        #region Map

        public Core.Entities.Address MapAddress()
        {
            return Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address);
        }

        public Core.Entities.Contact Map()
        {
            return Mapper.Map<CreateViewModel, Core.Entities.Contact>(this);
        }

        #endregion Map
    }
}