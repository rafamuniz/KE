using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ITriggerRepository : IKERepositoryBase<Trigger>
    {
        List<Trigger> GetsByTank(Guid tankId);
    }
}
