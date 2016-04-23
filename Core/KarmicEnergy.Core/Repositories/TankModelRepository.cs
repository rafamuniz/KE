using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class TankModelRepository : KERepositoryBase<TankModel>, ITankModelRepository
    {
        #region Constructor
        public TankModelRepository(KEContext context)
            : base(context)
        {            
        }
        #endregion Constructor
    }
}
