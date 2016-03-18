using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;

namespace KarmicEnergy.Core.Repositories
{
    public class CountryRepository : Repository<Country, KEContext>, ICountryRepository
    {
        #region Constructor
        public CountryRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor
    }
}
