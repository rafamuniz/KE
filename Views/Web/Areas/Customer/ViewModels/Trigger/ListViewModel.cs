using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger
{
    public class ListViewModel
    {
        #region Property

        public Guid Id { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "Site")]
        public Guid SiteId { get; set; }

        [Display(Name = "Site")]
        public String SiteName { get; set; }

        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.Trigger> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.Trigger entity)
        {
            Mapper.CreateMap<Core.Entities.Trigger, ListViewModel>();
            var viewModel = Mapper.Map<Core.Entities.Trigger, ListViewModel>(entity);
            //viewModel.SiteId = entity.SiteId;
            //viewModel.SiteName = entity.Site.Name;
            return viewModel;
        }

        #endregion Map
    }
}