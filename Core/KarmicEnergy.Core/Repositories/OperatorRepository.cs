using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class OperatorRepository : KERepositoryBase<Operator>, IOperatorRepository
    {
        #region Constructor
        public OperatorRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor

        public List<Operator> GetsByOperatorType(OperatorTypeEnum type)
        {
            return base.Find(x => x.OperatorTypeId == (Int16)type && x.DeletedDate == null).ToList();
        }
    }
}
