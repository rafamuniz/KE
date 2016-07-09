using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class DataSyncRepository : KERepositoryBase<DataSync>, IDataSyncRepository
    {
        #region Constructor
        public DataSyncRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor        
    }
}
