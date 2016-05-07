using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.StickConversion
{
    public class ListViewModel
    {
        #region Property
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String FromUnit { get; set; }

        public String ToUnit { get; set; }

        public String Status { get; set; }

        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.StickConversion> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.StickConversion entity)
        {
            Mapper.CreateMap<Core.Entities.StickConversion, ListViewModel>();
            var viewModel = Mapper.Map<Core.Entities.StickConversion, ListViewModel>(entity);
            viewModel.ToUnit = entity.ToUnit.Name;
            viewModel.FromUnit = entity.FromUnit.Name;
            return viewModel;
        }

        #endregion Map
    }
}