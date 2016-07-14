using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ITriggerRepository : IKERepositoryBase<Trigger>
    {
        List<Trigger> GetsBySite(Guid siteId);        
        List<Trigger> GetsByPond(Guid pondId);        
        List<Trigger> GetsByTank(Guid tankId);

        //List<Trigger> GetsAllBySite(Guid siteId);
        List<Trigger> GetsBySiteAndQuantity(Guid siteId, Int32 quantity = 5);
        //List<Trigger> GetsByTank(Guid tankId);
    }
}
