using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISiteRepository : IKERepositoryBase<Site>
    {
        List<Site> GetsByCustomer(Guid customerId);
    }
}
