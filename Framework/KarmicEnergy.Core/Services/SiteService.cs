using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarmicEnergy.Core.Services
{
    public class SiteService : KEServiceBase, ISiteService
    {
        #region Constructor

        public SiteService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public Site Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._UnitOfWork.SiteRepository.Get(id);
        }

        public IEnumerable<Site> Gets()
        {
            return this._UnitOfWork.SiteRepository.GetAll();
        }

        public IEnumerable<Site> GetsByCustomer(Guid customerId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            return this._UnitOfWork.SiteRepository.GetsByCustomer(customerId);
        }
        #endregion Functions
    }
}
