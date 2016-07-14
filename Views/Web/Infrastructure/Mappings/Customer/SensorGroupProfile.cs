using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.SensorGroup;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class SensorGroupProfile : AutoMapper.Profile
    {
        public SensorGroupProfile()
        {
            #region ViewModel To Entity

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.SensorGroup, SensorGroupViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}