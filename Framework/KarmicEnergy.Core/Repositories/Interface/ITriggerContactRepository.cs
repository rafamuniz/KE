using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ITriggerContactRepository : IKERepositoryBase<TriggerContact>
    {
        List<TriggerContact> GetsByTrigger(Guid triggerId);        
    }
}
