using AutoMapper;
using KarmicEnergy.Web.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.User
{
    public class EditViewModel
    {
        #region Constructor
        public EditViewModel()
        {
            Address = new AddressViewModel();
            Sites = new List<SiteViewModel>();
        }

        #endregion Constructor

        #region Property
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Role")]
        public String Role { get; set; }

        [IgnoreMap]
        public AddressViewModel Address { get; set; }

        [IgnoreMap]
        public List<SiteViewModel> Sites { get; set; }

        #endregion Property

        #region Map

        public static EditViewModel Map(Core.Entities.CustomerUser entity)
        {
            return Mapper.Map<Core.Entities.CustomerUser, EditViewModel>(entity);
        }

        public AddressViewModel MapAddress(Core.Entities.Address entity)
        {
            return Mapper.Map<Core.Entities.Address, AddressViewModel>(entity, this.Address);
        }

        public Core.Entities.Address MapAddressVMToEntity(Core.Entities.Address entity)
        {         
            return Mapper.Map<AddressViewModel, Core.Entities.Address>(this.Address, entity);
        }

        public List<SiteViewModel> MapSites(List<Core.Entities.Site> sites, IList<Core.Entities.CustomerUserSite> userSites)
        {
            List<SiteViewModel> viewModels = new List<SiteViewModel>();

            if (sites.Any())
            {
                foreach (var s in sites)
                {
                    SiteViewModel viewModel = new SiteViewModel() { Id = s.Id, Name = s.Name };

                    foreach (var us in userSites)
                    {
                        if (s.Id == us.SiteId)
                        {
                            viewModel.IsSelected = true;
                        }
                    }

                    viewModels.Add(viewModel);
                    this.Sites.Add(viewModel);
                }
            }

            return viewModels;
        }

        public List<Core.Entities.CustomerUserSite> MapSites()
        {
            List<Core.Entities.CustomerUserSite> entities = new List<Core.Entities.CustomerUserSite>();

            if (this.Sites.Any())
            {
                foreach (var item in this.Sites)
                {
                    if (item.IsSelected)
                    {
                        Core.Entities.CustomerUserSite entity = new Core.Entities.CustomerUserSite() { SiteId = item.Id };
                        entities.Add(entity);
                    }
                }
            }           

            return entities;
        }
        #endregion Map
    }
}