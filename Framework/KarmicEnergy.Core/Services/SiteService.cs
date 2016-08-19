using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Repositories;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Munizoft.Extensions;

namespace KarmicEnergy.Core.Services
{
    public class SiteService : KEServiceBase<Guid, Site>, ISiteService
    {
        #region Fields
        protected readonly ISiteRepository _repository;
        #endregion Fields

        #region Constructor
        public SiteService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #endregion Constructor

        #region Functions

        public override void Create(Site site)
        {
            if (site == null)
                throw new ArgumentNullException("site cannot be null");
            
            this._unitOfWork.SiteRepository.Add(site);
            this._unitOfWork.Complete();
        }

        public override void Update(Site site)
        {
            if (site == null)
                throw new ArgumentNullException("site cannot be null");

            var entity = this._unitOfWork.SiteRepository.Get(site.Id);

            entity.Update(site);

            var dateUpdated = DateTime.UtcNow;
            entity.LastModifiedDate = dateUpdated;
            entity.Address.LastModifiedDate = dateUpdated;

            this._unitOfWork.SiteRepository.Update(entity);
            this._unitOfWork.Complete();
        }

        public override void Delete(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            var dateDeleted = DateTime.UtcNow;
            var site = this._unitOfWork.SiteRepository.Get(siteId);

            // Site
            site.DeletedDate = dateDeleted;
            this._unitOfWork.SiteRepository.Update(site);

            // Address
            site.Address.DeletedDate = dateDeleted;
            this._unitOfWork.AddressRepository.Update(site.Address);

            // Sensor
            var sensors = this._unitOfWork.SensorRepository.GetsBySite(site.Id);
            sensors.ForEach(x => x.DeletedDate = dateDeleted);
            this._unitOfWork.SensorRepository.UpdateRange(sensors);

            // Pond
            var ponds = this._unitOfWork.PondRepository.GetsBySite(site.Id);
            ponds.ForEach(x => x.DeletedDate = dateDeleted);
            this._unitOfWork.PondRepository.UpdateRange(ponds);

            // Tank             
            var tanks = this._unitOfWork.TankRepository.GetsBySite(site.Id);
            tanks.ForEach(x => x.DeletedDate = dateDeleted);
            this._unitOfWork.TankRepository.UpdateRange(tanks);

            this._unitOfWork.Complete();
        }

        public override Site Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.SiteRepository.Get(id);
        }

        public override IEnumerable<Site> GetAll()
        {
            return this._unitOfWork.SiteRepository.GetAll();
        }

        public IEnumerable<Site> GetsByCustomer(Guid customerId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            return this._unitOfWork.SiteRepository.GetsByCustomer(customerId);
        }

        public IEnumerable<Site> GetsSiteByUser(Guid userId)
        {
            if (userId == default(Guid))
                throw new ArgumentException("userId is required");

            return this._unitOfWork.CustomerUserSiteRepository.GetsByUser(userId).Select(x => x.Site);

        }
        #endregion Functions
    }
}
