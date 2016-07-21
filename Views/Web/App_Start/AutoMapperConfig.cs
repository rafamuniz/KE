using AutoMapper;
using KarmicEnergy.Web.Infrastructure.Mappings.Admin;
using KarmicEnergy.Web.Infrastructure.Mappings.Customer;

namespace KarmicEnergy.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            //var types = Assembly.GetExecutingAssembly().GetExportedTypes();
            //LoadStandardMappings(types);
            //LoadCustomMappings(types);

            Mapper.Initialize(config =>
            {
                config.AddProfile<CustomerProfile>();
                config.AddProfile<StickConversionProfile>();
                config.AddProfile<TankModelProfile>();
                config.AddProfile<Infrastructure.Mappings.Admin.LogProfile>();
                config.AddProfile<Infrastructure.Mappings.Admin.UserProfile>();
                config.AddProfile<Infrastructure.Mappings.Admin.SyncProfile>();

                config.AddProfile<ContactProfile>();
                config.AddProfile<DashboardProfile>();
                config.AddProfile<FastTrackerProfile>();
                config.AddProfile<Infrastructure.Mappings.Customer.LogProfile>();
                config.AddProfile<MonitoringProfile>();
                config.AddProfile<PondProfile>();
                config.AddProfile<SensorGroupProfile>();
                config.AddProfile<SensorProfile>();
                config.AddProfile<SiteProfile>();
                config.AddProfile<TankProfile>();
                config.AddProfile<TriggerProfile>();
                config.AddProfile<Infrastructure.Mappings.Customer.UserProfile>();
            });
        }
    }
}