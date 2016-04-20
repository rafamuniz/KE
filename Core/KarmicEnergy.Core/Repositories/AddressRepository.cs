using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class AddressRepository : KERepositoryBase<Address, KEContext>, IAddressRepository
    {
        #region Constructor
        public AddressRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor    
    }
}
