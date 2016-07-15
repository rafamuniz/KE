using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Site;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class SiteProfile : AutoMapper.Profile
    {
        public SiteProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<ListViewModel, Core.Entities.Site>();
            this.CreateMap<CreateViewModel, Core.Entities.Site>();
            this.CreateMap<EditViewModel, Core.Entities.Site>();

            this.CreateMap<TankViewModel, Core.Entities.Site>();
            this.CreateMap<SiteAddressViewModel, Core.Entities.Site>();
            this.CreateMap<SiteAddressViewModel, Core.Entities.Address>();

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Site, ListViewModel>();
            this.CreateMap<Core.Entities.Site, CreateViewModel>();
            this.CreateMap<Core.Entities.Site, EditViewModel>();

            this.CreateMap<Core.Entities.Site, TankViewModel>();
            this.CreateMap<Core.Entities.Site, SiteAddressViewModel>();
            this.CreateMap<Core.Entities.Address, SiteAddressViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}