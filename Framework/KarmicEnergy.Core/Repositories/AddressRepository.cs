using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class AddressRepository : KERepositoryBase<Address>, IAddressRepository
    {
        #region Constructor
        public AddressRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor  

        public override IEnumerable<Address> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<Address> addresses = new List<Address>();

            // Sites
            var sites = (from s in Context.Sites
                         join a in Context.Addresses on s.AddressId equals a.Id
                         where a.LastModifiedDate > lastSyncDate &&
                               s.Id == siteId
                         select a).ToList();

            // Customer Users
            var customerUsers = (from cu in Context.CustomerUsers
                                 join c in Context.Customers on cu.CustomerId equals c.Id
                                 join s in Context.Sites on c.Id equals s.CustomerId
                                 join a in Context.Addresses on cu.AddressId equals a.Id
                                 where a.LastModifiedDate > lastSyncDate &&
                                        s.Id == siteId
                                 select a).ToList();

            // Contacts
            var contacts = (from co in Context.Contacts
                            join c in Context.Customers on co.CustomerId equals c.Id
                            join s in Context.Sites on c.Id equals s.CustomerId
                            join a in Context.Addresses on co.AddressId equals a.Id
                            where a.LastModifiedDate > lastSyncDate &&
                                    s.Id == siteId
                            select a).ToList();

            // Customers
            var customers = (from c in Context.Customers
                             join s in Context.Sites on c.Id equals s.CustomerId
                             join a in Context.Addresses on c.AddressId equals a.Id
                             where a.LastModifiedDate > lastSyncDate &&
                                    s.Id == siteId
                             select a).ToList();

            addresses.AddRange(sites);
            addresses.AddRange(customerUsers);
            addresses.AddRange(contacts);
            addresses.AddRange(customers);

            return addresses;
        }
    }
}
