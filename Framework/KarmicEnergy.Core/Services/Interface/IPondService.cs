using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface IPondService
    {
        Pond Get(Guid id);
        IList<Pond> Gets();

        IList<Pond> GetsByCustomer(Guid customerId);
        IList<Pond> GetsBySite(Guid siteId);
        IList<Pond> GetsByCustomerAndSite(Guid customerId, Guid siteId);

        IList<Pond> GetsBySiteWithAlarms(Guid siteId);
    }
}
