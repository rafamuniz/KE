using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class TankRepository : KERepositoryBase<Tank>, ITankRepository
    {
        #region Constructor
        public TankRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor        

        public List<Tank> GetsByCustomerId(Guid customerId)
        {
            return base.Find(x => x.Site.CustomerId == customerId && x.DeletedDate == null).ToList();
        }

        public List<Tank> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            return base.Find(x => x.Site.CustomerId == customerId && x.SiteId == siteId && x.Status == "A" && x.DeletedDate == null).ToList();
        }       
    }
}
