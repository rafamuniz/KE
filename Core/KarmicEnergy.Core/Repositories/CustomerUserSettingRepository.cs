using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerUserSettingRepository : Repository<CustomerUserSetting, KEContext>, ICustomerUserSettingRepository
    {
        #region Constructor
        public CustomerUserSettingRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor      

        public CustomerUserSetting GetByCustomerUserIdAndKey(Guid customerUserId, String key)
        {
            return base.Find(x => x.CustomerUserId == customerUserId && x.Key == key).SingleOrDefault();
        }

        public List<CustomerUserSetting> GetsByCustomerUserId(Guid customerUserId)
        {
            return base.Find(x => x.CustomerUserId == customerUserId).ToList();
        }
    }
}
