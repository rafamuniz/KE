using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class UnitTypeRepository : KERepositoryBase<UnitType>, IUnitTypeRepository
    {
        #region Constructor
        public UnitTypeRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
