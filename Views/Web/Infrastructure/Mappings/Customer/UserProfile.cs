using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.User;
using KarmicEnergy.Web.Entities;
using KarmicEnergy.Web.ViewModels;
using Munizoft.Extensions;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<ListViewModel, Core.Entities.CustomerUser>();

            this.CreateMap<CreateViewModel, Core.Entities.CustomerUser>();
            this.CreateMap<CreateViewModel, Core.Entities.Contact>();
            this.CreateMap<UserViewModel, Core.Entities.CustomerUser>();

            this.CreateMap<EditViewModel, Core.Entities.CustomerUser>();
            this.CreateMap<EditViewModel, Core.Entities.Contact>().ForMember(x => x.Address, opt => opt.Ignore());

            this.CreateMap<AddressViewModel, Core.Entities.Address>();
            this.CreateMap<CreateViewModel, Core.Entities.User>().ForMember(x => x.Address, opt => opt.Ignore());

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.CustomerUser, ListViewModel>();

            this.CreateMap<Core.Entities.CustomerUser, CreateViewModel>();
            this.CreateMap<Core.Entities.Contact, ListViewModel>();

            this.CreateMap<Core.Entities.CustomerUser, EditViewModel>()
                .ForMember(x => x.Address, opt => opt.Ignore())
                .ForMember(x => x.Sites, opt => opt.Ignore());

            this.CreateMap<ApplicationUser, EditViewModel>();

            this.CreateMap<Core.Entities.Contact, EditViewModel>();

            this.CreateMap<Core.Entities.Site, SiteViewModel>();
            this.CreateMap<Core.Entities.CustomerUserSite, SiteViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}