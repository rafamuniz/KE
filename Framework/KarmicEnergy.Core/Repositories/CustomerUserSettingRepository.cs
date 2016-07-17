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

        public override IEnumerable<CustomerUserSetting> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<CustomerUserSetting> customerUserSettings = new List<CustomerUserSetting>();

            var entities = (from cus in Context.CustomerUserSettings
                            join cu in Context.CustomerUsers on cus.CustomerUserId equals cu.Id
                            join c in Context.Customers on cu.CustomerId equals c.Id into Customers
                            from c1 in Customers
                            join s in Context.Sites on c1.Id equals s.CustomerId
                            where cus.LastModifiedDate > lastSyncDate &&
                                  s.Id == siteId
                            select cus).ToList();

            foreach (var entity in entities)
            {
                CustomerUserSetting customerUserSetting = new CustomerUserSetting()
                {
                    Id = entity.Id
                };

                customerUserSetting.Update(entity);
                customerUserSettings.Add(customerUserSetting);
            }

            return customerUserSettings;
        }
    }
}
