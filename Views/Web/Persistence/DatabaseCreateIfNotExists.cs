using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Web.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace KarmicEnergy.Web.Persistence
{
    public class DatabaseCreateIfNotExists : CreateDatabaseIfNotExists<ApplicationContext>
    {
        public DatabaseCreateIfNotExists()
        {

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
            KEUnitOfWork keuw = KEUnitOfWork.Create();
            CreateUser(context, keuw, "Karmic Energy", "karmicenergy@ke.com", "KarmicEnergy10$", "SuperAdmin");
            CreateUser(context, keuw, "KE", "ke@karmicenergy.com", "KarmicEnergy10$", "SuperAdmin");
            CreateUser(context, keuw, "Rafael Muniz", "muniz@karmicenergy.com", "KEMuniz11$", "SuperAdmin");
        }

        private void CreateUser(ApplicationContext context, KEUnitOfWork keuw, String name, String email, String password, String role)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser() { UserName = email, Email = email, Name = name };

            if (userManager.FindByName(user.UserName) == null)
            {
                var result = userManager.Create(user, password);

                // Add User Admin to Role Admin
                if (result.Succeeded)
                {
                    var resultRole = userManager.AddToRole(user.Id, role);
                    if (resultRole.Succeeded)
                    {
                        User keUser = new User(Guid.Parse(user.Id));
                        keUser.Address = new Address();
                        keuw.UserRepository.Add(keUser);
                        keuw.Complete();
                    }
                    else
                        throw new Exception(resultRole.Errors.Aggregate((i, j) => i + "\n" + j));

                }
            }
        }
    }
}