using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Pond;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class PondProfile : AutoMapper.Profile
    {
        public PondProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<ListViewModel, Core.Entities.Pond>();
            this.CreateMap<CreateViewModel, Core.Entities.Pond>();
            this.CreateMap<EditViewModel, Core.Entities.Pond>();

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Pond, ListViewModel>();
            this.CreateMap<Core.Entities.Pond, CreateViewModel>();
            this.CreateMap<Core.Entities.Pond, EditViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}