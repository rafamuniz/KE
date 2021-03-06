﻿using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IKERepositoryBase<TEntity> : IRepository<TEntity, KEContext>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAllActive();
        void UpdateWithoutDate(TEntity entity);
        void SaveSync(List<TEntity> entities);
        IEnumerable<TEntity> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate);
    }
}
