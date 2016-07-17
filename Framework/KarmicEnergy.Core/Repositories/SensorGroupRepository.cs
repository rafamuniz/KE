using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorGroupRepository : KERepositoryBase<SensorGroup>, ISensorGroupRepository
    {
        #region Constructor
        public SensorGroupRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor   

        public override IEnumerable<SensorGroup> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<SensorGroup> sensorGroups = new List<SensorGroup>();
            List<SensorGroup> entities = new List<SensorGroup>();

            var sites = (from sg in Context.SensorGroups
                         join se in Context.Sensors on sg.SensorId equals se.Id
                         join s in Context.Sites on se.SiteId equals s.Id
                         where sg.LastModifiedDate > lastSyncDate &&
                               s.Id == siteId
                         select sg).ToList();

            // Pond
            var ponds = (from sg in Context.SensorGroups
                         join se in Context.Sensors on sg.SensorId equals se.Id
                         join p in Context.Ponds on se.PondId equals p.Id
                         join s in Context.Sites on se.SiteId equals s.Id
                         where sg.LastModifiedDate > lastSyncDate &&
                               s.Id == siteId
                         select sg).ToList();

            // Tank
            var tanks = (from sg in Context.SensorGroups
                         join se in Context.Sensors on sg.SensorId equals se.Id
                         join tk in Context.Tanks on se.TankId equals tk.Id
                         join s in Context.Sites on se.SiteId equals s.Id
                         where sg.LastModifiedDate > lastSyncDate &&
                               s.Id == siteId
                         select sg).ToList();

            entities.AddRange(sites);
            entities.AddRange(ponds);
            entities.AddRange(tanks);

            foreach (var entity in entities)
            {
                SensorGroup sensorGroup = new SensorGroup()
                {
                    Id = entity.Id
                };

                sensorGroup.Update(entity);
                sensorGroups.Add(sensorGroup);
            }

            return sensorGroups;
        }
    }
}
