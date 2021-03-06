﻿using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ILogRepository : IKERepositoryBase<Log>
    {
        List<Log> GetsByCustomer(Guid customerId);
        List<Log> GetsBySite(Guid siteId);
        List<Log> GetsByUser(Guid userId);
    }
}
