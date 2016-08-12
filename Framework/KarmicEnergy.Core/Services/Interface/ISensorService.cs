using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ISensorService : IKEBaseService
    {
        Sensor Get(Guid id);
        IEnumerable<Sensor> Gets();

        Boolean HasSensorSite(Guid siteId);
        Boolean HasSensorTank(Guid tankId);
        Boolean HasSensorPond(Guid pondId);

        IEnumerable<Sensor> GetsByCustomerAndSite(Guid customerId, Guid siteId);
    }
}
