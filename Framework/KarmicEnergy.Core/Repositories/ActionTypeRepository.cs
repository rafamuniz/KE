using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class ActionTypeRepository : KERepositoryBase<ActionType>, IActionTypeRepository
    {
        #region Constructor
        public ActionTypeRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor        
    }
}
