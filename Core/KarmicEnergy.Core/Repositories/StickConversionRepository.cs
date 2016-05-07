using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class StickConversionRepository : KERepositoryBase<StickConversion>, IStickConversionRepository
    {
        #region Constructor
        public StickConversionRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor       
    }
}
