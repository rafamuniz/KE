using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class AlarmService : KEServiceBase, IAlarmService
    {
        #region Constructor

        public AlarmService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public Alarm Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._UnitOfWork.AlarmRepository.Get(id);
        }

        public IList<Alarm> Gets()
        {
            return this._UnitOfWork.AlarmRepository.GetAll().ToList();
        }
        
        #endregion Functions
    }
}
