﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor
{
    public class ListViewModel
    {
        #region Constructor
        public ListViewModel()
        {
            Sensors = new List<SensorViewModel>();
        }
        #endregion Constructor

        #region Property

        [Display(Name = "Site")]
        public Guid? SiteId { get; set; }

        [Display(Name = "Tank")]
        public Guid? TankId { get; set; }

        [Display(Name = "Pond")]
        public Guid? PondId { get; set; }

        public List<SensorViewModel> Sensors { get; set; }

        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.Sensor> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.Sensor entity)
        {            
            return Mapper.Map<Core.Entities.Sensor, ListViewModel>(entity);
        }

        #endregion Map
    }
}