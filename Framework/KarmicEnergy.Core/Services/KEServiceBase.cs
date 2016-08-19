using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public abstract class KEServiceBase<TKey, TEntity> : IKEBaseService<TKey, TEntity>
    {
        #region Fields
        protected readonly IKEUnitOfWork _unitOfWork;
        #endregion Fields

        #region Constructor
        public KEServiceBase(IKEUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region Functions
        public virtual void Create(TEntity entity)
        {
            //return entity;
            //if (entity == null)
            //{
            //    throw new ArgumentNullException("entity");
            //}

            //_repository.Add(entity);
            //_unitOfWork.Commit();
        }

        public virtual void Update(TEntity entity)
        {
            //return entity;
            //if (entity == null)
            //    throw new ArgumentNullException("entity");

            //_repository.Update(entity);
            //_unitOfWork.Commit();
        }

        public virtual void Delete(TKey id)
        {
            //if (entity == null)
            //    throw new ArgumentNullException("entity");

            //_repository.Delete(entity);
            //_unitOfWork.Commit();
        }

        public virtual TEntity Get(TKey id)
        {
            return Activator.CreateInstance<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return new List<TEntity>();
            //return _repository.GetAll();
        }

        #endregion Functions
    }
}