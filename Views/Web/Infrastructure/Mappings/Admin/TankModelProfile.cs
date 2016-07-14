using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Admin.ViewModels.TankModel;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Admin
{
    public class TankModelProfile : AutoMapper.Profile
    {
        public TankModelProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<Areas.Customer.ViewModels.Tank.TankModelViewModel, Core.Entities.TankModel>();

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.TankModel, ListViewModel>();
            this.CreateMap<Core.Entities.TankModel, CreateViewModel>();
            this.CreateMap<Core.Entities.TankModel, EditViewModel>();
            
            #endregion Entity To ViewModel 
        }
    }
}