using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorItemRepository : IKERepositoryBase<SensorItem>
    {
        Boolean HasSiteSensorItem(Guid siteId, ItemEnum item);
        Boolean HasPondSensorItem(Guid pondId, ItemEnum item);
        Boolean HasTankSensorItem(Guid tankId, ItemEnum item);
        Boolean HasSensorSensorItem(Guid sensorId, ItemEnum item);

        List<SensorItem> GetsBySiteAndItem(Guid siteId, ItemEnum item);
        List<SensorItem> GetsBySensor(Guid sensorId);
        SensorItem GetsBySensorAndItem(Guid sensorId, ItemEnum item);
    }
}
