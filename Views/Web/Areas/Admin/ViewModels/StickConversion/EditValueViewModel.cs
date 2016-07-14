using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.StickConversion
{
    public class EditValueViewModel
    {
        #region Property

        [Required]
        public Guid Id { get; set; }

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

        #region Map

        public static EditViewModel Map(Core.Entities.StickConversion entity)
        {        
            return Mapper.Map<Core.Entities.StickConversion, EditViewModel>(entity);
        }

        #endregion Map
    }
}