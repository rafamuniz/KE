using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class CustomerRepository : KERepositoryBase<Customer>, ICustomerRepository
    {
        #region Constructor
        public CustomerRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor 

        public override IEnumerable<Customer> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<Customer> customers = new List<Customer>();
            var entities = (from c in Context.Customers
                            join s in Context.Sites on c.Id equals s.CustomerId
                            where c.LastModifiedDate > lastSyncDate &&
                                  s.Id == siteId
                            select c).ToList();

            foreach (var entity in entities)
            {
                Customer customer = new Customer()
                {
                    Id = entity.Id
                };

                customer.Update(entity);
                customers.Add(customer);
            }

            return customers;
        }
    }
}
