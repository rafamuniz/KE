using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class UnitRepository : KERepositoryBase<Unit>, IUnitRepository
    {
        #region Constructor
        public UnitRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
