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

        public override IEnumerable<CustomerSetting> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<CustomerSetting> customerSettings = new List<CustomerSetting>();
            var entities = (from cus in Context.CustomerSettings
                            join cu in Context.Customers on cus.CustomerId equals cu.Id into Customers
                            from c1 in Customers
                            join s in Context.Sites on c1.Id equals s.CustomerId
                            where cus.LastModifiedDate > lastSyncDate &&
                                  s.Id == siteId
                            select cus).ToList();

            foreach (var entity in entities)
            {
                CustomerSetting customerSetting = new CustomerSetting()
                {
                    Id = entity.Id
                };

                customerSetting.Update(entity);
                customerSettings.Add(customerSetting);
            }

            return customerSettings;
        }
    }
}
