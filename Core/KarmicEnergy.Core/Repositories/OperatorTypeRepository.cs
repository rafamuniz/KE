using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class OperatorTypeRepository : KERepositoryBase<OperatorType>, IOperatorTypeRepository
    {
        #region Constructor
        public OperatorTypeRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor
    }
}
