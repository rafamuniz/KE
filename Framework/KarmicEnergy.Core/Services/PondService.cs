﻿using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using Munizoft.Extensions;

namespace KarmicEnergy.Core.Services
{
    public class PondService : KEServiceBase<Guid, Pond>, IPondService
    {
        #region Constructor

        public PondService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override void Create(Pond pond)
        {
            if (pond == null)
                throw new ArgumentNullException("pond cannot be null");

            this._unitOfWork.PondRepository.Add(pond);
            this._unitOfWork.Complete();
        }

        public override void Update(Pond pond)
        {
            if (pond == null)
                throw new ArgumentNullException("pond cannot be null");

            var entity = this._unitOfWork.PondRepository.Get(pond.Id);

            entity.Update(pond);

            var dateUpdated = DateTime.UtcNow;
            entity.LastModifiedDate = dateUpdated;

            this._unitOfWork.PondRepository.Update(entity);
            this._unitOfWork.Complete();
        }

        public override void Delete(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var deletedDate = DateTime.UtcNow;
            var pond = this._unitOfWork.PondRepository.Get(id);
            pond.DeletedDate = deletedDate;

            // Sensors
            foreach (var sensor in pond.Sensors)
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

                this._unitOfWork.SensorRepository.Update(sensor);
            }

            this._unitOfWork.PondRepository.Update(pond);
            this._unitOfWork.Complete();
        }

        public override Pond Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.PondRepository.Get(id);
        }

        public override IEnumerable<Pond> GetAll()
        {
            return this._unitOfWork.PondRepository.GetAll();
        }

        public IEnumerable<Pond> GetsByCustomer(Guid customerId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            return this._unitOfWork.PondRepository.GetsByCustomer(customerId);
        }

        public IEnumerable<Pond> GetsBySite(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._unitOfWork.PondRepository.GetsBySite(siteId);
        }

        public IEnumerable<Pond> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._unitOfWork.PondRepository.GetsByCustomerAndSite(customerId, siteId);
        }

        public IEnumerable<Pond> GetsBySiteWithAlarms(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._unitOfWork.PondRepository.Find(x => x.SiteId == siteId, "Sensors");
        }

        #endregion Functions
    }
}
