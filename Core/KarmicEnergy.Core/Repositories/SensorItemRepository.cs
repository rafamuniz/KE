using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorItemRepository : Repository<Item, KEContext>, ISensorItemRepository
    {
        #region Constructor
        public SensorItemRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
