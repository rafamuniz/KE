using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ISensorService : IKEBaseService<Guid, Sensor>
    {
        Boolean HasSensorSite(Guid siteId);
        Boolean HasSensorTank(Guid tankId);
        Boolean HasSensorPond(Guid pondId);

        IEnumerable<Sensor> GetsBySite(Guid siteId);
        IEnumerable<Sensor> GetsByCustomerAndSite(Guid customerId, Guid siteId);
    }
}
