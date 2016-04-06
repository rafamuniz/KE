using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorItemEventRepository : IRepository<SensorItemEvent, KEContext>
    {
        SensorItemEvent GetTankWithWaterVolumeLastData(Guid tankId);
    }
}
