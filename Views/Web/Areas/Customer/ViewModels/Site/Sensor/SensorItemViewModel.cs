using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site.Sensor
{
    public class SensorItemViewModel
    {
        #region Property

        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Boolean IsSelected { get; set; }

        public Int16? UnitSelected { get; set; }

        public List<UnitViewModel> Units { get; set; }

        #endregion Property

        #region Map

        public static List<SensorItemViewModel> Map(List<Core.Entities.Item> entities)
        {
            List<SensorItemViewModel> vms = new List<SensorItemViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(SensorItemViewModel.Map(c)));
            }

            return vms;
        }

        public static SensorItemViewModel Map(Core.Entities.Item entity)
        {
            Mapper.CreateMap<Core.Entities.Item, SensorItemViewModel>();
            return Mapper.Map<Core.Entities.Item, SensorItemViewModel>(entity);
        }

        #endregion Map
    }
}