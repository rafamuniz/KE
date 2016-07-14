using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger
{
    public class CustomerUserViewModel
    {
        #region Property

        public Guid Id { get; set; }

        public String Name { get; set; }

        public Boolean IsSelected { get; set; }

        #endregion Property

        #region Map

        public static List<CustomerUserViewModel> Map(List<Core.Entities.CustomerUser> entities)
        {
            List<CustomerUserViewModel> vms = new List<CustomerUserViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(CustomerUserViewModel.Map(c)));
            }

            return vms;
        }

        public static CustomerUserViewModel Map(Core.Entities.CustomerUser entity)
        {            
            return Mapper.Map<Core.Entities.CustomerUser, CustomerUserViewModel>(entity);
        }

        #endregion Map
    }
}