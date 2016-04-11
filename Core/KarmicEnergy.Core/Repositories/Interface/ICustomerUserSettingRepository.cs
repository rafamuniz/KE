using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ICustomerUserSettingRepository : IRepository<CustomerUserSetting, KEContext>
    {
        CustomerUserSetting GetByCustomerUserIdAndKey(Guid customerUserId, String key);

        List<CustomerUserSetting> GetsByCustomerUserId(Guid customerUserId);
    }
}
