using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ISensorService : IKEBaseService
    {
        Sensor Get(Guid id);
        List<Sensor> Gets();

        Boolean HasSensorSite(Guid siteId);
        Boolean HasSensorTank(Guid tankId);
        Boolean HasSensorPond(Guid pondId);

        List<Sensor> GetsBySite(Guid siteId);
        List<Sensor> GetsByCustomerAndSite(Guid customerId, Guid siteId);
    }
}
