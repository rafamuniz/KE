using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IKERepositoryBase<TEntity> : IRepository<TEntity, KEContext>
        where TEntity : class, new()
    {
        IEnumerable<TEntity> GetAllActive();
        void Sync(List<TEntity> entities);
    }
}
