using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.User;
using KarmicEnergy.Web.ViewModels;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<ListViewModel, Core.Entities.CustomerUser>();
            this.CreateMap<CreateViewModel, Core.Entities.CustomerUser>();
            this.CreateMap<EditViewModel, Core.Entities.CustomerUser>();

            this.CreateMap<AddressViewModel, Core.Entities.Address>();
            this.CreateMap<CreateViewModel, Core.Entities.User>().ForMember(x => x.Address, opt => opt.Ignore());

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.CustomerUser, ListViewModel>();
            this.CreateMap<Core.Entities.CustomerUser, CreateViewModel>();
            this.CreateMap<Core.Entities.CustomerUser, EditViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}