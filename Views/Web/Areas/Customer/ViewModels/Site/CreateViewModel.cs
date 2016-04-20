using AutoMapper;
using KarmicEnergy.Web.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site
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

        [Display(Name = "IP")]
        [Required]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")]
        public String IPAddress { get; set; }

        [Display(Name = "Status")]
        [DefaultValue("A")]
        [Required]
        public String Status { get; set; } = "A";

        [Display(Name = "Latitude")]
        public String Latitude { get; set; }

        [Display(Name = "Longitude")]
        public String Longitude { get; set; }

        [NotMapped]
        public AddressViewModel Address { get; set; }

        #endregion Property

        //#region Address

        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //[MaxLength(256)]
        //public String Email { get; set; }

        //[MaxLength(3)]
        //[Display(Name = "Code")]
        //public String PhoneNumberCountryCode { get; set; } = "1";

        //[MaxLength(16)]
        //[Display(Name = "Phone Number")]
        //public String PhoneNumber { get; set; }

        //[Display(Name = "Code")]
        //[MaxLength(3)]
        //[Required(ErrorMessage = "{0} cannot be null or empty")]
        //public String MobileNumberCountryCode { get; set; } = "1";

        //[Display(Name = "Mobile Number")]
        //[MaxLength(16)]
        //[Required(ErrorMessage = "{0} cannot be null or empty")]
        //public String MobileNumber { get; set; }

        //[Display(Name = "Address Line 1")]
        //[MaxLength(256)]
        //public String AddressLine1 { get; set; }

        //[Display(Name = "Address Line 2")]
        //[MaxLength(256)]
        //public String AddressLine2 { get; set; }

        //[Display(Name = "City")]
        //[MaxLength(128)]
        //public String City { get; set; }

        //[Display(Name = "State")]
        //[MaxLength(64)]
        //public String State { get; set; }

        //[Display(Name = "Country")]
        //[MaxLength(64)]
        //public String Country { get; set; } = "United States";

        //[Display(Name = "Zip Code")]
        //[MaxLength(16)]
        //public String ZipCode { get; set; }

        //#endregion Address

        #region Map

        public Core.Entities.Site Map()
        {
            Mapper.CreateMap<CreateViewModel, Core.Entities.Site>().ForMember(x => x.Address, opt => opt.Ignore());
            return Mapper.Map<CreateViewModel, Core.Entities.Site>(this);
        }

        public static CreateViewModel Map(Core.Entities.Address entity)
        {
            Mapper.CreateMap<Core.Entities.Address, CreateViewModel>();
            return Mapper.Map<Core.Entities.Address, CreateViewModel>(entity);
        }

        public Core.Entities.Address MapAddress()
        {
            Mapper.CreateMap<AddressViewModel, Core.Entities.Address>();
            return Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address);
        }

        #endregion Map
    }
}