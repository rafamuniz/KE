using Autofac;
using Autofac.Integration.Mvc;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services;
using KarmicEnergy.Web.Persistence;
using System.Reflection;
using System.Web.Mvc;

namespace KarmicEnergy.Web.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var assembly = typeof(KarmicEnergyApp).Assembly;
            // Register dependencies in controllers
            builder.RegisterControllers(assembly);

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            // Register Contexts
            builder.RegisterType(typeof(ApplicationContext)).As(typeof(IApplicationContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(KEContext)).As(typeof(IKEContext)).InstancePerLifetimeScope();

            // Register UnitOfWork
            builder.RegisterType(typeof(KEUnitOfWork)).As(typeof(IKEUnitOfWork)).InstancePerLifetimeScope();

            // Register Repositories
            builder.RegisterAssemblyTypes(Assembly.Load("KarmicEnergy.Core"))
                                .Where(t => t.Name.EndsWith("Repository"))
                                .AsImplementedInterfaces()
                                .InstancePerLifetimeScope();

            // Register services
            builder.RegisterAssemblyTypes(Assembly.Load("KarmicEnergy.Core"))
                               .Where(t => t.Name.EndsWith("Service"))
                               .AsImplementedInterfaces()
                               .InstancePerLifetimeScope();

            // Register our Data dependencies
            //builder.RegisterModule(new DataModule("MVCWithAutofacDB"));

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}