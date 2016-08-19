using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class SensorService : KEServiceBase<Guid, Sensor>, ISensorService
    {
        #region Constructor

        public SensorService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override Sensor Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.SensorRepository.Get(id);
        }

        public override  IEnumerable<Sensor> GetAll()
        {
            return this._unitOfWork.SensorRepository.GetAll();
        }

        public Boolean HasSensorSite(Guid siteId)
        {
            return this._unitOfWork.SensorRepository.HasSensorSite(siteId);
        }

        public Boolean HasSensorTank(Guid tankId)
        {
            return this._unitOfWork.SensorRepository.HasSensorTank(tankId);
        }

        public Boolean HasSensorPond(Guid pondId)
        {
            return this._unitOfWork.SensorRepository.HasSensorPond(pondId);
        }

        public IEnumerable<Sensor> GetsBySite(Guid siteId)
        {
            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._unitOfWork.SensorRepository.GetsBySite(siteId);
        }

        public IEnumerable<Sensor> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            if (customerId == default(Guid))
                throw new ArgumentException("customerId is required");

            if (siteId == default(Guid))
                throw new ArgumentException("siteId is required");

            return this._unitOfWork.SensorRepository.GetsByCustomerAndSite(customerId, siteId);
        }

        #endregion Functions
    }
}
