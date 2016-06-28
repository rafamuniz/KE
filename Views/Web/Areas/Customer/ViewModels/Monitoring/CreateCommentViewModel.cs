using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Monitoring
{
    public class CreateCommentViewModel
    {
        #region Constructor
        public CreateCommentViewModel()
        {

        }
        #endregion Constructor

        #region Property

        [Required]
        public Guid AlarmId { get; set; }

        [Required]
        [Display(Name = "Comment")]
        public String Comment { get; set; }

        public AlarmDetailViewModel Detail { get; set; }

        #endregion Property
    }
}