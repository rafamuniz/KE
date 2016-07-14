using AutoMapper;
using KarmicEnergy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.User
{
    public class SiteViewModel
    {
        #region Property
        public Guid Id { get; set; }

        public String Name { get; set; }

        public Boolean IsSelected { get; set; }

        #endregion Property

        #region Map

        public static List<SiteViewModel> Map(List<Core.Entities.Site> entities)
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