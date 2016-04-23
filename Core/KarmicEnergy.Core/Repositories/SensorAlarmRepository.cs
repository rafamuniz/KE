using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class TriggerRepository : KERepositoryBase<Trigger>, ITriggerRepository
    {
        #region Constructor
        public TriggerRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor               
    }
}
