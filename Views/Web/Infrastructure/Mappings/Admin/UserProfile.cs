using KarmicEnergy.Web.Areas.Admin.ViewModels.User;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Admin
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<ListViewModel, Core.Entities.User>();
            this.CreateMap<CreateViewModel, Core.Entities.User>();//.ForMember(x => x.Address, opt => opt.Ignore());
            this.CreateMap<EditViewModel, Core.Entities.User>();//.ForMember(x => x.Address, opt => opt.Ignore());
            this.CreateMap<ChangePasswordViewModel, Core.Entities.User>();

            this.CreateMap<EditViewModel, Web.Entities.ApplicationUser>();

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.User, ListViewModel>();
            this.CreateMap<Core.Entities.User, CreateViewModel>();
            this.CreateMap<Core.Entities.User, EditViewModel>();
            this.CreateMap<Core.Entities.User, ChangePasswordViewModel>();

            this.CreateMap<Web.Entities.ApplicationUser, ListViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}