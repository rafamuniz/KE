using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ITriggerRepository : IKERepositoryBase<Trigger>
    {
        List<Trigger> GetsAllBySite(Guid siteId);
        List<Trigger> GetsBySite(Guid siteId, Int32 quantity = 5);
        List<Trigger> GetsByTank(Guid tankId);
    }
}
