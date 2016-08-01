using AutoMapper;
using KarmicEnergy.Web.ViewModels.Shared;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Map
{
    public class IndexViewModel
    {
        #region Constructor

        public IndexViewModel()
        {

        }

        #endregion Constructor

        #region Property

        [IgnoreMap]
        public IEnumerable<SiteViewModel> Sites { get; set; } = new List<SiteViewModel>();

        public Guid? SiteId { get; set; }

        public String IPAddress { get; set; }

        public String Latitude { get; set; }

        public String Longitude { get; set; }

        #endregion Property

        #region Map
             
        public void Map(Core.Entities.Site entity)
        {
            Mapper.Map<Core.Entities.Site, IndexViewModel>(entity, this);
        }
        #endregion Map
    }
}