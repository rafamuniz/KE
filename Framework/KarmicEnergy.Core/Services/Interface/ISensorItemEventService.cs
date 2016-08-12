using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ISensorItemEventService : IKEBaseService
    {
        SensorItemEvent Get(Guid id);
        IList<SensorItemEvent> Gets();

        SensorItemEvent GetLastEventByTankAndItem(Guid tankId, ItemEnum item);
    }
}