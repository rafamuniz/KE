using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface IKEBaseService<TKey, TEntity>
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey entity);
        TEntity Get(TKey id);
        IEnumerable<TEntity> GetAll();
    }
}
