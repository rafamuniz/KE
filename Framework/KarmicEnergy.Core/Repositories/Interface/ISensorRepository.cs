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

        List<Sensor> GetsActive();

        List<Sensor> GetsByCustomer(Guid customerId);
        List<Sensor> GetsBySite(Guid siteId);
        List<Sensor> GetsByCustomerAndSite(Guid customerId, Guid siteId);
        List<Sensor> GetsByCustomerAndTank(Guid customerId, Guid tankId);
        List<Sensor> GetsByCustomerAndPond(Guid customerId, Guid pondId);

        List<Sensor> GetsBySiteAndSensorType(Guid siteId, SensorTypeEnum sensorType);
    }
}
