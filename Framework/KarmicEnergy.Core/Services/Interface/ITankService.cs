using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ITankService
    {
        Tank Get(Guid id);
        IEnumerable<Tank> Gets();
        IEnumerable<Tank> GetsByCustomerAndSite(Guid customerId, Guid siteId);
    }
}
