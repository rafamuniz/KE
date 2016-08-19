using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using Munizoft.Extensions;

namespace KarmicEnergy.Core.Services
{
    public class TriggerService : KEServiceBase<Guid, Trigger>, ITriggerService
    {
        #region Constructor

        public TriggerService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override void Create(Trigger entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity cannot be null");

            this._unitOfWork.TriggerRepository.Add(entity);
            this._unitOfWork.Complete();
        }

        public override void Update(Trigger entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity cannot be null");

            var e = this._unitOfWork.TriggerRepository.Get(entity.Id);

            entity.Update(e);

            var UpdatedDate = DateTime.UtcNow;
            entity.LastModifiedDate = UpdatedDate;

            this._unitOfWork.TriggerRepository.Update(e);
            this._unitOfWork.Complete();
        }

        public override void Delete(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var deletedDate = DateTime.UtcNow;
            var entity = this._unitOfWork.TriggerRepository.Get(id);
            entity.DeletedDate = deletedDate;

            this._unitOfWork.TriggerRepository.Update(entity);
        }

        public override Trigger Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.TriggerRepository.Get(id);
        }

        public Tank Get(Guid id, params String[] includes)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var tanks = this._unitOfWork.TankRepository.Find(x => x.Id == id, includes).ToList();

            return tanks.Single();
        }

        public override IEnumerable<Trigger> GetAll()
        {
            return this._unitOfWork.TriggerRepository.GetAll();
        }

        #endregion Functions
    }
}
