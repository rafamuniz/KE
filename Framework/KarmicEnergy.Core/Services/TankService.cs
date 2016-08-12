using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
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

        public Tank Get(Guid id, params String[] includes)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var tanks = this._UnitOfWork.TankRepository.Find(x => x.Id == id, includes).ToList();

            return tanks.Single();
        }

        public IList<Tank> Gets()
        {
            return this._UnitOfWork.TankRepository.GetAll().ToList();
        }
        public IList<Tank> GetsByCustomer(Guid customerId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            return this._UnitOfWork.TankRepository.GetsByCustomer(customerId);
        }

        public IList<Tank> GetsBySite(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._UnitOfWork.TankRepository.GetsBySite(siteId);
        }

        public IList<Tank> GetsBySiteWithTankModel(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._UnitOfWork.TankRepository.Find(x => x.SiteId == siteId && x.DeletedDate == null, "TankModel").ToList();
        }

        public IList<Tank> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._UnitOfWork.TankRepository.GetsByCustomerAndSite(customerId, siteId);
        }

        public IList<Tank> GetsBySiteWithAlarms(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._UnitOfWork.TankRepository.Find(x => x.SiteId == siteId, "Sensors").ToList();
        }

        public IList<Tank> GetsBySiteWithWaterVolume(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._UnitOfWork.TankRepository.Find(x => x.SiteId == siteId, "Sensors").ToList();
        }

        #endregion Functions
    }
}
