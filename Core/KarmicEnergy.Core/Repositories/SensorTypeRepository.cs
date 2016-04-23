using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorTypeRepository : KERepositoryBase<SensorType>, ISensorTypeRepository
    {
        #region Constructor
        public SensorTypeRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor               
    }
}
