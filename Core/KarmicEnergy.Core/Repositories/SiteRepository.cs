using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;

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
    }
}
