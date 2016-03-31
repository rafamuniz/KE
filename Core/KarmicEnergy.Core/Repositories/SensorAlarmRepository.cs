using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorItemAlarmRepository : Repository<SensorItemAlarm, KEContext>, ISensorItemAlarmRepository
    {
        #region Constructor
        public SensorItemAlarmRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor               
    }
}
