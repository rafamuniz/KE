namespace KarmicEnergy.Core.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KarmicEnergy.Core.Persistence.KEContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KarmicEnergy.Core.Persistence.KEContext context)
        {
            try
            {
                Unit.Load()
                    .ForEach(e => context.Units.AddOrUpdate(x => x.Id, e));

                Severity.Load()
                    .ForEach(e => context.Severities.AddOrUpdate(x => x.Id, e));

                SensorType.Load()
                    .ForEach(e => context.SensorTypes.AddOrUpdate(x => x.Id, e));

                Geometry.Load()
                    .ForEach(e => context.Geometries.AddOrUpdate(x => x.Id, e));

                TankModel.Load()
                    .ForEach(e => context.TankModels.AddOrUpdate(x => x.Id, e));

                Item.Load()
                    .ForEach(e => context.Items.AddOrUpdate(x => x.Id, e));

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.SensorTypes.AddOrUpdate(
            //  p => p.FullName,
            //  new Person { FullName = "Andrew Peters" },
            //  new Person { FullName = "Brice Lambson" },
            //  new Person { FullName = "Rowan Miller" }
            //);
        }
    }
}
