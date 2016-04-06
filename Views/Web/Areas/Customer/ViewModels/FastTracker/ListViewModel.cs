﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker
{
    public class ListViewModel
    {
        #region Property

        public Guid SiteId { get; set; }
                
        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.Tank> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<Core.Entities.Tank, ListViewModel>();
            return Mapper.Map<Core.Entities.Tank, ListViewModel>(entity);
        }

        #endregion Map
    }
}