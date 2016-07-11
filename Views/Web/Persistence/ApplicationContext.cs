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
using KarmicEnergy.Web.Entities;

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