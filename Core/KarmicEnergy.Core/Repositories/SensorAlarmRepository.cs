using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class TriggerRepository : Repository<Trigger, KEContext>, ITriggerRepository
    {
        #region Constructor
        public TriggerRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor               
    }
}
