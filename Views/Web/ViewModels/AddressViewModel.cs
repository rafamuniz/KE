using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.ViewModels
{
    public class AddressViewModel
    {
        #region Property

        public virtual Guid? Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [MaxLength(256)]
        public virtual String Email { get; set; }

        [MaxLength(3)]
        [Display(Name = "Code")]
        public virtual String PhoneNumberCountryCode { get; set; } = "1";

        [MaxLength(16)]
        [Display(Name = "Phone Number")]
        public virtual String PhoneNumber { get; set; }

        [Display(Name = "Code")]
        [MaxLength(3)]
        [Required(ErrorMessage = "{0} cannot be null or empty")]
        public virtual String MobileNumberCountryCode { get; set; } = "1";

        [Display(Name = "Mobile Number")]
        [MaxLength(16)]
        [Required(ErrorMessage = "{0} cannot be null or empty")]
        public virtual String MobileNumber { get; set; }

        [Display(Name = "Address Line 1")]
        [MaxLength(256)]
        public virtual String AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        [MaxLength(256)]
        public virtual String AddressLine2 { get; set; }

        [Display(Name = "City")]
        [MaxLength(128)]
        public virtual String City { get; set; }

        [Display(Name = "State")]
        [MaxLength(64)]
        public virtual String State { get; set; }

        [Display(Name = "Country")]
        [MaxLength(64)]
        public virtual String Country { get; set; } = "United States";

        [Display(Name = "Zip Code")]
        [MaxLength(16)]
        public virtual String ZipCode { get; set; }

        #endregion Property
    }
}