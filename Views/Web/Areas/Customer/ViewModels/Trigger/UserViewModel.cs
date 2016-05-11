using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger
{
    public class UserViewModel
    {
        #region Property
                
        public Guid Id { get; set; }
                
        public String Name { get; set; }

        public Boolean IsSelected { get; set; }
                
        #endregion Property

        #region Map

        public static List<UserViewModel> Map(List<Core.Entities.CustomerUser> entities)
        {
            List<UserViewModel> vms = new List<UserViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(UserViewModel.Map(c)));
            }

            return vms;
        }

        public static UserViewModel Map(Core.Entities.CustomerUser entity)
        {
            Mapper.CreateMap<Core.Entities.CustomerUser, UserViewModel>();
            return Mapper.Map<Core.Entities.CustomerUser, UserViewModel>(entity);
        }

        #endregion Map
    }
}