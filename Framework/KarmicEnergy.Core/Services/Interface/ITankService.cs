using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ITankService : IKEBaseService<Guid, Tank>
    {
        IEnumerable<Tank> GetsByCustomer(Guid customerId);
        IEnumerable<Tank> GetsBySite(Guid siteId);
        IEnumerable<Tank> GetsByCustomerAndSite(Guid customerId, Guid siteId);

        IEnumerable<Tank> GetsBySiteWithTankModel(Guid siteId);
        IEnumerable<Tank> GetsBySiteWithAlarms(Guid siteId);
    }
}
