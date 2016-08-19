using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorRepository : IKERepositoryBase<Sensor>
    {
        Boolean HasSensorSite(Guid siteId);
        Boolean HasSensorTank(Guid tankId);
        Boolean HasSensorPond(Guid pondId);

        IEnumerable<Sensor> GetsActive();

        IEnumerable<Sensor> GetsByCustomer(Guid customerId);
        IEnumerable<Sensor> GetsBySite(Guid siteId);
        IEnumerable<Sensor> GetsByPond(Guid pondId);
        IEnumerable<Sensor> GetsByTank(Guid tankId);

        IEnumerable<Sensor> GetsByCustomerAndSite(Guid customerId, Guid siteId);
        IEnumerable<Sensor> GetsByCustomerAndTank(Guid customerId, Guid tankId);
        IEnumerable<Sensor> GetsByCustomerAndPond(Guid customerId, Guid pondId);

        IEnumerable<Sensor> GetsBySiteAndSensorType(Guid siteId, SensorTypeEnum sensorType);
    }
}
