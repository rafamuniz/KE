using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.TankModel
{
    public class EditViewModel
    {
        #region Property

        [Required]
        public Int32 Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [AutoMapper.IgnoreMap]
        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Image FileName")]
        public String ImageFileName { get; set; }

        #endregion Property

        #region Map

        public static EditViewModel Map(Core.Entities.TankModel entity)
        {
            //EditViewModel viewModel = new EditViewModel()
            //{
            //    Id = entity.Id,
            //    Name = entity.Name,
            //    Status = entity.Status,
            //    ImageFileName = entity.ImageFilename
            //};
            //return viewModel;
       
            return Mapper.Map<Core.Entities.TankModel, EditViewModel>(entity);
        }

        #endregion Map
    }
}