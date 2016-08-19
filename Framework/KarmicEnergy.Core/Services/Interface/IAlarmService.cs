using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface IAlarmService : IKEBaseService<Guid, Alarm>
    {
        void Acknowledge(Guid alarmId, Guid userId, String username);
        void Clear(Guid alarmId, Guid userId, String message);

        IEnumerable<Alarm> GetsBySite(Guid siteId);
        IEnumerable<Alarm> GetsBySiteWithTrigger(Guid siteId);

        IEnumerable<Alarm> GetsByPond(Guid pondId);
        IEnumerable<Alarm> GetsByPondWithTrigger(Guid pondId);

        IEnumerable<Alarm> GetsByTank(Guid tankId);
        IEnumerable<Alarm> GetsByTankWithTrigger(Guid tankId);

        IEnumerable<Alarm> GetsBySensor(Guid sensorId);
        IEnumerable<Alarm> GetsBySensorWithTrigger(Guid sensorId);
    }
}