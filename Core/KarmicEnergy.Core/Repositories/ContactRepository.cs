using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class ContactRepository : Repository<Contact, KEContext>, IContactRepository
    {
        #region Constructor
        public ContactRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
