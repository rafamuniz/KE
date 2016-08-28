using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ISiteService : IKEBaseService<Guid, Site>
    {
        IEnumerable<Site> GetAllWithLocation();

        IEnumerable<Site> GetsByCustomer(Guid customerId);
        IEnumerable<Site> GetsByCustomerWithLocation(Guid customerId);

        IEnumerable<Site> GetsSiteByUser(Guid userId);
        IEnumerable<Site> GetsSiteByUserWithLocation(Guid userId);
    }
}
