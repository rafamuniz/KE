using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorRepository : IKERepositoryBase<Sensor>
    {
        Boolean HasSensorTank(Guid tankId);
        Boolean HasSensorSite(Guid siteId);

        List<Sensor> GetsByCustomer(Guid customerId);
        List<Sensor> GetsByTank(Guid tankId);
        List<Sensor> GetsByCustomerAndTank(Guid customerId, Guid tankId);

        List<Sensor> GetsBySite(Guid siteId);
        List<Sensor> GetsByCustomerAndSite(Guid customerId, Guid siteId);

        List<Sensor> GetsBySiteAndSensorType(Guid siteId, SensorTypeEnum sensorType);
    }
}
