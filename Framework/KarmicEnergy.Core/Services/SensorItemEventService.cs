using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class SensorItemEventService : KEServiceBase, ISensorItemEventService
    {
        #region Constructor

        public SensorItemEventService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public SensorItemEvent Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._UnitOfWork.SensorItemEventRepository.Get(id);
        }

        public IList<SensorItemEvent> Gets()
        {
            return this._UnitOfWork.SensorItemEventRepository.GetAll().ToList();
        }

        public SensorItemEvent GetLastEventByTankAndItem(Guid tankId, ItemEnum item)
        {
            return this._UnitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tankId, item);
        }

        #endregion Functions
    }
}
