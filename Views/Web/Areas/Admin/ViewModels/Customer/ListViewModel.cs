﻿using AutoMapper;
using KarmicEnergy.Web.Entities;
using KarmicEnergy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.Customer
{
    public class ListViewModel
    {
        #region Property
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String UserName { get; set; }

        public String Email { get; set; }
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

        public static List<ListViewModel> Map(List<Core.Entities.Customer> entities)
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

        public static ListViewModel Map(Core.Entities.Customer entity)
        {
          
            return Mapper.Map<Core.Entities.Customer, ListViewModel>(entity);
        }

        #endregion Map
    }
}