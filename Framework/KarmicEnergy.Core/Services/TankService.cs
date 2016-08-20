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
                throw new ArgumentNullException(String.Format(Resources.ResultResource.ParameterNull, "Tank"));

            this._unitOfWork.TankRepository.Add(tank);
            this._unitOfWork.Complete();
        }

        public override void Update(Tank tank)
        {
            if (tank == null)
                throw new ArgumentNullException(String.Format(Resources.ResultResource.ParameterNull, "Tank"));

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
                throw new ArgumentNullException(String.Format(Resources.ResultResource.ParameterRequired, "id"));

            throw new ArgumentException("id is required");

            var deletedDate = DateTime.UtcNow;
            var tank = this._unitOfWork.TankRepository.Get(id);
            tank.DeletedDate = deletedDate;

            // Sensors
            foreach (var sensor in tank.Sensors)
            {
                sensor.DeletedDate = deletedDate;

                // Sensor Items
                foreach (var sensorItem in sensor.SensorItems)
                {
                    sensorItem.DeletedDate = deletedDate;

                    // Triggers
                    var triggers = this._unitOfWork.TriggerRepository.Find(x => x.SensorItemId == sensorItem.Id && x.DeletedDate == null);
                    foreach (var trigger in triggers)
                    {
                        trigger.DeletedDate = deletedDate;

                        // Trigger Contacts
                        var contacts = this._unitOfWork.TriggerContactRepository.Find(x => x.TriggerId == trigger.Id && x.DeletedDate == null);
                        foreach (var contact in contacts)
                        {
                            contact.DeletedDate = deletedDate;
                            this._unitOfWork.TriggerContactRepository.Update(contact);
                        }

                        this._unitOfWork.TriggerRepository.Update(trigger);
                    }

                    this._unitOfWork.SensorItemRepository.Update(sensorItem);
                }
            }

            this._unitOfWork.TankRepository.Update(tank);
            this._unitOfWork.Complete();
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
                throw new ArgumentNullException(String.Format(Resources.ResultResource.ParameterRequired, "id"));

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
                throw new ArgumentNullException(String.Format(Resources.ResultResource.ParameterRequired, "customerId"));

            return this._unitOfWork.TankRepository.GetsByCustomer(customerId);
        }

        public IEnumerable<Tank> GetsBySite(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentNullException(String.Format(Resources.ResultResource.ParameterRequired, "siteId"));

            return this._unitOfWork.TankRepository.GetsBySite(siteId);
        }

        public IEnumerable<Tank> GetsBySiteWithTankModel(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentNullException(String.Format(Resources.ResultResource.ParameterRequired, "siteId"));

            return this._unitOfWork.TankRepository.Find(x => x.SiteId == siteId && x.DeletedDate == null, "TankModel").ToList();
        }

        public IEnumerable<Tank> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            if (customerId == default(Guid))
                throw new ArgumentNullException(String.Format(Resources.ResultResource.ParameterRequired, "customerId"));

            if (siteId == default(Guid))
                throw new ArgumentNullException(String.Format(Resources.ResultResource.ParameterRequired, "siteId"));

            return this._unitOfWork.TankRepository.GetsByCustomerAndSite(customerId, siteId);
        }

        public IEnumerable<Tank> GetsBySiteWithAlarms(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentNullException(String.Format(Resources.ResultResource.ParameterRequired, "siteId"));

            return this._unitOfWork.TankRepository.Find(x => x.SiteId == siteId, "Sensors").ToList();
        }

        public IEnumerable<Tank> GetsBySiteWithWaterVolume(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentNullException(String.Format(Resources.ResultResource.ParameterRequired, "siteId"));

            return this._unitOfWork.TankRepository.Find(x => x.SiteId == siteId, "Sensors").ToList();
        }

        #endregion Functions
    }
}
