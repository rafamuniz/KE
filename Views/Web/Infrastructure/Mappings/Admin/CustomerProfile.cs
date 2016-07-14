using KarmicEnergy.Web.Areas.Admin.ViewModels.Customer;
using KarmicEnergy.Web.Entities;
using KarmicEnergy.Web.ViewModels;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Admin
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<ListViewModel, Core.Entities.Customer>();
            this.CreateMap<CreateViewModel, Core.Entities.Customer>().ForMember(x => x.Address, opt => opt.Ignore());
            this.CreateMap<EditViewModel, Core.Entities.Customer>().ForMember(x => x.Address, opt => opt.Ignore());
            this.CreateMap<ChangePasswordViewModel, Core.Entities.Customer>();

            this.CreateMap<ListViewModel, ApplicationUser>();
            this.CreateMap<CreateViewModel, ApplicationUser>();
            this.CreateMap<EditViewModel, ApplicationUser>();
            this.CreateMap<ChangePasswordViewModel, ApplicationUser>();
                        
            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Customer, Areas.Admin.ViewModels.Customer.ListViewModel>();
            this.CreateMap<Core.Entities.Customer, Areas.Admin.ViewModels.Customer.CreateViewModel>();
            this.CreateMap<Core.Entities.Customer, Areas.Admin.ViewModels.Customer.EditViewModel>();
            this.CreateMap<Core.Entities.Customer, Areas.Admin.ViewModels.Customer.ChangePasswordViewModel>();

            this.CreateMap<ApplicationUser, ListViewModel>();
            this.CreateMap<ApplicationUser, CreateViewModel>();
            this.CreateMap<ApplicationUser, EditViewModel>();
            this.CreateMap<ApplicationUser, ChangePasswordViewModel>();

            this.CreateMap<Core.Entities.Address, AddressViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}