using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class ItemRepository : Repository<Item, KEContext>, IItemRepository
    {
        #region Constructor
        public ItemRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
