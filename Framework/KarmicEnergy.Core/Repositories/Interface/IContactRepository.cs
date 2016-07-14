using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IContactRepository : IKERepositoryBase<Contact>
    {
        List<Contact> GetsByCustomer(Guid customerId);
    }
}
