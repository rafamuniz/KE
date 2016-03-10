using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerUserRepository : Repository<CustomerUser, KEContext>, ICustomerUserRepository
    {
        #region Constructor
        public CustomerUserRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor
    }
}
