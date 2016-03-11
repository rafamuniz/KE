using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.User
{
    public class ListViewModel
    {
        #region Property
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public String Role { get; set; }
        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.CustomerUser> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.CustomerUser entity)
        {
            Mapper.CreateMap<Core.Entities.CustomerUser, ListViewModel>();
            return Mapper.Map<Core.Entities.CustomerUser, ListViewModel>(entity);
        }

        #endregion Map
    }
}