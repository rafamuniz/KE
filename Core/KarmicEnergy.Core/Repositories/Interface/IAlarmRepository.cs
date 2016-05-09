using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IAlarmRepository : IKERepositoryBase<Alarm>
    {
        List<Alarm> GetsActive();
        List<Alarm> GetsBySiteId(Guid siteId);
        Int32 GetTotalOpenByTankId(Guid tankId);
    }
}
