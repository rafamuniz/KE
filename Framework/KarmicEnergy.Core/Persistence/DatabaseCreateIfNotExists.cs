using KarmicEnergy.Core.Entities;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace KarmicEnergy.Core.Persistence
{
    public class DatabaseCreateIfNotExists : CreateDatabaseIfNotExists<KEContext>
    {
        public DatabaseCreateIfNotExists()
        {

        }

        protected override void Seed(KEContext context)
        {
            StickConversion.Load()
                  .ForEach(e => context.StickConversions.AddOrUpdate(x => x.Id, e));

            NotificationType.Load()
               .ForEach(e => context.NotificationTypes.AddOrUpdate(x => x.Id, e));

            NotificationTemplate.Load()
               .ForEach(e => context.NotificationTemplates.AddOrUpdate(x => x.Name, e));

            ActionType.Load()
                .ForEach(e => context.ActionTypes.AddOrUpdate(x => x.Id, e));

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

            base.Seed(context);
        }        
    }
}