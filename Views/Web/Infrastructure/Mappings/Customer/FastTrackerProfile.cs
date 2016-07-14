using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class FastTrackerProfile : AutoMapper.Profile
    {
        public FastTrackerProfile()
        {
            #region ViewModel To Entity

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Site, ListViewModel>();
            this.CreateMap<Core.Entities.Trigger, TriggerViewModel>();
            this.CreateMap<Core.Entities.Alarm, AlarmViewModel>();
            
            #endregion Entity To ViewModel 
        }
    }
}