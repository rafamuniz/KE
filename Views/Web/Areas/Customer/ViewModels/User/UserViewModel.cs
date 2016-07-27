using AutoMapper;
using KarmicEnergy.Web.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.User
{
    public class UserViewModel
    {
        #region Constructor
        public UserViewModel()
        {            
            Sites = new List<SiteViewModel>();
        }

        #endregion Constructor

        #region Property
        
        [Required]
        [Display(Name = "Username")]
        [EmailAddress]
        public String Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public String Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public String PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "Sites")]
        public List<SiteViewModel> Sites { get; set; }

        #endregion Property

        #region Map

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
                       
        #endregion Map
    }
}