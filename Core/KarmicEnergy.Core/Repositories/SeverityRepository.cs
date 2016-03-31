using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;

namespace KarmicEnergy.Core.Repositories
{
    public class SeverityRepository : Repository<Severity, KEContext>, ISeverityRepository
    {
        #region Constructor
        public SeverityRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor
    }
}
