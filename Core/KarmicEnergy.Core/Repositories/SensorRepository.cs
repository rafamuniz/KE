using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorRepository : Repository<Sensor, KEContext>, ISensorRepository
    {
        #region Constructor
        public SensorRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor       
        
        public List<Sensor> GetsByCustomerId(Guid customerId)
        {
            return base.Find(x => x.Tank.Site.CustomerId == customerId).ToList();
        }
    }
}
