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

            var deletedDate = DateTime.UtcNow;
            var site = this._unitOfWork.SiteRepository.Get(siteId);

            // Site
            site.DeletedDate = deletedDate;
            this._unitOfWork.SiteRepository.Update(site);

            // Address
            site.Address.DeletedDate = deletedDate;
            this._unitOfWork.AddressRepository.Update(site.Address);

            #region Sensor
            var sensors = this._unitOfWork.SensorRepository.GetsBySite(site.Id);
            foreach (var sensor in sensors)
            {
                sensor.DeletedDate = deletedDate;
                this._unitOfWork.SensorRepository.Update(sensor);

                // Sensor Items
                foreach (var sensorItem in sensor.SensorItems)
                {
                    sensorItem.DeletedDate = deletedDate;
                    this._unitOfWork.SensorItemRepository.Update(sensorItem);

                    // Triggers
                    var triggers = this._unitOfWork.TriggerRepository.Find(x => x.SensorItemId == sensorItem.Id && x.DeletedDate == null);
                    foreach (var trigger in triggers)
                    {
                        trigger.DeletedDate = deletedDate;
                        this._unitOfWork.TriggerRepository.Update(trigger);

                        // Trigger Contacts
                        var contacts = this._unitOfWork.TriggerContactRepository.Find(x => x.TriggerId == trigger.Id && x.DeletedDate == null);
                        foreach (var contact in contacts)
                        {
                            contact.DeletedDate = deletedDate;
                            this._unitOfWork.TriggerContactRepository.Update(contact);
                        }
                    }
                }
            }
            #endregion Sensor

            #region Pond
            var ponds = this._unitOfWork.PondRepository.GetsBySite(site.Id);
            foreach (var pond in ponds)
            {
                pond.DeletedDate = deletedDate;
                this._unitOfWork.PondRepository.Update(pond);

                // Sensors
                foreach (var sensor in pond.Sensors)
                {
                    sensor.DeletedDate = deletedDate;
                    this._unitOfWork.SensorRepository.Update(sensor);

                    // Sensor Items
                    foreach (var sensorItem in sensor.SensorItems)
                    {
                        sensorItem.DeletedDate = deletedDate;
                        this._unitOfWork.SensorItemRepository.Update(sensorItem);

                        // Triggers
                        var triggers = this._unitOfWork.TriggerRepository.Find(x => x.SensorItemId == sensorItem.Id && x.DeletedDate == null);
                        foreach (var trigger in triggers)
                        {
                            trigger.DeletedDate = deletedDate;
                            this._unitOfWork.TriggerRepository.Update(trigger);

                            // Trigger Contacts
                            var contacts = this._unitOfWork.TriggerContactRepository.Find(x => x.TriggerId == trigger.Id && x.DeletedDate == null);
                            foreach (var contact in contacts)
                            {
                                contact.DeletedDate = deletedDate;
                                this._unitOfWork.TriggerContactRepository.Update(contact);
                            }
                        }
                    }
                }
            }

            #endregion Pond

            #region Tank

            var tanks = this._unitOfWork.TankRepository.GetsBySite(site.Id);
            foreach (var tank in tanks)
            {
                tank.DeletedDate = deletedDate;
                this._unitOfWork.TankRepository.Update(tank);

                // Sensors
                foreach (var sensor in tank.Sensors)
                {
                    sensor.DeletedDate = deletedDate;
                    this._unitOfWork.SensorRepository.Update(sensor);

                    // Sensor Items
                    foreach (var sensorItem in sensor.SensorItems)
                    {
                        sensorItem.DeletedDate = deletedDate;
                        this._unitOfWork.SensorItemRepository.Update(sensorItem);

                        // Triggers
                        var triggers = this._unitOfWork.TriggerRepository.Find(x => x.SensorItemId == sensorItem.Id && x.DeletedDate == null);
                        foreach (var trigger in triggers)
                        {
                            trigger.DeletedDate = deletedDate;
                            this._unitOfWork.TriggerRepository.Update(trigger);

                            // Trigger Contacts
                            var contacts = this._unitOfWork.TriggerContactRepository.Find(x => x.TriggerId == trigger.Id && x.DeletedDate == null);
                            foreach (var contact in contacts)
                            {
                                contact.DeletedDate = deletedDate;
                                this._unitOfWork.TriggerContactRepository.Update(contact);
                            }
                        }
                    }
                }
            }

            #endregion Tank

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

        public IEnumerable<Site> GetAllWithLocation()
        {
            return this._unitOfWork.SiteRepository.GetAll().Where(x => !String.IsNullOrEmpty(x.Latitude) && !String.IsNullOrEmpty(x.Longitude) && x.Status == "A" && x.DeletedDate == null);
        }

        public IEnumerable<Site> GetsByCustomer(Guid customerId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            return this._unitOfWork.SiteRepository.GetsByCustomer(customerId);
        }

        public IEnumerable<Site> GetsByCustomerWithLocation(Guid customerId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            return this._unitOfWork.SiteRepository.GetsByCustomer(customerId).Where(x => !String.IsNullOrEmpty(x.Latitude) && !String.IsNullOrEmpty(x.Longitude) && x.Status == "A" && x.DeletedDate == null);
        }

        public IEnumerable<Site> GetsSiteByUser(Guid userId)
        {
            if (userId == default(Guid))
                throw new ArgumentException("userId is required");

            return this._unitOfWork.CustomerUserSiteRepository.GetsByUser(userId).Select(x => x.Site);

        }

        public IEnumerable<Site> GetsSiteByUserWithLocation(Guid userId)
        {
            if (userId == default(Guid))
                throw new ArgumentException("userId is required");

            return this._unitOfWork.CustomerUserSiteRepository.GetsByUser(userId)
                .Where(x => !String.IsNullOrEmpty(x.Site.Latitude) && !String.IsNullOrEmpty(x.Site.Longitude) && x.Site.Status == "A" && x.DeletedDate == null)
                .Select(x => x.Site);
        }
        #endregion Functions
    }
}
