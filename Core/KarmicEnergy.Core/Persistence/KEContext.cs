using KarmicEnergy.Core.Entities;
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

            //modelBuilder.Entity<UserEntity>()
            //  .HasMany<RoleEntity>(x => x.Roles)
            //  .WithMany(c => c.Users)
            //  .Map(cs =>
            //  {
            //      cs.MapLeftKey("UserId");
            //      cs.MapRightKey("RoleId");
            //      cs.ToTable("UserRoles");
            //  });
        }

        #region DbSet
        internal IDbSet<Customer> Customers { get; set; }
        internal IDbSet<CustomerUser> CustomerUsers { get; set; }
        internal IDbSet<Site> Sites { get; set; }
        internal IDbSet<Tank> Tanks { get; set; }
        internal IDbSet<TankModel> TankModels { get; set; }
        internal IDbSet<Sensor> Sensors { get; set; }
        internal IDbSet<Country> Countries { get; set; }
        #endregion DbSet
    }
}
