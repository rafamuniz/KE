using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorRepository : IKERepositoryBase<Sensor>
    {
        Boolean HasSensor(Guid tankId);
        List<Sensor> GetsByCustomerId(Guid customerId);
        List<Sensor> GetsByTankId(Guid tankId);
        List<Sensor> GetsByTankIdAndCustomerId(Guid customerId, Guid tankId);
    }
}
