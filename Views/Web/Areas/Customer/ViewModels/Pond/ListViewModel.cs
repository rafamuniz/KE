using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Munizoft.Extensions;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Pond
{
    public class ListViewModel
    {
        #region Property

        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "Site")]
        public Guid SiteId { get; set; }

        [Display(Name = "Site")]
        public String SiteName { get; set; }

        #endregion Property

        #region Map

        public static List<ListViewModel> Map(IEnumerable<Core.Entities.Pond> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.Pond entity)
        {         
            var viewModel = Mapper.Map<Core.Entities.Pond, ListViewModel>(entity);
            viewModel.SiteId = entity.SiteId;
            viewModel.SiteName = entity.Site.Name;
            return viewModel;
        }

        #endregion Map
    }
}