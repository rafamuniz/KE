using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IGroupRepository : IKERepositoryBase<Group>
    {
        List<Group> GetsBySiteId(Guid siteId);
    }
}
