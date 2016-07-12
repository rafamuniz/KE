using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class CityRepository : KERepositoryBase<City>, ICityRepository
    {
        #region Constructor
        public CityRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
