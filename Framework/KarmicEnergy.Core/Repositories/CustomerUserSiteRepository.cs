using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerUserSiteRepository : KERepositoryBase<CustomerUserSite>, ICustomerUserSiteRepository
    {
        #region Constructor
        public CustomerUserSiteRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor

        public List<CustomerUserSite> GetsByUser(Guid userId)
        {
            return base.Find(x => x.CustomerUserId == userId && x.Site.Status == "A" && x.Site.DeletedDate == null).ToList();
        }

        public List<Site> GetsSiteByUser(Guid userId)
        {
            return (from cus in Context.CustomerUserSites
                    join s in Context.Sites on cus.SiteId equals s.Id
                    where s.Status == "A" && s.DeletedDate == null
                    where cus.CustomerUserId == userId && cus.DeletedDate == null
                    select s).ToList();
        }

        public override IEnumerable<CustomerUserSite> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<CustomerUserSite> customerUserSites = new List<CustomerUserSite>();

            var entities = (from cus in Context.CustomerUserSites
                            join cu in Context.CustomerUsers on cus.CustomerUserId equals cu.Id
                            join c in Context.Customers on cu.CustomerId equals c.Id into Customers
                            from c1 in Customers
                            join s in Context.Sites on c1.Id equals s.CustomerId
                            where cus.LastModifiedDate > lastSyncDate &&
                                  s.Id == siteId
                            select cus).ToList();

            foreach (var entity in entities)
            {
                CustomerUserSite customerUserSite = new CustomerUserSite()
                {
                    Id = entity.Id
                };

                customerUserSite.Update(entity);
                customerUserSites.Add(customerUserSite);
            }

            return customerUserSites;
        }
    }
}
