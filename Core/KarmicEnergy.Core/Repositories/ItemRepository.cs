using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class ItemRepository : KERepositoryBase<Item>, IItemRepository
    {
        #region Constructor
        public ItemRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor     

        public List<Item> GetsBySensorTypeId(Int16 sensorTypeId)
        {
            return base.Find(x => x.SensorTypeId == sensorTypeId && x.DeletedDate == null).ToList();
        }
    }
}
