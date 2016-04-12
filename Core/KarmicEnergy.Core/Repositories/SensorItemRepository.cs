using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorItemRepository : Repository<SensorItem, KEContext>, ISensorItemRepository
    {
        #region Constructor
        public SensorItemRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor 

        public Boolean HasSensorItem(Guid tankId, ItemEnum item)
        {
            return base.Find(x => x.Sensor.TankId == tankId && x.ItemId == (Int32)item).Any();
        }
    }
}
