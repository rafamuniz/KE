using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class StickConversionValueRepository : KERepositoryBase<StickConversionValue>, IStickConversionValueRepository
    {
        #region Constructor
        public StickConversionValueRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor       
    }
}
