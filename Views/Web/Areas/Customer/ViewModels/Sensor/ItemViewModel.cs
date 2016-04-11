using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor
{
    public class ItemViewModel
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

        public static List<ItemViewModel> Map(List<Core.Entities.Item> entities)
        {
            List<ItemViewModel> vms = new List<ItemViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ItemViewModel.Map(c)));
            }

            return vms;
        }

        public static ItemViewModel Map(Core.Entities.Item entity)
        {
            Mapper.CreateMap<Core.Entities.Item, ItemViewModel>();
            return Mapper.Map<Core.Entities.Item, ItemViewModel>(entity);
        }

        #endregion Map
    }
}