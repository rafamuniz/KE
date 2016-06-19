using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KarmicEnergy.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("Photo", TypeName = "VARBINARY")]
        [MaxLength]
        public Byte[] Photo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("KEConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DatabaseCreateIfNotExists());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().Property(x => x.Photo).HasColumnType("VARBINARY(MAX)");

            base.OnModelCreating(modelBuilder);
        }
    }

    public class DatabaseCreateIfNotExists : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        public DatabaseCreateIfNotExists()
        {

        }

        protected override void Seed(ApplicationDbContext context)
        {
            CreateUsers(context);
            base.Seed(context);
        }

        private void CreateUsers(ApplicationDbContext context)
        {
           
        }
    }
}