using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class CountryRepository : KERepositoryBase<Country>, ICountryRepository
    {
        #region Constructor
        public CountryRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor
    }
}
