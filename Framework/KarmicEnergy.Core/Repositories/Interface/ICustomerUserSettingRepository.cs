using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ICustomerUserSettingRepository : IKERepositoryBase<CustomerUserSetting>
    {
        CustomerUserSetting GetByCustomerUserIdAndKey(Guid customerUserId, String key);

        List<CustomerUserSetting> GetsByCustomerUserId(Guid customerUserId);
    }
}
