using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.SensorGroup
{
    public class ListViewModel
    {
        #region Property

        public Guid Id { get; set; }

        [Display(Name = "Site")]
        public Guid SiteId { get; set; }

        [Display(Name = "Site")]
        public String SiteName { get; set; }

        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.Group> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.Group entity)
        {            
            var viewModel = Mapper.Map<Core.Entities.Group, ListViewModel>(entity);

            viewModel.SiteId = entity.Site.Id;
            viewModel.SiteName = entity.Site.Name;

            return viewModel;
        }

        #endregion Map
    }
}