using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ICustomerUserRepository : IKERepositoryBase<CustomerUser>
    {
        List<CustomerUser> GetsByCustomerId(Guid customerId);
    }
}
