using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Map;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Site;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class MapProfile : AutoMapper.Profile
    {
        public MapProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<IndexViewModel, Core.Entities.Site>();

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Site, IndexViewModel>()
                .ForMember(x => x.Sites, opt => opt.Ignore());

            #endregion Entity To ViewModel 
        }
    }
}