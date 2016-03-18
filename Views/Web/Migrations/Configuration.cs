namespace KarmicEnergy.Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KarmicEnergy.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "KarmicEnergy.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(KarmicEnergy.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            CreateUsers(context);
            base.Seed(context);
        }

        private void CreateUsers(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            IList<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole("Admin"),
                new IdentityRole("Operator"),
                new IdentityRole("Customer"),
                new IdentityRole("CustomerAdmin"),
                new IdentityRole("CustomerOperator")
            };

            foreach (var r in roles)
            {
                //Create Role Admin if it does not exist
                if (!roleManager.RoleExists(r.Name))
                {
                    var roleresult = roleManager.Create(r);
                }
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser() { UserName = "karmicenergy@ke.com", Email = "karmicenergy@ke.com", Name = "Karmic Energy" };
            String password = "KarmicEnergy10$";

            var adminresult = userManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
