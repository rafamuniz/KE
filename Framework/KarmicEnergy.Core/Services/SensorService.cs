using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class SensorService : KEServiceBase, ISensorService
    {
        #region Constructor

        public SensorService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public Sensor Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._UnitOfWork.SensorRepository.Get(id);
        }

        public IEnumerable<Sensor> Gets()
        {
            return this._UnitOfWork.SensorRepository.GetAll();
        }

        public Boolean HasSensorSite(Guid siteId)
        {
            return this._UnitOfWork.SensorRepository.HasSensorSite(siteId);
        }

        public Boolean HasSensorTank(Guid tankId)
        {
            return this._UnitOfWork.SensorRepository.HasSensorTank(tankId);
        }

        public Boolean HasSensorPond(Guid pondId)
        {
            return this._UnitOfWork.SensorRepository.HasSensorPond(pondId);
        }

        public IEnumerable<Sensor> GetsBySite(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._UnitOfWork.SensorRepository.GetsBySite(siteId);
        }

        public IEnumerable<Sensor> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._UnitOfWork.SensorRepository.GetsByCustomerAndSite(customerId, siteId);
        }





        #endregion Functions
    }
}
