using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger
{
    public class CreateViewModel
    {
        #region Constructor
        public CreateViewModel()
        {
            Contacts = new List<ContactViewModel>();
            Users = new List<UserViewModel>();
        }

        #endregion Constructor

        #region Property

        [Display(Name = "Value")]
        [MaxLength(20)]
        [Required]
        public String Value { get; set; }

        [Display(Name = "Status")]
        [DefaultValue("A")]
        [Required]
        public String Status { get; set; } = "A";

        [Display(Name = "Site")]
        [Required]
        public Guid? SiteId { get; set; }

        [Display(Name = "Tank")]
        public Guid? TankId { get; set; }

        [Display(Name = "Sensor")]
        [Required]
        public Guid? SensorId { get; set; }

        [Display(Name = "Item")]
        [Required]
        public Guid? SensorItemId { get; set; }

        [Display(Name = "Severity")]
        [Required]
        public Int16 SeverityId { get; set; }

        [Display(Name = "Operator")]
        [Required]
        public Int16 OperatorId { get; set; }

        public IList<ContactViewModel> Contacts { get; set; }

        public IList<UserViewModel> Users { get; set; }

        #endregion Property

        #region Map

        public Core.Entities.Tank Map()
        {
            Mapper.CreateMap<CreateViewModel, Core.Entities.Tank>();
            return Mapper.Map<CreateViewModel, Core.Entities.Tank>(this);
        }

        #endregion Map
    }
}