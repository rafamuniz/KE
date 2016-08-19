using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using Munizoft.Extensions;

namespace KarmicEnergy.Core.Services
{
    public class UserService : KEServiceBase<Guid, User>, IUserService
    {
        #region Constructor

        public UserService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override void Create(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity cannot be null");

            this._unitOfWork.UserRepository.Add(entity);
            this._unitOfWork.Complete();
        }

        public override void Update(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity cannot be null");

            var e = this._unitOfWork.UserRepository.Get(entity.Id);

            entity.Update(e);

            var UpdatedDate = DateTime.UtcNow;
            entity.LastModifiedDate = UpdatedDate;

            this._unitOfWork.UserRepository.Update(e);
            this._unitOfWork.Complete();
        }

        public override void Delete(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var deletedDate = DateTime.UtcNow;
            var entity = this._unitOfWork.UserRepository.Get(id);
            entity.DeletedDate = deletedDate;

            this._unitOfWork.UserRepository.Update(entity);
        }

        public override User Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.UserRepository.Get(id);
        }
             
        public override IEnumerable<User> GetAll()
        {
            return this._unitOfWork.UserRepository.GetAll();
        }

        #endregion Functions
    }
}
