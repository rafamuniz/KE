﻿using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;

namespace KarmicEnergy.Core.Repositories
{
    public interface ISiteRepository : IRepository<Site, KEContext>
    {
    }
}
