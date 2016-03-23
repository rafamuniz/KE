using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class AlarmRepository : Repository<Alarm, KEContext>, IAlarmRepository
    {
        #region Constructor
        public AlarmRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor               
    }
}
