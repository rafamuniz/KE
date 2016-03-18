using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class DashboardViewModel
    {
        #region Property
        
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Description")]
        [MaxLength]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "SiteId")]
        [Required]
        public Guid SiteId { get; set; }

        [Display(Name = "TankModelId")]
        [Required]
        public Int32 TankModelId { get; set; }

        #endregion Property

        #region Map

        public static List<DashboardViewModel> Map(List<Core.Entities.Tank> entities)
        {
            List<DashboardViewModel> vms = new List<DashboardViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(DashboardViewModel.Map(c)));
            }

            return vms;
        }

        public static DashboardViewModel Map(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<Core.Entities.Tank, DashboardViewModel>();
            return Mapper.Map<Core.Entities.Tank, DashboardViewModel>(entity);
        }

        #endregion Map
    }
}