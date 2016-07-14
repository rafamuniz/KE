using KarmicEnergy.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site
{
    public class SiteAddressViewModel : AddressViewModel
    {
        #region Property

        [Display(Name = "Email")]
        [EmailAddress]
        [MaxLength(300)]
        public override String Email { get; set; }

        [Display(Name = "Address Line 1")]
        [MaxLength(256)]
        [Required]
        public override String AddressLine1 { get; set; }

        [Display(Name = "City")]
        [MaxLength(128)]
        [Required]
        public override String City { get; set; }

        [Display(Name = "State")]
        [MaxLength(64)]
        [Required]
        public override String State { get; set; }

        [Display(Name = "Country")]
        [MaxLength(64)]
        [Required]
        public override String Country { get; set; } = "United States";

        [Display(Name = "Zip Code")]
        [MaxLength(16)]
        [Required]
        public override String ZipCode { get; set; }

        #endregion Property
    }
}