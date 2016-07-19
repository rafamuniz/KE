using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class PondRepository : KERepositoryBase<Pond>, IPondRepository
    {
        #region Constructor
        public PondRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor        

        public List<Pond> GetsByCustomer(Guid customerId)
        {
            return base.Find(x => x.Site.CustomerId == customerId && x.DeletedDate == null).ToList();
        }

        public List<Pond> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            return base.Find(x => x.Site.CustomerId == customerId && x.SiteId == siteId && x.DeletedDate == null).ToList();
        }

        public override IEnumerable<Pond> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<Pond> ponds = new List<Pond>();
            var entities = base.Find(x => x.SiteId == siteId && x.LastModifiedDate > lastSyncDate).ToList();

            foreach (var entity in entities)
            {
                Pond pond = new Pond()
                {
                    Id = entity.Id
                };

                pond.Update(entity);
                ponds.Add(pond);
            }

            return ponds;
        }
    }
}
