using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface IAlarmService : IKEBaseService
    {
        Alarm Get(Guid id);
        List<Alarm> Gets();

        List<Alarm> GetsBySite(Guid siteId);
        List<Alarm> GetsBySiteWithTrigger(Guid siteId);

        List<Alarm> GetsByPond(Guid pondId);
        List<Alarm> GetsByPondWithTrigger(Guid pondId);

        List<Alarm> GetsByTank(Guid tankId);
        List<Alarm> GetsByTankWithTrigger(Guid tankId);

        List<Alarm> GetsBySensor(Guid sensorId);
        List<Alarm> GetsBySensorWithTrigger(Guid sensorId);
    }
}