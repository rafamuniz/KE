using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class TriggerRepository : KERepositoryBase<Trigger>, ITriggerRepository
    {
        #region Constructor
        public TriggerRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor               

        public List<Trigger> GetsBySite(Guid siteId)
        {
            return base.Find(x => x.SensorItem.Sensor.SiteId == siteId && x.DeletedDate == null).ToList();
        }

        public List<Trigger> GetsByPond(Guid pondId)
        {            
            return base.Find(x => x.SensorItem.Sensor.PondId == pondId && x.DeletedDate == null).ToList();
        }

        public List<Trigger> GetsByTank(Guid tankId)
        {
            return base.Find(x => x.SensorItem.Sensor.TankId == tankId && x.DeletedDate == null).ToList();
        }

        public List<Trigger> GetsBySiteAndQuantity(Guid siteId, Int32 quantity = 5)
        {
            return base.Find(x => x.SensorItem.Sensor.SiteId == siteId && x.DeletedDate == null).Take(quantity).ToList();
        }

        public override IEnumerable<Trigger> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<Trigger> triggers = new List<Trigger>();
            List<Trigger> entities = new List<Trigger>();

            var sites = (from t in Context.Triggers
                         join si in Context.SensorItems on t.SensorItemId equals si.Id
                         join se in Context.Sensors on si.SensorId equals se.Id
                         join s in Context.Sites on se.SiteId equals s.Id
                         where t.LastModifiedDate > lastSyncDate &&
                               s.Id == siteId
                         select t).ToList();

            // Pond
            var ponds = (from t in Context.Triggers
                         join si in Context.SensorItems on t.SensorItemId equals si.Id
                         join se in Context.Sensors on si.SensorId equals se.Id
                         join p in Context.Ponds on se.PondId equals p.Id
                         join s in Context.Sites on se.SiteId equals s.Id
                         where t.LastModifiedDate > lastSyncDate &&
                               s.Id == siteId
                         select t).ToList();

            // Tank
            var tanks = (from t in Context.Triggers
                         join si in Context.SensorItems on t.SensorItemId equals si.Id
                         join se in Context.Sensors on si.SensorId equals se.Id
                         join tk in Context.Tanks on se.TankId equals tk.Id
                         join s in Context.Sites on se.SiteId equals s.Id
                         where t.LastModifiedDate > lastSyncDate &&
                               s.Id == siteId
                         select t).ToList();

            entities.AddRange(sites);
            entities.AddRange(ponds);
            entities.AddRange(tanks);

            foreach (var entity in entities)
            {
                Trigger trigger = new Trigger()
                {
                    Id = entity.Id
                };

                trigger.Update(entity);
                triggers.Add(trigger);
            }

            return triggers;
        }
    }
}
