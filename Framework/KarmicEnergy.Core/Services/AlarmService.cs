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

        public List<Alarm> Gets()
        {
            return this._UnitOfWork.AlarmRepository.GetAll().ToList();
        }

        public List<Alarm> GetsBySite(Guid siteId)
        {
            return this._UnitOfWork.AlarmRepository.GetsBySite(siteId).ToList();
        }

        public List<Alarm> GetsBySiteWithTrigger(Guid siteId)
        {
            return this._UnitOfWork.AlarmRepository.GetsBySite(siteId, "Trigger", "Trigger.SensorItem", "Trigger.Severity", "Trigger.SensorItem.Sensor", "Trigger.SensorItem.Unit", "Trigger.SensorItem.Sensor.Tank", "Trigger.SensorItem.Sensor.Tank.Site", "Trigger.SensorItem.Item").ToList();
        }

        public List<Alarm> GetsByPond(Guid pondId)
        {
            return this._UnitOfWork.AlarmRepository.GetsByPond(pondId).ToList();
        }

        public List<Alarm> GetsByPondWithTrigger(Guid pondId)
        {
            return this._UnitOfWork.AlarmRepository.GetsByPond(pondId, "Trigger", "Trigger.SensorItem", "Trigger.Severity", "Trigger.SensorItem.Sensor", "Trigger.SensorItem.Unit", "Trigger.SensorItem.Sensor.Tank", "Trigger.SensorItem.Sensor.Tank.Site", "Trigger.SensorItem.Item").ToList();
        }

        public List<Alarm> GetsByTank(Guid tankId)
        {
            return this._UnitOfWork.AlarmRepository.GetsByTank(tankId).ToList();
        }

        public List<Alarm> GetsByTankWithTrigger(Guid tankId)
        {
            return this._UnitOfWork.AlarmRepository.GetsByTank(tankId, "Trigger", "Trigger.SensorItem", "Trigger.Severity", "Trigger.SensorItem.Sensor", "Trigger.SensorItem.Unit", "Trigger.SensorItem.Sensor.Tank", "Trigger.SensorItem.Sensor.Tank.Site", "Trigger.SensorItem.Item").ToList();
        }

        public List<Alarm> GetsBySensor(Guid sensorId)
        {
            return this._UnitOfWork.AlarmRepository.GetsBySensor(sensorId).ToList();
        }

        #endregion Functions
    }
}
