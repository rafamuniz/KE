using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface IAlarmHistoryService : IKEBaseService<Guid, AlarmHistory>
    {
        AlarmHistory CreateComment(AlarmHistory alarmHistory);
    }
}