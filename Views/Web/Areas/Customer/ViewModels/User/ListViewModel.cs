using AutoMapper;
using KarmicEnergy.Web.Entities;
using KarmicEnergy.Web.Models;
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

        public String UserName { get; set; }

        public String Email { get; set; }

        //private String role;
        public String Role { get; set; }
        //{
        //    get
        //    {
        //        if (role == "CustomerAdmin")
        //            return "Admin";
        //        else if (role == "CustomerOperator")
        //            return "Operator";
        //        else
        //            return String.Empty;
        //    }
        //    set { role = value; }
        //}

        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<ApplicationUser> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static List<ListViewModel> Map(List<Core.Entities.CustomerUser> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(ApplicationUser entity)
        {            
            return Mapper.Map<ApplicationUser, ListViewModel>(entity);
        }

        public static ListViewModel Map(Core.Entities.CustomerUser entity)
        {            
            return Mapper.Map<Core.Entities.CustomerUser, ListViewModel>(entity);
        }

        #endregion Map
    }
}