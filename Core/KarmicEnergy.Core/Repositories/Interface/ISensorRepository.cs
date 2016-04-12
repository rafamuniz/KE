using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISensorRepository : IRepository<Sensor, KEContext>
    {
        Boolean HasSensor(Guid tankId);
        List<Sensor> GetsByCustomerId(Guid customerId);
        List<Sensor> GetsByTankId(Guid tankId);
        List<Sensor> GetsByTankIdAndCustomerId(Guid customerId, Guid tankId);
    }
}
