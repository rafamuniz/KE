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
            List<Trigger> triggers = new List<Trigger>();

            var triggerSites = base.Find(x => x.SensorItem.Sensor.SiteId == siteId && x.DeletedDate == null).ToList();

            var triggertanks = base.Find(x => x.SensorItem.Sensor.Tank.SiteId == siteId && x.DeletedDate == null).ToList();

            triggers.AddRange(triggerSites);
            triggers.AddRange(triggertanks);

            return triggers;
        }

        public List<Trigger> GetsByTank(Guid tankId)
        {
            return base.Find(x => x.SensorItem.Sensor.TankId == tankId && x.DeletedDate == null).ToList();
        }
    }
}
