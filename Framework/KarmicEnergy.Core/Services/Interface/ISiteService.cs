using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarmicEnergy.Core.Services.Interface
{
    public interface ISiteService
    {
        Site Get(Guid id);
        IEnumerable<Site> Gets();
        IEnumerable<Site> GetsByCustomer(Guid customerId);
    }
}
