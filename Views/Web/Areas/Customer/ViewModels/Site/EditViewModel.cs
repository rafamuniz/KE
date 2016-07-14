using AutoMapper;
using KarmicEnergy.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site
{
    public class EditViewModel
    {
        #region Constructor

        public EditViewModel()
        {
            Address = new SiteAddressViewModel();
        }

        #endregion Constructor

        #region Property

        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Reference")]
        [Required]
        [MaxLength(8)]
        public String Reference { get; set; }

        [Display(Name = "IP Address")]
        [Required]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")]
        public String IPAddress { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "Latitude")]
        public String Latitude { get; set; }

        [Display(Name = "Longitude")]
        public String Longitude { get; set; }

        [IgnoreMap]
        public SiteAddressViewModel Address { get; set; }

        #endregion Property

        #region Map

        public void MapEntityToVM(Core.Entities.Site entity)
        {
            Mapper.Map<Core.Entities.Site, EditViewModel>(entity, this);
        }

        public void MapEntityToVM(Core.Entities.Address entity)
        {
            Mapper.Map<Core.Entities.Address, SiteAddressViewModel>(entity, this.Address);
        }

        public void MapVMToEntity(Core.Entities.Site entity)
        {
            Mapper.Map<EditViewModel, Core.Entities.Site>(this, entity);
        }

        public void MapVMToEntity(Core.Entities.Address entity)
        {
            Mapper.Map<SiteAddressViewModel, Core.Entities.Address>(this.Address, entity);
        }

        #endregion Map
    }
}