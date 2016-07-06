using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IItemRepository : IKERepositoryBase<Item>
    {
        List<Item> GetsBySensorTypeId(Int16 sensorTypeId);
    }
}
