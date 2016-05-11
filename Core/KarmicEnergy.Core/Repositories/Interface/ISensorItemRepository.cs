using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorItemRepository : IKERepositoryBase<SensorItem>
    {
        Boolean HasSensorItem(Guid tankId, ItemEnum item);
        List<SensorItem> GetsBySensor(Guid sensorId);
    }
}
