﻿using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorItemEventRepository : IKERepositoryBase<SensorItemEvent>
    {
        List<SensorItemEvent> GetsBySite(Guid siteId);
        List<SensorItemEvent> GetsByPond(Guid pondId);
        List<SensorItemEvent> GetsByTank(Guid tankId);
        SensorItemEvent GetLastEventBySensorItem(Guid sensorItemId);
        IEnumerable<SensorItemEvent> GetsBySensorItemAndQuantity(Guid sensorItemId, Int32 quantity = 5);

        SensorItemEvent GetLastEventBySiteAndItem(Guid siteId, ItemEnum item);
        SensorItemEvent GetLastEventByPondAndItem(Guid pondId, ItemEnum item);
        SensorItemEvent GetLastEventByTankAndItem(Guid tankId, ItemEnum item);

        List<SensorItemEvent> GetsByTankIdAndByItem(Guid tankId, ItemEnum item, Int32 quantity);

        List<SensorItemEvent> GetsToCheckAlarm();
    }
}
