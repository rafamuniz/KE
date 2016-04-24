using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorGroupRepository : KERepositoryBase<SensorGroup>, ISensorGroupRepository
    {
        #region Constructor
        public SensorGroupRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
