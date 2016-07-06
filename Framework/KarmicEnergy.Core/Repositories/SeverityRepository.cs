using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class SeverityRepository : KERepositoryBase<Severity>, ISeverityRepository
    {
        #region Constructor
        public SeverityRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor
    }
}
