using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface IAlarmService : IKEBaseService
    {
        Alarm Get(Guid id);
        IList<Alarm> Gets();
    }
}