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

        public List<Site> GetsByCustomer(Guid customerId)
        {
            return base.Find(x => x.CustomerId == customerId && x.DeletedDate == null).ToList();
        }

        public override IEnumerable<Site> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<Site> sites = new List<Site>();
            var entities = base.Find(x => x.Id == siteId && x.LastModifiedDate > lastSyncDate).ToList();

            foreach (var entity in entities)
            {
                Site site = new Site()
                {
                    Id = entity.Id
                };

                site.Update(entity);
                sites.Add(site);
            }

            return sites;
        }
    }
}
