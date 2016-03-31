using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ITankRepository : IRepository<Tank, KEContext>
    {
        List<Tank> GetsByCustomerId(Guid customerId);
        List<Tank> GetsByCustomerIdAndSiteId(Guid customerId, Guid siteId);
        List<Models.TankModel> GetsWithLastMeasurement(Guid customerId);
    }
}
