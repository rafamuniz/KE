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
                LogType.Load()
                    .ForEach(e => context.LogTypes.AddOrUpdate(x => x.Id, e));

                OperatorType.Load()
                    .ForEach(e => context.OperatorTypes.AddOrUpdate(x => x.Id, e));

                Operator.Load()
                    .ForEach(e => context.Operators.AddOrUpdate(x => x.Id, e));

                UnitType.Load()
                    .ForEach(e => context.UnitTypes.AddOrUpdate(x => x.Id, e));

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

                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
