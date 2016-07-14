using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor
{
    public class SensorViewModel
    {
        #region Constructor
        public SensorViewModel()
        {
        }
        #endregion Constructor

        #region Property

        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Reference")]
        [MaxLength(8)]
        public String Reference { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "Site")]        
        public Guid? SiteId { get; set; }

        [Display(Name = "Tank")]        
        public Guid? TankId { get; set; }

        #endregion Property

        #region Map

        public static List<SensorViewModel> Map(List<Core.Entities.Sensor> entities)
        {
            List<SensorViewModel> vms = new List<SensorViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(SensorViewModel.Map(c)));
            }

            return vms;
        }

        public static SensorViewModel Map(Core.Entities.Sensor entity)
        {            
            return Mapper.Map<Core.Entities.Sensor, SensorViewModel>(entity);
        }

        #endregion Map
    }
}