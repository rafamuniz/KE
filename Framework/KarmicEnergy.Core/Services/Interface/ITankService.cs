using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ITankService
    {
        Tank Get(Guid id);
        Tank Get(Guid id, params String[] includes);

        IList<Tank> Gets();

        IList<Tank> GetsByCustomer(Guid customerId);
        IList<Tank> GetsBySite(Guid siteId);
        IList<Tank> GetsByCustomerAndSite(Guid customerId, Guid siteId);

        IList<Tank> GetsBySiteWithTankModel(Guid siteId);
        IList<Tank> GetsBySiteWithAlarms(Guid siteId);
    }
}
