using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Site.Sensor
{
    public class UnitViewModel
    {
        #region Property

        //Integer value of a checkbox
        public Int32 Id { get; set; }

        //String name of a checkbox
        public String Name { get; set; }

        //Boolean value to select a checkbox
        //on the list
        public Boolean IsSelected { get; set; }

        #endregion Property

        #region Map

        public static List<UnitViewModel> Map(List<Core.Entities.Unit> entities)
        {
            List<UnitViewModel> vms = new List<UnitViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(UnitViewModel.Map(c)));
            }

            return vms;
        }

        public static UnitViewModel Map(Core.Entities.Unit entity)
        {
            Mapper.CreateMap<Core.Entities.Unit, UnitViewModel>();
            return Mapper.Map<Core.Entities.Unit, UnitViewModel>(entity);
        }

        #endregion Map
    }
}