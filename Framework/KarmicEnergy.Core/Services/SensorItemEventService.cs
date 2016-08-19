using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class SensorItemEventService : KEServiceBase<Guid, SensorItemEvent>, ISensorItemEventService
    {
        #region Constructor

        public SensorItemEventService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override SensorItemEvent Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.SensorItemEventRepository.Get(id);
        }

        public override IEnumerable<SensorItemEvent> GetAll()
        {
            return this._unitOfWork.SensorItemEventRepository.GetAll().ToList();
        }

        public SensorItemEvent GetLastEventByTankAndItem(Guid tankId, ItemEnum item)
        {
            return this._unitOfWork.SensorItemEventRepository.GetLastEventByTankAndItem(tankId, item);
        }

        #endregion Functions
    }
}
