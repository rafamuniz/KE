using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerSettingRepository : KERepositoryBase<CustomerSetting>, ICustomerSettingRepository
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
