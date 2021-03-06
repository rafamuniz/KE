﻿using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public abstract class KERepositoryBase<TEntity> : Repository<TEntity, KEContext>, IKERepositoryBase<TEntity>
        where TEntity : BaseEntity
    {
        #region Constructor
        public KERepositoryBase(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor

        public IEnumerable<TEntity> GetAllActive()
        {
            return _entities.Where(x => x.DeletedDate == null).ToList();
        }

        public override void Update(TEntity entity)
        {
            entity.LastModifiedDate = DateTime.UtcNow;
            base.Update(entity);
        }


        /// <summary>
        /// Update Entity but not update the proprety LastModifiedDate
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateWithoutDate(TEntity entity)
        {
            base.Update(entity);
        }

        public virtual void SaveSync(List<TEntity> entities)
        {
            this.AddOrUpdateRange(entities);
        }

        public virtual IEnumerable<TEntity> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            return _entities.Where(x => x.LastModifiedDate > lastSyncDate);
        }
    }
}