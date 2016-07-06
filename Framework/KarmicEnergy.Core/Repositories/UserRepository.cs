using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class UserRepository : KERepositoryBase<User>, IUserRepository
    {
        #region Constructor
        public UserRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor
    }
}
