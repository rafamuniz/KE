using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class CustomerService : KEServiceBase<Guid, Customer>, ICustomerService
    {
        #region Constructor

        public CustomerService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override Customer Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.CustomerRepository.Get(id);
        }

        public override IEnumerable<Customer> GetAll()
        {
            return this._unitOfWork.CustomerRepository.GetAll();
        }

        public override void Delete(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var deletedDate = DateTime.UtcNow;

            var customer = this._unitOfWork.CustomerRepository.Get(id);
            customer.DeletedDate = deletedDate;
            this._unitOfWork.CustomerRepository.Update(customer);

            // Address
            customer.Address.DeletedDate = deletedDate;
            this._unitOfWork.AddressRepository.Update(customer.Address);

            #region CustomerUser
            var customerUsers = this._unitOfWork.CustomerUserRepository.Find(x => x.CustomerId == id && x.DeletedDate == null);
            foreach (var customerUser in customerUsers)
            {
                customerUser.DeletedDate = deletedDate;
                this._unitOfWork.CustomerUserRepository.Update(customerUser);
            }
            #endregion CustomerUser

            #region Contact
            var contacts = this._unitOfWork.ContactRepository.Find(x => x.CustomerId == id && x.DeletedDate == null);
            foreach (var contact in contacts)
            {
                contact.DeletedDate = deletedDate;
                this._unitOfWork.ContactRepository.Update(contact);
            }
            #endregion Contact

            #region Site
            var sites = this._unitOfWork.SiteRepository.Find(x => x.CustomerId == id && x.DeletedDate == null);
            foreach (var site in sites)
            {
                site.DeletedDate = deletedDate;
                this._unitOfWork.SiteRepository.Update(site);

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
                            var sensorContacts = this._unitOfWork.TriggerContactRepository.Find(x => x.TriggerId == trigger.Id && x.DeletedDate == null);
                            foreach (var contact in sensorContacts)
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
                                var pondContacts = this._unitOfWork.TriggerContactRepository.Find(x => x.TriggerId == trigger.Id && x.DeletedDate == null);
                                foreach (var contact in pondContacts)
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
                                var tankContacts = this._unitOfWork.TriggerContactRepository.Find(x => x.TriggerId == trigger.Id && x.DeletedDate == null);
                                foreach (var contact in tankContacts)
                                {
                                    contact.DeletedDate = deletedDate;
                                    this._unitOfWork.TriggerContactRepository.Update(contact);
                                }
                            }
                        }
                    }
                }

                #endregion Tank
            }
            #endregion Site

            this._unitOfWork.Complete();
        }
                
        #endregion Functions
    }
}
