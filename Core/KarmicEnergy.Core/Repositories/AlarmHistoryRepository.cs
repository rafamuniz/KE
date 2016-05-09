using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
namespace KarmicEnergy.Core.Repositories
{
    public class AlarmHistoryRepository : KERepositoryBase<AlarmHistory>, IAlarmHistoryRepository
    {
        #region Constructor
        public AlarmHistoryRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor    
    }
}
