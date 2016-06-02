using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorItemEventRepository : IKERepositoryBase<SensorItemEvent>
    {
        SensorItemEvent GetLastEventByTankAndItem(Guid tankId, ItemEnum item);
        SensorItemEvent GetLastEventBySiteAndItem(Guid siteId, ItemEnum item);
        List<SensorItemEvent> GetsByTankIdAndByItem(Guid tankId, ItemEnum item, Int32 quantity);        
    }
}
