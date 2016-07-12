using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class GeometryRepository : KERepositoryBase<Geometry>, IGeometryRepository
    {
        #region Constructor
        public GeometryRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
