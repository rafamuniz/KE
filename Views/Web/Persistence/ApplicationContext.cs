using KarmicEnergy.Web.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace KarmicEnergy.Web.Persistence
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext()
            : base("KEConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DatabaseCreateIfNotExists());
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().Property(x => x.Photo).HasColumnType("VARBINARY(MAX)");

            base.OnModelCreating(modelBuilder);
        }
    }

}