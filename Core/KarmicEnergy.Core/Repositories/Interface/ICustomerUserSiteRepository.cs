using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ICustomerUserSiteRepository : IKERepositoryBase<CustomerUserSite>
    {
        List<CustomerUserSite> GetsByUser(Guid userId);
        List<Site> GetsSiteByUser(Guid userId);
    }
}
