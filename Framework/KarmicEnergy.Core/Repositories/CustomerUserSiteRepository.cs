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
    }
}
