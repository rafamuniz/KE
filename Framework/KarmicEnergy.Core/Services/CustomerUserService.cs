using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using Munizoft.Extensions;

namespace KarmicEnergy.Core.Services
{
    public class CustomerUserService : KEServiceBase<Guid, CustomerUser>, ICustomerUserService
    {
        #region Constructor

        public CustomerUserService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override void Create(CustomerUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity cannot be null");

            this._unitOfWork.CustomerUserRepository.Add(entity);
            this._unitOfWork.Complete();
        }

        public override void Update(CustomerUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity cannot be null");

            var e = this._unitOfWork.CustomerUserRepository.Get(entity.Id);

            entity.Update(e);

            var UpdatedDate = DateTime.UtcNow;
            entity.LastModifiedDate = UpdatedDate;

            this._unitOfWork.CustomerUserRepository.Update(e);
            this._unitOfWork.Complete();
        }

        public override void Delete(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var deletedDate = DateTime.UtcNow;
            var entity = this._unitOfWork.CustomerUserRepository.Get(id);
            entity.DeletedDate = deletedDate;

            this._unitOfWork.CustomerUserRepository.Update(entity);
        }

        public override CustomerUser Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.CustomerUserRepository.Get(id);
        }
             
        public override IEnumerable<CustomerUser> GetAll()
        {
            return this._unitOfWork.CustomerUserRepository.GetAll();
        }

        #endregion Functions
    }
}
