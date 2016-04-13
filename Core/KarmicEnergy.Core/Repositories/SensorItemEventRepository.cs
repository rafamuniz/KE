using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorItemEventRepository : Repository<SensorItemEvent, KEContext>, ISensorItemEventRepository
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

            return lastEvent.AsEnumerable().Last();
        }

        public List<SensorItemEvent> GetTankWithWaterVolume(Guid tankId, Int32 quantity)
        {
            var events = Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.Tank.Id == tankId && x.SensorItem.ItemId == (int)ItemEnum.WaterVolume && x.Value != null).OrderByDescending(d => d.EventDate).Take(quantity);
            return events.ToList();
        }
    }
}
