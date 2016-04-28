using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class LogRepository : KERepositoryBase<Log>, ILogRepository
    {
        #region Constructor
        public LogRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
