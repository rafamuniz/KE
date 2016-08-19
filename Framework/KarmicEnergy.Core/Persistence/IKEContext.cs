using KarmicEnergy.Core.Entities;
using System.Data.Entity;

namespace KarmicEnergy.Core.Persistence
{
    public interface IKEContext
    {
        #region DbSet

        IDbSet<DataSync> DataSyncs { get; set; }

        IDbSet<Address> Addresses { get; set; }
        IDbSet<Contact> Contacts { get; set; }
        IDbSet<Customer> Customers { get; set; }
        IDbSet<CustomerSetting> CustomerSettings { get; set; }

        IDbSet<User> Users { get; set; }

        IDbSet<CustomerUser> CustomerUsers { get; set; }
        IDbSet<CustomerUserSetting> CustomerUserSettings { get; set; }
        IDbSet<CustomerUserSite> CustomerUserSites { get; set; }

        IDbSet<Site> Sites { get; set; }
        IDbSet<Pond> Ponds { get; set; }

        IDbSet<Tank> Tanks { get; set; }
        IDbSet<TankModel> TankModels { get; set; }
        IDbSet<Sensor> Sensors { get; set; }
        IDbSet<SensorItem> SensorItems { get; set; }
        IDbSet<Item> Items { get; set; }
        IDbSet<SensorType> SensorTypes { get; set; }
        IDbSet<Group> Groups { get; set; }
        IDbSet<SensorGroup> SensorGroups { get; set; }
        IDbSet<SensorItemEvent> SensorItemEvents { get; set; }
        IDbSet<Trigger> Triggers { get; set; }

        IDbSet<Operator> Operators { get; set; }
        IDbSet<OperatorType> OperatorTypes { get; set; }

        IDbSet<TriggerContact> TriggerContacts { get; set; }

        IDbSet<Severity> Severities { get; set; }
        IDbSet<Alarm> Alarms { get; set; }
        IDbSet<ActionType> ActionTypes { get; set; }
        IDbSet<AlarmHistory> AlarmHistories { get; set; }

        IDbSet<Geometry> Geometries { get; set; }
        IDbSet<Unit> Units { get; set; }
        IDbSet<UnitType> UnitTypes { get; set; }
        IDbSet<Country> Countries { get; set; }
        IDbSet<City> Cities { get; set; }

        IDbSet<StickConversion> StickConversions { get; set; }

        IDbSet<StickConversionValue> StickConversionValues { get; set; }

        IDbSet<Log> Logs { get; set; }
        IDbSet<LogType> LogTypes { get; set; }

        IDbSet<NotificationTemplate> NotificationTemplates { get; set; }
        IDbSet<NotificationType> NotificationTypes { get; set; }
        IDbSet<Notification> Notifications { get; set; }

        #endregion DbSet
    }
}
