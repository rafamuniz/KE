using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class CustomerService : KEServiceBase<Guid, Customer>, ICustomerService
    {
        #region Constructor

        public CustomerService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override Customer Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.CustomerRepository.Get(id);
        }

        public override IEnumerable<Customer> GetAll()
        {
            return this._unitOfWork.CustomerRepository.GetAll();
        }

        public override void Delete(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var deletedDate = DateTime.UtcNow;

            var customer = this._unitOfWork.CustomerRepository.Get(id);
            customer.DeletedDate = deletedDate;

            this._unitOfWork.CustomerRepository.Update(customer);            
        }
 
        #endregion Functions
    }
}
