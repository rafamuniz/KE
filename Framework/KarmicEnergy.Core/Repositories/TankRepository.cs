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
        
        public override IEnumerable<Tank> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<Tank> tanks = new List<Tank>();
            var entities = (from t in Context.Tanks
                            join s in Context.Sites on t.SiteId equals s.Id
                            where s.LastModifiedDate > lastSyncDate &&
                                  s.Id == siteId
                            select t).ToList();

            foreach (var entity in entities)
            {
                Tank tank = new Tank()
                {
                    Id = entity.Id
                };

                tank.Update(entity);
                tanks.Add(tank);
            }

            return tanks;
        }
    }
}
