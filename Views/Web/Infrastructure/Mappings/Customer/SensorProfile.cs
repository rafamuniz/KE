using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class SensorProfile : AutoMapper.Profile
    {
        public SensorProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<ListViewModel, Core.Entities.Sensor>();
            this.CreateMap<CreateViewModel, Core.Entities.Sensor>();
            this.CreateMap<EditViewModel, Core.Entities.Sensor>();

            this.CreateMap<SensorViewModel, Core.Entities.Sensor>();

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Sensor, ListViewModel>();
            this.CreateMap<Core.Entities.Sensor, CreateViewModel>();
            this.CreateMap<Core.Entities.Sensor, EditViewModel>();

            this.CreateMap<Core.Entities.Sensor, SensorViewModel>();
            this.CreateMap<Core.Entities.Item, ItemViewModel>();
            this.CreateMap<Core.Entities.Unit, UnitViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}