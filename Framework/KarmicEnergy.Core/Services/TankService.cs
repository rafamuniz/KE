using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class TankService : KEServiceBase, ITankService
    {
        #region Constructor

        public TankService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public Tank Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._UnitOfWork.TankRepository.Get(id);
        }

        public IEnumerable<Tank> Gets()
        {
            return this._UnitOfWork.TankRepository.GetAll();
        }

        public IEnumerable<Tank> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._UnitOfWork.TankRepository.GetsByCustomerAndSite(customerId, siteId);
        }
        
        #endregion Functions
    }
}
