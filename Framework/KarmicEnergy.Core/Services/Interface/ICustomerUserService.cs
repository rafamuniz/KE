using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ICustomerUserService : IKEBaseService<Guid, CustomerUser>
    {
        IEnumerable<CustomerUser> GetsByCustomer(Guid customerId);
    }
}