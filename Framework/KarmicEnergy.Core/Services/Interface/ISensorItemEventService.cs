using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ISensorItemEventService : IKEBaseService<Guid, SensorItemEvent>
    {
        SensorItemEvent GetLastEventByTankAndItem(Guid tankId, ItemEnum item);
    }
}