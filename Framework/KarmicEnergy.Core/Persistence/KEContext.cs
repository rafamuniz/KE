using KarmicEnergy.Core.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KarmicEnergy.Core.Persistence
{
    public class KEContext : DbContext, IKEContext
    {
        #region Constructor

        public KEContext()
        : base("KEConnection")
        {
            Database.SetInitializer(new DatabaseCreateIfNotExists());
        }

        public KEContext(Boolean enablelazyLoading = true)
            : base("KEConnection")
        {
            this.Configuration.LazyLoadingEnabled = enablelazyLoading;
            Database.SetInitializer(new DatabaseCreateIfNotExists());
        }

        public KEContext(String nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public KEContext(String nameOrConnectionString, Boolean enablelazyLoading = true)
         : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = enablelazyLoading;
        }
        #endregion Constructor

        #region Functions
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tank>().Property(x => x.Description).HasColumnType("NVARCHAR(MAX)");
            modelBuilder.Entity<Pond>().Property(x => x.Description).HasColumnType("NVARCHAR(MAX)");

            modelBuilder.Entity<TankModel>().Property(x => x.Image).HasColumnType("VARBINARY(MAX)");
            modelBuilder.Entity<CustomerSetting>().Property(x => x.Value).HasColumnType("NVARCHAR(MAX)");
            modelBuilder.Entity<CustomerUserSetting>().Property(x => x.Value).HasColumnType("NVARCHAR(MAX)");
            modelBuilder.Entity<Notification>().Property(p => p.Message).HasColumnType("NVARCHAR(MAX)");
            modelBuilder.Entity<NotificationTemplate>().Property(p => p.Message).HasColumnType("NVARCHAR(MAX)");

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
        #endregion Functions

        #region DbSet

        public IDbSet<DataSync> DataSyncs { get; set; }

        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<Contact> Contacts { get; set; }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<CustomerSetting> CustomerSettings { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<CustomerUser> CustomerUsers { get; set; }
        public IDbSet<CustomerUserSetting> CustomerUserSettings { get; set; }
        public IDbSet<CustomerUserSite> CustomerUserSites { get; set; }

        public IDbSet<Site> Sites { get; set; }
        public IDbSet<Pond> Ponds { get; set; }

        public IDbSet<Tank> Tanks { get; set; }
        public IDbSet<TankModel> TankModels { get; set; }
        public IDbSet<Sensor> Sensors { get; set; }
        public IDbSet<SensorItem> SensorItems { get; set; }
        public IDbSet<Item> Items { get; set; }
        public IDbSet<SensorType> SensorTypes { get; set; }
        public IDbSet<Group> Groups { get; set; }
        public IDbSet<SensorGroup> SensorGroups { get; set; }
        public IDbSet<SensorItemEvent> SensorItemEvents { get; set; }
        public IDbSet<Trigger> Triggers { get; set; }

        public IDbSet<Operator> Operators { get; set; }
        public IDbSet<OperatorType> OperatorTypes { get; set; }

        public IDbSet<TriggerContact> TriggerContacts { get; set; }

        public IDbSet<Severity> Severities { get; set; }
        public IDbSet<Alarm> Alarms { get; set; }
        public IDbSet<ActionType> ActionTypes { get; set; }
        public IDbSet<AlarmHistory> AlarmHistories { get; set; }

        public IDbSet<Geometry> Geometries { get; set; }
        public IDbSet<Unit> Units { get; set; }
        public IDbSet<UnitType> UnitTypes { get; set; }
        public IDbSet<Country> Countries { get; set; }
        public IDbSet<City> Cities { get; set; }

        public IDbSet<StickConversion> StickConversions { get; set; }

        public IDbSet<StickConversionValue> StickConversionValues { get; set; }

        public IDbSet<Log> Logs { get; set; }
        public IDbSet<LogType> LogTypes { get; set; }

        public IDbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public IDbSet<NotificationType> NotificationTypes { get; set; }
        public IDbSet<Notification> Notifications { get; set; }

        #endregion DbSet
    }
}
