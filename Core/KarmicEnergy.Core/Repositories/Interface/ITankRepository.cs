using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ITankRepository : IKERepositoryBase<Tank>
    {
        List<Tank> GetsByCustomerId(Guid customerId);
        List<Tank> GetsByCustomerAndSite(Guid customerId, Guid siteId);
        List<Models.TankModel> GetsWithLastMeasurement(Guid customerId);
    }
}
