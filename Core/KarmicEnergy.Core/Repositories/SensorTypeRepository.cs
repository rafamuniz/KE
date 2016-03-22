using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorTypeRepository : Repository<SensorType, KEContext>, ISensorTypeRepository
    {
        #region Constructor
        public SensorTypeRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor               
    }
}
