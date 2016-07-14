using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class TriggerProfile : AutoMapper.Profile
    {
        public TriggerProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<TriggerViewModel, Core.Entities.Trigger>();
            this.CreateMap<ListViewModel, Core.Entities.Trigger>();
            this.CreateMap<CreateViewModel, Core.Entities.Trigger>();
            this.CreateMap<EditViewModel, Core.Entities.Trigger>();

            this.CreateMap<UserViewModel, Core.Entities.CustomerUser>();
            this.CreateMap<ContactViewModel, Core.Entities.Contact>();

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Trigger, TriggerViewModel>();
            this.CreateMap<Core.Entities.Trigger, ListViewModel>();
            this.CreateMap<Core.Entities.Trigger, CreateViewModel>();
            this.CreateMap<Core.Entities.Trigger, EditViewModel>();

            this.CreateMap<Core.Entities.CustomerUser, UserViewModel>();
            this.CreateMap<Core.Entities.Contact, ContactViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}