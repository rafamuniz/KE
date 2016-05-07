using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.StickConversion
{
    public class CreateViewModel
    {
        #region Property

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "From Unit")]
        [Required]
        public Int16 FromUnitId { get; set; }

        [Display(Name = "To Unit")]
        [Required]
        public Int16 ToUnitId { get; set; }

        #endregion Property
    }
}