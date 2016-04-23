using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorItemEventRepository : IKERepositoryBase<SensorItemEvent>
    {
        SensorItemEvent GetLastEventByTankIdAndItem(Guid tankId, ItemEnum item);
        List<SensorItemEvent> GetsByTankIdAndByItem(Guid tankId, ItemEnum item, Int32 quantity);        
    }
}
