using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class LogTypeRepository : KERepositoryBase<LogType>, ILogTypeRepository
    {
        #region Constructor
        public LogTypeRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
