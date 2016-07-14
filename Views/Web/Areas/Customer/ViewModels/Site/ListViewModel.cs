using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site
{
    public class ListViewModel
    {
        #region Property
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String IPAddress { get; set; }

        public String Status { get; set; }
        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.Site> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.Site entity)
        {            
            return Mapper.Map<Core.Entities.Site, ListViewModel>(entity);
        }

        #endregion Map
    }
}