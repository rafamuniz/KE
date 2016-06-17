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
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            IList<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole("SuperAdmin"),
                new IdentityRole("Admin"),
                new IdentityRole("User"),

                new IdentityRole("Customer"),
                new IdentityRole("General Manager"),
                new IdentityRole("Supervisor"),
                new IdentityRole("Operator")
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
            string password = "KarmicEnergy10$";

            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser() { UserName = "admin@karmicenergy.com", Email = "admin@karmicenergy.com", Name = "Admin" },
                new ApplicationUser() { UserName = "karmicenergy@ke.com", Email = "karmicenergy@ke.com", Name = "Karmic Energy" },
                new ApplicationUser() { UserName = "ke@karmicenergy.com", Email = "ke@karmicenergy.com", Name = "Karmic Energy" }
            };

            if (users.Any())
            {
                foreach (var user in users)
                {
                    var u = userManager.FindByName(user.Name);
                    if (u == null)
                    {
                        var adminresult = userManager.Create(user, password);

                        //Add User Admin to Role Admin
                        if (adminresult.Succeeded)
                        {
                            var result = userManager.AddToRole(user.Id, "SuperAdmin");
                        }
                    }
                }
            }

            CreateUsers(context);
            base.Seed(context);
        }

        private void CreateUsers(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            IList<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole("SuperAdmin"),
                new IdentityRole("Admin"),
                new IdentityRole("User"),

                new IdentityRole("Customer"),
                new IdentityRole("General Manager"),
                new IdentityRole("Supervisor"),
                new IdentityRole("Operator"),
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

            var userkarmicenergy = new ApplicationUser() { UserName = "karmicenergy@ke.com", Email = "karmicenergy@ke.com", Name = "Karmic Energy" };
            String passwordkarmicenergy = "KarmicEnergy10$";

            if (userManager.FindByName(userkarmicenergy.UserName) == null)
            {
                var resultkarmicenergy = userManager.Create(userkarmicenergy, passwordkarmicenergy);

                //Add User Admin to Role Admin
                if (resultkarmicenergy.Succeeded)
                {
                    var resultRole = userManager.AddToRole(userkarmicenergy.Id, "SuperAdmin");

                    if (!resultRole.Succeeded)
                    {
                        throw new Exception(resultRole.Errors.Aggregate((i, j) => i + "\n" + j));
                    }
                }
            }

            var userRafaelMuniz = new ApplicationUser() { UserName = "muniz@ke.com", Email = "muniz@ke.com", Name = "Rafael Muniz" };
            String passwordRafaelMuniz = "KEMuniz11$";            

            if (userManager.FindByName(userkarmicenergy.UserName) == null)
            {
                var resultRafaelMuniz = userManager.Create(userRafaelMuniz, passwordRafaelMuniz);

                //Add User Admin to Role Admin
                if (resultRafaelMuniz.Succeeded)
                {
                    var resultRole = userManager.AddToRole(userRafaelMuniz.Id, "SuperAdmin");
                    
                    if (!resultRole.Succeeded)
                    {
                        throw new Exception(resultRole.Errors.Aggregate((i, j) => i + "\n" + j));
                    }
                }
            }
        }
    }
}
