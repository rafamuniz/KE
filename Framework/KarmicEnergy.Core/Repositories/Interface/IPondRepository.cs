using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IPondRepository : IKERepositoryBase<Pond>
    {
        List<Pond> GetsByCustomer(Guid customerId);
        List<Pond> GetsByCustomerAndSite(Guid customerId, Guid siteId);
    }
}
