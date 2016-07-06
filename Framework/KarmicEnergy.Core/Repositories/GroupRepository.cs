using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class GroupRepository : KERepositoryBase<Group>, IGroupRepository
    {
        #region Constructor
        public GroupRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor       

        public List<Group> GetsBySiteId(Guid siteId)
        {
            return base.Find(x => x.SiteId == siteId && x.DeletedDate == null).ToList();
        }
    }
}
