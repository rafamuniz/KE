using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class ItemRepository : KERepositoryBase<Item>, IItemRepository
    {
        #region Constructor
        public ItemRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
