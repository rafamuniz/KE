using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IAlarmRepository : IKERepositoryBase<Alarm>
    {
        IEnumerable<Alarm> GetsOpen();
        IEnumerable<Alarm> GetsOpen(params String[] includes);

        IEnumerable<Alarm> GetsOpenByCustomer(Guid customerId);
        IEnumerable<Alarm> GetsOpenByCustomer(Guid customerId, params String[] includes);
        
        IEnumerable<Alarm> GetsOpenBySite(Guid siteId);
        IEnumerable<Alarm> GetsOpenBySite(Guid siteId, params String[] includes);

        IEnumerable<Alarm> GetsOpenByPond(Guid pondId);
        IEnumerable<Alarm> GetsOpenByPond(Guid pondId, params String[] includes);

        IEnumerable<Alarm> GetsOpenByTank(Guid tankId);
        IEnumerable<Alarm> GetsOpenByTank(Guid tankId, params String[] includes);

        IEnumerable<Alarm> GetsOpenBySensor(Guid sensorId);
        IEnumerable<Alarm> GetsOpenBySensor(Guid sensorId, params String[] includes);
        
        IEnumerable<Alarm> GetsBySite(Guid siteId);
        IEnumerable<Alarm> GetsBySite(Guid siteId, params String[] includes);

        IEnumerable<Alarm> GetsByPond(Guid pondId);
        IEnumerable<Alarm> GetsByPond(Guid pondId, params String[] includes);

        IEnumerable<Alarm> GetsByTank(Guid tankId);
        IEnumerable<Alarm> GetsByTank(Guid tankId, params String[] includes);

        IEnumerable<Alarm> GetsBySensor(Guid sensorId);
        IEnumerable<Alarm> GetsBySensor(Guid sensorId, params String[] includes);

        Int32 GetTotalOpenByTankId(Guid tankId);

        IEnumerable<Alarm> GetsByTrigger(Guid triggerId);
        Alarm GetOpenByTrigger(Guid triggerId);
        IEnumerable<Alarm> GetsCloseByTrigger(Guid triggerId);
    }
}
