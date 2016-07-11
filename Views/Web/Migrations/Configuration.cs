namespace KarmicEnergy.Web.Migrations
{
    using Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "IdentityContext";
        }

        protected override void Seed(ApplicationContext context)
        {
            CreateRoles(context);
            CreateUsers(context);
            base.Seed(context);
        }

        private void CreateRoles(ApplicationContext context)
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
        }

        private void CreateUsers(ApplicationContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            #region Karmic Energy
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
            #endregion Karmic Energy

            #region Rafael Muniz
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
            #endregion Rafael Muniz
        }
    }
}
