using AutoMapper;
using KarmicEnergy.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarmicEnergy.Web.ViewModels.User
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

        public static List<ListViewModel> Map(List<CustomerUser> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(CustomerUser entity)
        {
            Mapper.CreateMap<CustomerUser, ListViewModel>();
            return Mapper.Map<CustomerUser, ListViewModel>(entity);
        }

        #endregion Map
    }
}