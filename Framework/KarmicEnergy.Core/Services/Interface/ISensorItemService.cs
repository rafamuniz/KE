using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ISensorItemService : IKEBaseService<Guid, SensorItem>
    {
        Boolean HasSiteSensorItem(Guid siteId, ItemEnum item);

        Boolean HasPondSensorItem(Guid pondId, ItemEnum item);

        Boolean HasTankSensorItem(Guid tankId, ItemEnum item);

        Boolean HasSensorSensorItem(Guid sensorId, ItemEnum item);

    }
}