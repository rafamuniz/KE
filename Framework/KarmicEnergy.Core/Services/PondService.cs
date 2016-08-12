using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class PondService : KEServiceBase, IPondService
    {
        #region Constructor

        public PondService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public Pond Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._UnitOfWork.PondRepository.Get(id);
        }

        public IList<Pond> Gets()
        {
            return this._UnitOfWork.PondRepository.GetAll().ToList();
        }

        public IList<Pond> GetsByCustomer(Guid customerId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            return this._UnitOfWork.PondRepository.GetsByCustomer(customerId);
        }

        public IList<Pond> GetsBySite(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._UnitOfWork.PondRepository.GetsBySite(siteId);
        }

        public IList<Pond> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._UnitOfWork.PondRepository.GetsByCustomerAndSite(customerId, siteId);
        }

        public IList<Pond> GetsBySiteWithAlarms(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._UnitOfWork.PondRepository.Find(x => x.SiteId == siteId, "Sensors").ToList();
        }

        #endregion Functions
    }
}
