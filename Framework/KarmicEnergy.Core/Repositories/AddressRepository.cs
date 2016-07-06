using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class AddressRepository : KERepositoryBase<Address>, IAddressRepository
    {
        #region Constructor
        public AddressRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor    
    }
}
