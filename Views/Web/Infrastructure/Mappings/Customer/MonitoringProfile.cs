using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Monitoring;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class MonitoringProfile : AutoMapper.Profile
    {
        public MonitoringProfile()
        {
            #region ViewModel To Entity


            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.SensorItemEvent, EventInfoViewModel>();
            this.CreateMap<Core.Entities.SensorItemEvent, EventDetailViewModel>();

            this.CreateMap<Core.Entities.AlarmHistory, CommentViewModel>();
            this.CreateMap<Core.Entities.Alarm, AlarmDetailViewModel>();
            this.CreateMap<Core.Entities.Alarm, AlarmInfoViewModel>();
            this.CreateMap<Core.Entities.Alarm, ListAlarmViewModel>();

            this.CreateMap<Core.Entities.Alarm, AlarmViewModel>();
            this.CreateMap<Core.Entities.Alarm, ViewModels.AlarmViewModel>();

            this.CreateMap<Core.Entities.Alarm, AlarmHistoryViewModel>();
            this.CreateMap<Core.Entities.Alarm, Areas.Customer.ViewModels.Dashboard.AlarmViewModel>();
            this.CreateMap<Core.Entities.Alarm, Areas.Customer.ViewModels.FastTracker.AlarmViewModel>();
            this.CreateMap<Core.Entities.Alarm, Areas.Customer.ViewModels.Monitoring.AlarmViewModel>();
            this.CreateMap<Core.Entities.SensorItemEvent, ListViewModel>();
                                   
            #endregion Entity To ViewModel 
        }
    }
}