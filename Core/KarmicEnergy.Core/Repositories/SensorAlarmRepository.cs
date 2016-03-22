using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorAlarmRepository : Repository<SensorAlarm, KEContext>, ISensorAlarmRepository
    {
        #region Constructor
        public SensorAlarmRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor               
    }
}
