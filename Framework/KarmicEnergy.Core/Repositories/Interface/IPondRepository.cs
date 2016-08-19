using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IPondRepository : IKERepositoryBase<Pond>
    {
        IEnumerable<Pond> GetsByCustomer(Guid customerId);
        IEnumerable<Pond> GetsBySite(Guid siteId);
        IEnumerable<Pond> GetsByCustomerAndSite(Guid customerId, Guid siteId);
    }
}
