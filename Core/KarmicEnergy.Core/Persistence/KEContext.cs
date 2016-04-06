﻿using KarmicEnergy.Core.Entities;
using System;
using System.Data.Entity;

namespace KarmicEnergy.Core.Persistence
{
    public class KEContext : DbContext
    {
        #region Constructor

        public KEContext()
        : base("KEConnection")
        {
        }

        public KEContext(String nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
        #endregion Constructor

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TankModel>().Property(x => x.Image).HasColumnType("VARBINARY(MAX)");

            //modelBuilder.HasDefaultSchema("security");
            CreateManyToMany(modelBuilder);
        }

        private void CreateManyToMany(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Sensor>()
            //  .HasMany<Item>(x => x.Items)
            //  .WithMany(s => s.Sensors)
            //  .Map(cs =>
            //  {
            //      cs.MapLeftKey("SensorId");
            //      cs.MapRightKey("ItemId");
            //      cs.ToTable("SensorItems");
            //  });
        }

        #region DbSet
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<CustomerUser> CustomerUsers { get; set; }
        public IDbSet<Site> Sites { get; set; }
        public IDbSet<Tank> Tanks { get; set; }
        public IDbSet<TankModel> TankModels { get; set; }
        public IDbSet<Sensor> Sensors { get; set; }
        public IDbSet<SensorItem> SensorItems { get; set; }
        public IDbSet<Item> Items { get; set; }
        public IDbSet<SensorType> SensorTypes { get; set; }
        public IDbSet<SensorItemEvent> SensorItemEvents { get; set; }
        public IDbSet<Trigger> Triggers { get; set; }
        public IDbSet<Severity> Severities { get; set; }
        public IDbSet<Alarm> Alarms { get; set; }
        public IDbSet<Country> Countries { get; set; }
        #endregion DbSet
    }
}
