using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Tank;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class TankProfile : AutoMapper.Profile
    {
        public TankProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<ListViewModel, Core.Entities.Tank>();
            this.CreateMap<CreateViewModel, Core.Entities.Tank>();
            this.CreateMap<EditViewModel, Core.Entities.Tank>();

            this.CreateMap<EditViewModel, Core.Entities.Tank>().ForMember(x => x.TankModel, opt => opt.Ignore());

            #endregion ViewModel To Entity

            #region Entity To ViewModel 
                     
            this.CreateMap<Core.Entities.Tank, EditViewModel>().ForMember(x => x.TankModel, opt => opt.Ignore());

            this.CreateMap<Core.Entities.Tank, TankDashboardViewModel>();
            this.CreateMap<Core.Entities.Tank, TankViewModel>();
            this.CreateMap<Core.Entities.Tank, ListViewModel>();
            this.CreateMap<Core.Entities.Tank, Areas.Customer.ViewModels.Dashboard.TankViewModel>();
            this.CreateMap<Core.Entities.Tank, GaugeViewModel>();
            this.CreateMap<Core.Entities.TankModel, TankModelViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}