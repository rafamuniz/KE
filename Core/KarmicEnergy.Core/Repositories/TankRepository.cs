using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class TankRepository : Repository<Tank, KEContext>, ITankRepository
    {
        #region Constructor
        public TankRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor        
    }
}
