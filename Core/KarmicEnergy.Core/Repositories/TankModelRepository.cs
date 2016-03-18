using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class TankModelRepository : Repository<TankModel, KEContext>, ITankModelRepository
    {
        #region Constructor
        public TankModelRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor
    }
}
