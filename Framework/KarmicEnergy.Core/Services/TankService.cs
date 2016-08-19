using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using Munizoft.Extensions;

namespace KarmicEnergy.Core.Services
{
    public class TankService : KEServiceBase<Guid, Tank>, ITankService
    {
        #region Constructor

        public TankService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override void Create(Tank tank)
        {
            if (tank == null)
                throw new ArgumentNullException("tank cannot be null");

            this._unitOfWork.TankRepository.Add(tank);
            this._unitOfWork.Complete();
        }

        public override void Update(Tank tank)
        {
            if (tank == null)
                throw new ArgumentNullException("tank cannot be null");

            var entity = this._unitOfWork.TankRepository.Get(tank.Id);

            entity.Update(tank);

            var dateUpdated = DateTime.UtcNow;
            entity.LastModifiedDate = dateUpdated;

            this._unitOfWork.TankRepository.Update(entity);
            this._unitOfWork.Complete();
        }

        public override void Delete(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var deletedDate = DateTime.UtcNow;
            var tank = this._unitOfWork.TankRepository.Get(id);
            tank.DeletedDate = deletedDate;

            // Sensor
            var sensors = this._unitOfWork.SensorRepository.GetsByTank(tank.Id);
            sensors.ForEach(x => x.DeletedDate = deletedDate);
            this._unitOfWork.SensorRepository.UpdateRange(sensors);

            this._unitOfWork.TankRepository.Update(tank);
        }

        public override Tank Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.TankRepository.Get(id);
        }

        public Tank Get(Guid id, params String[] includes)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var tanks = this._unitOfWork.TankRepository.Find(x => x.Id == id, includes).ToList();

            return tanks.Single();
        }

        public override IEnumerable<Tank> GetAll()
        {
            return this._unitOfWork.TankRepository.GetAll();
        }

        public IEnumerable<Tank> GetsByCustomer(Guid customerId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            return this._unitOfWork.TankRepository.GetsByCustomer(customerId);
        }

        public IEnumerable<Tank> GetsBySite(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._unitOfWork.TankRepository.GetsBySite(siteId);
        }

        public IEnumerable<Tank> GetsBySiteWithTankModel(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._unitOfWork.TankRepository.Find(x => x.SiteId == siteId && x.DeletedDate == null, "TankModel").ToList();
        }

        public IEnumerable<Tank> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._unitOfWork.TankRepository.GetsByCustomerAndSite(customerId, siteId);
        }

        public IEnumerable<Tank> GetsBySiteWithAlarms(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._unitOfWork.TankRepository.Find(x => x.SiteId == siteId, "Sensors").ToList();
        }

        public IEnumerable<Tank> GetsBySiteWithWaterVolume(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._unitOfWork.TankRepository.Find(x => x.SiteId == siteId, "Sensors").ToList();
        }

        #endregion Functions
    }
}
