using KarmicEnergy.Core.Entities;
using System;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorItemRepository : IKERepositoryBase<SensorItem>
    {
        Boolean HasSensorItem(Guid tankId, ItemEnum item);
    }
}
