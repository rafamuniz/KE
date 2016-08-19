using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface IPondService : IKEBaseService<Guid, Pond>
    {
        IEnumerable<Pond> GetsByCustomer(Guid customerId);
        IEnumerable<Pond> GetsBySite(Guid siteId);
        IEnumerable<Pond> GetsByCustomerAndSite(Guid customerId, Guid siteId);

        IEnumerable<Pond> GetsBySiteWithAlarms(Guid siteId);
    }
}
