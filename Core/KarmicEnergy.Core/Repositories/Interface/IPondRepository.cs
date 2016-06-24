using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public interface IPondRepository : IKERepositoryBase<Pond>
    {
        //List<Tank> GetsByCustomerId(Guid customerId);
        //List<Tank> GetsByCustomerIdAndSiteId(Guid customerId, Guid siteId);
        //List<Models.TankModel> GetsWithLastMeasurement(Guid customerId);
    }
}
