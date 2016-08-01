using AutoMapper;
using System;
using Munizoft.Extensions;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.ViewModels.Shared
{
    public class SiteViewModel
    {
        #region Property

        public virtual Guid Id { get; set; }

        [Display(Name = "Site")]
        public virtual String Name { get; set; }

        #endregion Property

        #region Map

        public static IEnumerable<SiteViewModel> Map(IEnumerable<Core.Entities.Site> entities)
        {
            List<SiteViewModel> vms = new List<SiteViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(SiteViewModel.Map(c)));
            }

            return vms;
        }

        public static SiteViewModel Map(Core.Entities.Site entity)
        {
            return Mapper.Map<Core.Entities.Site, SiteViewModel>(entity);
        }

        #endregion Map
    }
}