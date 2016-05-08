using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorGroupRepository : KERepositoryBase<SensorGroup>, ISensorGroupRepository
    {
        #region Constructor
        public SensorGroupRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor   
    }
}
