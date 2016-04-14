using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorItemEventRepository : IRepository<SensorItemEvent, KEContext>
    {
        SensorItemEvent GetLastEventByTankIdAndItem(Guid tankId, ItemEnum item);
        List<SensorItemEvent> GetsByTankIdAndByItem(Guid tankId, ItemEnum item, Int32 quantity);        
    }
}
