using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class AlarmHistoryService : KEServiceBase<Guid, AlarmHistory>, IAlarmHistoryService
    {
        #region Constructor

        public AlarmHistoryService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override AlarmHistory Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.AlarmHistoryRepository.Get(id);
        }

        public override IEnumerable<AlarmHistory> GetAll()
        {
            return this._unitOfWork.AlarmHistoryRepository.GetAll();
        }

        public override void Delete(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var alarm = this._unitOfWork.AlarmHistoryRepository.Get(id);
            alarm.DeletedDate = DateTime.UtcNow;
            this._unitOfWork.AlarmHistoryRepository.Update(alarm);            
        }

        public AlarmHistory CreateComment(AlarmHistory alarmHistory)
        {
            var alarm = this._unitOfWork.AlarmRepository.Get(alarmHistory.AlarmId);

            AlarmHistory history = new AlarmHistory()
            {
                UserId = alarmHistory.UserId,
                UserName = alarmHistory.UserName,
                ActionTypeId = (Int16)ActionTypeEnum.Comment,
                Message = alarmHistory.Message,
                AlarmId = alarm.Id,
                Value = alarm.Value
            };

            this._unitOfWork.AlarmHistoryRepository.Add(history);
            this._unitOfWork.Complete();

            return history;
        }
               
        #endregion Functions
    }
}
