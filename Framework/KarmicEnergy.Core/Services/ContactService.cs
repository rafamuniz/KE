using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class ContactService : KEServiceBase<Guid, Contact>, IContactService
    {
        #region Constructor

        public ContactService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions
        public override void Create(Contact entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity cannot be null");

            this._unitOfWork.ContactRepository.Add(entity);
            this._unitOfWork.Complete();
        }

        public override void Update(Contact entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity cannot be null");

            var contact = this._unitOfWork.ContactRepository.Get(entity.Id);

            entity.Update(contact);

            var updatedDate = DateTime.UtcNow;
            entity.LastModifiedDate = updatedDate;

            this._unitOfWork.ContactRepository.Update(entity);
            this._unitOfWork.Complete();
        }

        public override void Delete(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var contact = this._unitOfWork.ContactRepository.Get(id);
            contact.DeletedDate = DateTime.UtcNow;
            this._unitOfWork.ContactRepository.Update(contact);
        }

        public override Contact Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.ContactRepository.Get(id);
        }

        public override IEnumerable<Contact> GetAll()
        {
            return this._unitOfWork.ContactRepository.GetAll();
        }
                
        #endregion Functions
    }
}
