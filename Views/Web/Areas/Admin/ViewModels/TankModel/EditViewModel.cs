using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.TankModel
{
    public class EditViewModel
    {
        #region Property

        public Int32 Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "Image")]
        [Required]
        public Byte[] Image { get; set; }

        #endregion Property

        #region Map

        public static EditViewModel Map(Core.Entities.TankModel entity)
        {
            Mapper.CreateMap<Core.Entities.TankModel, EditViewModel>();
            return Mapper.Map<Core.Entities.TankModel, EditViewModel>(entity);
        }

        #endregion Map
    }
}