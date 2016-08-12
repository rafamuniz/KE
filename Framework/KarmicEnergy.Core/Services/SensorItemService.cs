using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class SensorItemService : KEServiceBase, ISensorItemService
    {
        #region Constructor

        public SensorItemService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public SensorItem Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._UnitOfWork.SensorItemRepository.Get(id);
        }

        public IList<SensorItem> Gets()
        {
            return this._UnitOfWork.SensorItemRepository.GetAll().ToList();
        }

        public Boolean HasSiteSensorItem(Guid siteId, ItemEnum item)
        {
            return this._UnitOfWork.SensorItemRepository.HasSiteSensorItem(siteId, item);
        }

        public Boolean HasPondSensorItem(Guid pondId, ItemEnum item)
        {
            return this._UnitOfWork.SensorItemRepository.HasPondSensorItem(pondId, item);
        }

        public Boolean HasTankSensorItem(Guid tankId, ItemEnum item)
        {
            return this._UnitOfWork.SensorItemRepository.HasTankSensorItem(tankId, item);
        }

        public Boolean HasSensorSensorItem(Guid sensorId, ItemEnum item)
        {
            return this._UnitOfWork.SensorItemRepository.HasSensorSensorItem(sensorId, item);
        }

        #endregion Functions
    }
}
