using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class TriggerContactRepository : KERepositoryBase<TriggerContact>, ITriggerContactRepository
    {
        #region Constructor
        public TriggerContactRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor               

        public List<TriggerContact> GetsByTrigger(Guid triggerId)
        {
            return base.Find(x => x.TriggerId == triggerId && x.Status == "A" && x.DeletedDate == null).ToList();
        }

        public override IEnumerable<TriggerContact> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<TriggerContact> triggerContacts = new List<TriggerContact>();
            List<TriggerContact> entities = new List<TriggerContact>();

            var sites = (from tc in Context.TriggerContacts
                         join t in Context.Triggers on tc.TriggerId equals t.Id
                         join si in Context.SensorItems on t.SensorItemId equals si.Id
                         join se in Context.Sensors on si.SensorId equals se.Id
                         where tc.LastModifiedDate > lastSyncDate &&
                               se.SiteId == siteId
                         select tc).ToList();

            // Pond
            var ponds = (from tc in Context.TriggerContacts
                         join t in Context.Triggers on tc.TriggerId equals t.Id
                         join si in Context.SensorItems on t.SensorItemId equals si.Id
                         join se in Context.Sensors on si.SensorId equals se.Id
                         join p in Context.Ponds on se.PondId equals p.Id
                         where tc.LastModifiedDate > lastSyncDate &&
                               p.SiteId == siteId
                         select tc).ToList();

            // Tank
            var tanks = (from tc in Context.TriggerContacts
                         join t in Context.Triggers on tc.TriggerId equals t.Id
                         join si in Context.SensorItems on t.SensorItemId equals si.Id
                         join se in Context.Sensors on si.SensorId equals se.Id
                         join tk in Context.Tanks on se.TankId equals tk.Id
                         where tc.LastModifiedDate > lastSyncDate &&
                               tk.SiteId == siteId
                         select tc).ToList();

            entities.AddRange(sites);
            entities.AddRange(ponds);
            entities.AddRange(tanks);

            foreach (var entity in entities)
            {
                TriggerContact triggerContact = new TriggerContact()
                {
                    Id = entity.Id
                };

                triggerContact.Update(entity);
                triggerContacts.Add(triggerContact);
            }

            return triggerContacts;
        }
    }
}
