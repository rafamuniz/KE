using KarmicEnergy.Core.Entities;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IOperatorRepository : IKERepositoryBase<Operator>
    {
        List<Operator> GetsByOperatorType(OperatorTypeEnum type);
    }
}
