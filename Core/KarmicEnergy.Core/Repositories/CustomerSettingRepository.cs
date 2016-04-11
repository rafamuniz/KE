using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerSettingRepository : Repository<CustomerSetting, KEContext>, ICustomerSettingRepository
    {
        #region Constructor
        public CustomerSettingRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor     
        
        public CustomerSetting GetByCustomerIdAndKey(Guid customerId, String key)
        {
            return base.Find(x => x.CustomerId == customerId && x.Key == key).SingleOrDefault();
        }

        public List<CustomerSetting> GetsByCustomerId(Guid customerId)
        {
            return base.Find(x => x.CustomerId == customerId).ToList();
        }
    }
}
