using System;

namespace KarmicEnergy.Core.Entities.Interface
{
    public interface IConvertUnit
    {
        String Convert();
        String Convert(Int16 unitId);
    }
}
