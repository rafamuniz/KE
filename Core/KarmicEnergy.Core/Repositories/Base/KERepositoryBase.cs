using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public abstract class KERepositoryBase<TEntity, Ctx> : Repository<TEntity, Ctx>
        where TEntity : BaseEntity
        where Ctx : KEContext, new()
    {
        #region Constructor
        public KERepositoryBase(Ctx context)
            : base(context)
        {

        }
        #endregion Constructor

        public IEnumerable<TEntity> GetAllActive()
        {
            return _entities.Where(x => x.DeletedDate != null).ToList();
        }

        public override void Remove(TEntity entity)
        {
            entity.DeletedDate = DateTime.UtcNow;
            base.Remove(entity);
        }

        public override void RemoveRange(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(e => Remove(e));
        }
    }
}