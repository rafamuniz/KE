using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SiteRepository : Repository<Site, KEContext>, ISiteRepository
    {
        #region Constructor
        public SiteRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor

        public List<Site> GetsByCustomerId(Guid customerId)
        {
            return base.Find(x => x.CustomerId == customerId).ToList();
        }
    }
}
