using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class GroupRepository : KERepositoryBase<Group>, IGroupRepository
    {
        #region Constructor
        public GroupRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
