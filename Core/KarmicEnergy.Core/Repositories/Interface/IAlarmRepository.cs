using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IAlarmRepository : IKERepositoryBase<Alarm>
    {
        List<Alarm> GetsActive();
        List<Alarm> GetsActiveByCustomer(Guid customerId);
        List<Alarm> GetsActiveBySite(Guid siteId);
        List<Alarm> GetsActiveByPond(Guid pondId);
        List<Alarm> GetsActiveByTank(Guid tankId);

        List<Alarm> GetsBySite(Guid siteId);
        List<Alarm> GetsByTank(Guid tankId);

        Int32 GetTotalOpenByTankId(Guid tankId);

        Alarm GetActiveByTrigger(Guid triggerId);
        List<Alarm> GetsCloseByTrigger(Guid triggerId);
    }
}
