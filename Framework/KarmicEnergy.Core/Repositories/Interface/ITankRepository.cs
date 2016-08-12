using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ITankRepository : IKERepositoryBase<Tank>
    {
        List<Tank> GetsByCustomer(Guid customerId);
        List<Tank> GetsBySite(Guid siteId);
        List<Tank> GetsByCustomerAndSite(Guid customerId, Guid siteId);        
    }
}
