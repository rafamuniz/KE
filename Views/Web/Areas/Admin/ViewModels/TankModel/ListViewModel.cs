using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.TankModel
{
    public class ListViewModel
    {
        #region Property
        public Int32 Id { get; set; }

        public String Name { get; set; }

        [IgnoreMap]
        public String Image
        {
            get
            {
                String urlBase = "/images/tankmodels/";
                return String.Format("{0}/{1}/{2}", urlBase, Id, ImageFileName.Replace("{info}", "measure").Replace("-{color}", ""));
            }

            private set { }
        }

        public String ImageFileName { get; set; }

        public String Status { get; set; }

        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.TankModel> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.TankModel entity)
        {

            return Mapper.Map<Core.Entities.TankModel, ListViewModel>(entity);
        }

        #endregion Map
    }
}