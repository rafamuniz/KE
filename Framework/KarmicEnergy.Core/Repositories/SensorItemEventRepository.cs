using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorItemEventRepository : KERepositoryBase<SensorItemEvent>, ISensorItemEventRepository
    {
        #region Constructor
        public SensorItemEventRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor       

        public List<SensorItemEvent> GetsBySite(Guid siteId)
        {
            return Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.SiteId == siteId && x.SensorItem.Sensor.TankId == null).ToList();
        }

        public List<SensorItemEvent> GetsByPond(Guid pondId)
        {
            return Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.PondId == pondId).ToList();
        }

        public List<SensorItemEvent> GetsByTank(Guid tankId)
        {
            return Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.TankId == tankId).ToList();
        }

        public SensorItemEvent GetLastEventByPondAndItem(Guid pondId, ItemEnum item)
        {
            var lastEvent = Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.PondId == pondId && x.SensorItem.ItemId == (int)item).OrderByDescending(d => d.EventDate);
            if (!lastEvent.Any())
                return null;

            return lastEvent.AsEnumerable().FirstOrDefault();
        }

        public SensorItemEvent GetLastEventByTankAndItem(Guid tankId, ItemEnum item)
        {
            var lastEvent = Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.TankId == tankId && x.SensorItem.ItemId == (int)item).OrderByDescending(d => d.EventDate);
            if (!lastEvent.Any())
                return null;

            return lastEvent.AsEnumerable().FirstOrDefault();
        }

        public SensorItemEvent GetLastEventBySensorItem(Guid sensorItemId)
        {
            return Context.SensorItemEvents.Where(x => x.SensorItem.Id == sensorItemId).OrderByDescending(d => d.EventDate).FirstOrDefault();
        }

        public IEnumerable<SensorItemEvent> GetsBySensorItemAndQuantity(Guid sensorItemId, Int32 quantity = 5)
        {
            return Context.SensorItemEvents.Where(x => x.SensorItem.Id == sensorItemId).OrderByDescending(d => d.EventDate).Take(quantity);
        }

        public SensorItemEvent GetLastEventBySiteAndItem(Guid siteId, ItemEnum item)
        {
            var lastEvent = Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.SiteId == siteId && x.SensorItem.Sensor.TankId == null && x.SensorItem.ItemId == (int)item).OrderByDescending(d => d.EventDate);
            if (!lastEvent.Any())
                return null;

            return lastEvent.AsEnumerable().FirstOrDefault();
        }

        public List<SensorItemEvent> GetsByTankIdAndByItem(Guid tankId, ItemEnum item, Int32 quantity)
        {
            var events = Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.Tank.Id == tankId && x.SensorItem.ItemId == (Int32)item && x.Value != null).OrderBy(d => d.EventDate).Take(quantity);
            return events.ToList();
        }

        /// <summary>
        /// Gets Events to check alarm
        /// </summary>
        /// <returns></returns>
        public List<SensorItemEvent> GetsToCheckAlarm()
        {
            return Context.SensorItemEvents.Where(x => x.CheckedAlarmDate == null).ToList();
        }

        public override IEnumerable<SensorItemEvent> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<SensorItemEvent> entities = new List<SensorItemEvent>();

            var sites = base.Find(x => x.SensorItem.Sensor.SiteId == siteId && x.LastModifiedDate > lastSyncDate).ToList();
            var ponds = base.Find(x => x.SensorItem.Sensor.Pond.SiteId == siteId && x.LastModifiedDate > lastSyncDate).ToList();
            var tanks = base.Find(x => x.SensorItem.Sensor.Tank.SiteId == siteId && x.LastModifiedDate > lastSyncDate).ToList();

            entities.AddRange(sites);
            entities.AddRange(ponds);
            entities.AddRange(tanks);

            return entities.AsEnumerable();
        }
    }
}
