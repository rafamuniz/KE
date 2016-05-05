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

        public SensorItemEvent GetLastEventByTankIdAndItem(Guid tankId, ItemEnum item)
        {
            var lastEvent = Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.Tank.Id == tankId && x.SensorItem.ItemId == (int)item).OrderByDescending(d => d.EventDate);
            if (!lastEvent.Any())
                return null;

            return lastEvent.AsEnumerable().FirstOrDefault();
        }

        public List<SensorItemEvent> GetsByTankIdAndByItem(Guid tankId, ItemEnum item, Int32 quantity)
        {
            var events = Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.Tank.Id == tankId && x.SensorItem.ItemId == (Int32)item && x.Value != null).OrderBy(d => d.EventDate).Take(quantity);
            return events.ToList();
        }

        //public List<SensorItemEvent> GetsByTankIdAndByHour(Guid tankId, ItemEnum item, Int32 beforDays)
        //{
        //    var events = Context.SensorItemEvents.Where(u => u.EventDate != null && u.EventDate <= DateTime.UtcNow.AddDays(-beforDays)).GroupBy(u => u.EventDate.Hour).Select(g => new { Hour = g.Key, Totals = g.Count() }).ToList();
            
        //    return events.ToList();
        //}

        //public List<SensorItemEvent> GetsByTankIdAndByDay(Guid tankId, ItemEnum item, Int32 beforDays)
        //{
        //    var events = Context.SensorItemEvents.Where(u => u.EventDate != null && u.EventDate <= DateTime.UtcNow.AddDays(-beforDays)).GroupBy(u => u.EventDate.Day).Select(g => new { Hour = g.Key, Totals = g.Count() });
        //    return events.ToList();
        //}
    }
}
