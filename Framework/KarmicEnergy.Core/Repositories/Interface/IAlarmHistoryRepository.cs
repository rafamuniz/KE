using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IAlarmHistoryRepository : IKERepositoryBase<AlarmHistory>
    {
        List<AlarmHistory> GetsByActionType(Guid triggerId, ActionTypeEnum actionType);
    }
}
