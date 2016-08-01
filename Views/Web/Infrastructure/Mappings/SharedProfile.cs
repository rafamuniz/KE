using KarmicEnergy.Web.ViewModels.Shared;

namespace KarmicEnergy.Web.Infrastructure.Mappings
{
    public class SharedProfile : AutoMapper.Profile
    {
        public SharedProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<SiteViewModel, Core.Entities.Site>();

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Site, SiteViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}