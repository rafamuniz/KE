using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ISiteService : IKEBaseService<Guid, Site>
    {
        IEnumerable<Site> GetsByCustomer(Guid customerId);
        IEnumerable<Site> GetsSiteByUser(Guid userId);
    }
}
