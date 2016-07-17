using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerUserRepository : KERepositoryBase<CustomerUser>, ICustomerUserRepository
    {
        #region Constructor
        public CustomerUserRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor

        public List<CustomerUser> GetsByCustomer(Guid customerId)
        {
            return base.Find(x => x.CustomerId == customerId).ToList();
        }

        public override IEnumerable<CustomerUser> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<CustomerUser> customerUsers = new List<CustomerUser>();
            var entities = (from cu in Context.CustomerUsers
                            join c in Context.Customers on cu.CustomerId equals c.Id
                            join s in Context.Sites on c.Id equals s.CustomerId
                            where c.LastModifiedDate > lastSyncDate &&
                                  s.Id == siteId
                            select cu).ToList();

            foreach (var entity in entities)
            {
                CustomerUser customerUser = new CustomerUser()
                {
                    Id = entity.Id
                };

                customerUser.Update(entity);
                customerUsers.Add(customerUser);
            }

            return customerUsers;
        }
    }
}
