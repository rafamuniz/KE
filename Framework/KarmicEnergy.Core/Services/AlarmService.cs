using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class AlarmService : KEServiceBase<Guid, Alarm>, IAlarmService
    {
        #region Constructor

        public AlarmService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override Alarm Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.AlarmRepository.Get(id);
        }

        public override IEnumerable<Alarm> GetAll()
        {
            return this._unitOfWork.AlarmRepository.GetAll();
        }

        public override void Delete(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var alarm = this._unitOfWork.AlarmRepository.Get(id);
            alarm.DeletedDate = DateTime.UtcNow;
            this._unitOfWork.AlarmRepository.Update(alarm);            
        }

        public void Acknowledge(Guid alarmId, Guid userId, String username)
        {
            var alarm = this._unitOfWork.AlarmRepository.Get(alarmId);

            alarm.LastAckDate = DateTime.UtcNow;
            alarm.LastAckUserId = userId;
            alarm.LastAckUserName = username;

            AlarmHistory alarmHistory = new AlarmHistory()
            {
                UserId = userId,
                UserName = alarm.LastAckUserName,
                ActionTypeId = (Int16)ActionTypeEnum.Ack,
                AlarmId = alarm.Id,
                Value = alarm.Value,
            };

            this._unitOfWork.AlarmHistoryRepository.Add(alarmHistory);
            this._unitOfWork.AlarmRepository.Update(alarm);
            this._unitOfWork.Complete();
        }

        public void Clear(Guid alarmId, Guid userId, String message)
        {
            var alarm = this._unitOfWork.AlarmRepository.Get(alarmId);

            alarm.EndDate = DateTime.UtcNow;

            AlarmHistory alarmHistory = new AlarmHistory()
            {
                UserId = userId,
                ActionTypeId = (Int16)ActionTypeEnum.Clear,
                Message = message,
                AlarmId = alarm.Id,
                Value = alarm.Value
            };

            this._unitOfWork.AlarmHistoryRepository.Add(alarmHistory);
            this._unitOfWork.AlarmRepository.Update(alarm);
            this._unitOfWork.Complete();
        }

        public IEnumerable<Alarm> GetsBySite(Guid siteId)
        {
            return this._unitOfWork.AlarmRepository.GetsBySite(siteId).ToList();
        }

        public IEnumerable<Alarm> GetsBySiteWithTrigger(Guid siteId)
        {
            return this._unitOfWork.AlarmRepository.GetsBySite(siteId, "Trigger", "Trigger.SensorItem", "Trigger.Severity", "Trigger.SensorItem.Sensor", "Trigger.SensorItem.Unit", "Trigger.SensorItem.Sensor.Tank", "Trigger.SensorItem.Sensor.Tank.Site", "Trigger.SensorItem.Item").ToList();
        }

        public IEnumerable<Alarm> GetsByPond(Guid pondId)
        {
            return this._unitOfWork.AlarmRepository.GetsByPond(pondId).ToList();
        }

        public IEnumerable<Alarm> GetsByPondWithTrigger(Guid pondId)
        {
            return this._unitOfWork.AlarmRepository.GetsByPond(pondId, "Trigger", "Trigger.SensorItem", "Trigger.Severity", "Trigger.SensorItem.Sensor", "Trigger.SensorItem.Unit", "Trigger.SensorItem.Sensor.Tank", "Trigger.SensorItem.Sensor.Tank.Site", "Trigger.SensorItem.Item").ToList();
        }

        public IEnumerable<Alarm> GetsByTank(Guid tankId)
        {
            return this._unitOfWork.AlarmRepository.GetsByTank(tankId).ToList();
        }

        public IEnumerable<Alarm> GetsByTankWithTrigger(Guid tankId)
        {
            return this._unitOfWork.AlarmRepository.GetsByTank(tankId, "Trigger", "Trigger.SensorItem", "Trigger.Severity", "Trigger.SensorItem.Sensor", "Trigger.SensorItem.Unit", "Trigger.SensorItem.Sensor.Tank", "Trigger.SensorItem.Sensor.Tank.Site", "Trigger.SensorItem.Item").ToList();
        }

        public IEnumerable<Alarm> GetsBySensor(Guid sensorId)
        {
            return this._unitOfWork.AlarmRepository.GetsBySensor(sensorId).ToList();
        }

        public IEnumerable<Alarm> GetsBySensorWithTrigger(Guid sensorId)
        {
            return this._unitOfWork.AlarmRepository.GetsBySensor(sensorId, "Trigger", "Trigger.SensorItem", "Trigger.Severity", "Trigger.SensorItem.Sensor", "Trigger.SensorItem.Unit", "Trigger.SensorItem.Sensor.Tank", "Trigger.SensorItem.Sensor.Tank.Site", "Trigger.SensorItem.Item").ToList();
        }

        #endregion Functions
    }
}
