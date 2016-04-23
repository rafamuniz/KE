using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface ICustomerSettingRepository : IKERepositoryBase<CustomerSetting>
    {
        CustomerSetting GetByCustomerIdAndKey(Guid customerId, String key);
        
        List<CustomerSetting> GetsByCustomerId(Guid customerId);        
    }
}
