using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class SiteRepository : KERepositoryBase<Site>, ISiteRepository
    {
        #region Constructor
        public SiteRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor

        public List<Site> GetsByCustomerId(Guid customerId)
        {
            return base.Find(x => x.CustomerId == customerId && x.DeletedDate == null).ToList();
        }
    }
}
