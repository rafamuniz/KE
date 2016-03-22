using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorDataRepository : Repository<SensorData, KEContext>, ISensorDataRepository
    {
        #region Constructor
        public SensorDataRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor               
    }
}
