using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class LogRepository : KERepositoryBase<Log>, ILogRepository
    {
        #region Constructor
        public LogRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor      
        
        public List<Log> GetsByCustomer(Guid customerId)
        {
            return base.Find(x => x.CustomerId == customerId && x.DeletedDate == null).OrderByDescending(d => d.CreatedDate).ToList();
        }
        
        public List<Log> GetsBySite(Guid siteId)
        {
            return base.Find(x => x.SiteId == siteId && x.DeletedDate == null).OrderByDescending(d => d.CreatedDate).ToList();
        }

        public List<Log> GetsByUser(Guid userId)
        {
            return base.Find(x => x.UserId == userId && x.DeletedDate == null).OrderByDescending(d => d.CreatedDate).ToList();
        }        
    }
}
