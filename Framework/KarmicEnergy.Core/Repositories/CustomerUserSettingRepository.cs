using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerUserSettingRepository : KERepositoryBase<CustomerUserSetting>, ICustomerUserSettingRepository
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
