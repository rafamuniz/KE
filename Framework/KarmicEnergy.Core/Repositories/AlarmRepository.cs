using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class AlarmRepository : KERepositoryBase<Alarm>, IAlarmRepository
    {
        #region Constructor
        public AlarmRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor 

        #region Open

        /// <summary>
        /// Gets all Open
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpen()
        {
            return this.GetsOpen(null);
        }

        /// <summary>
        /// Gets all Open
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpen(params String[] includes)
        {
            return base.Find(x => x.EndDate == null, includes);
        }

        /// <summary>
        /// Gets all Alarms Open by CustomerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpenByCustomer(Guid customerId)
        {
            return GetsOpenByCustomer(customerId, null);
        }

        /// <summary>
        /// Gets all Alarms Open by CustomerId
        /// Including Entities
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpenByCustomer(Guid customerId, params String[] includes)
        {
            List<Alarm> alarms = new List<Alarm>();

            var alarmsSite = base.Find(x => x.Trigger.SensorItem.Sensor.Site.CustomerId == customerId && x.EndDate == null, includes).ToList();
            var alarmsPond = base.Find(x => x.Trigger.SensorItem.Sensor.Pond.Site.CustomerId == customerId && x.EndDate == null, includes).ToList();
            var alarmsTank = base.Find(x => x.Trigger.SensorItem.Sensor.Tank.Site.CustomerId == customerId && x.EndDate == null, includes).ToList();

            alarms.AddRange(alarmsSite);
            alarms.AddRange(alarmsPond);
            alarms.AddRange(alarmsTank);

            var a = alarms.Distinct();

            return a;
        }

        /// <summary>
        /// Gets all Alarms Open by SiteId
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpenBySite(Guid siteId)
        {
            return this.GetsOpenBySite(siteId, null);
        }

        /// <summary>
        /// Gets all Alarms Open by SiteId
        /// Including Entities
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpenBySite(Guid siteId, params String[] includes)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.SiteId == siteId && x.Trigger.SensorItem.Sensor.Tank == null && x.EndDate == null, includes);
        }

        /// <summary>
        /// Gets all Alarms Open by PondId
        /// </summary>
        /// <param name="pondId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpenByPond(Guid pondId)
        {
            return this.GetsOpenByPond(pondId, null);
        }

        /// <summary>
        /// Gets all Alarms Open by PondId
        /// including entities
        /// </summary>
        /// <param name="pondId"></param>
        /// <param name="includess"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpenByPond(Guid pondId, params String[] includes)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.PondId == pondId && x.EndDate == null, includes);
        }

        /// <summary>
        /// Gets all Alarms Open by TankId
        /// </summary>
        /// <param name="tankId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpenByTank(Guid tankId)
        {
            return this.GetsOpenByTank(tankId, null);
        }

        /// <summary>
        /// Gets all Alarms Open by TankId
        /// Including Entities
        /// </summary>
        /// <param name="tankId"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpenByTank(Guid tankId, params String[] includes)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.TankId == tankId && x.EndDate == null, includes);
        }

        /// <summary>
        /// Gets all Alarms Open and Close by SensorId
        /// </summary>
        /// <param name="sensorId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpenBySensor(Guid sensorId)
        {
            return this.GetsBySensor(sensorId);
        }

        /// <summary>
        /// Gets all Alarms Open and Close by SensorId
        /// Including Entities
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsOpenBySensor(Guid sensorId, params String[] includes)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.Id == sensorId && x.EndDate == null, includes);
        }

        #endregion Open

        /// <summary>
        /// Gets all Alarms Open and Close by SiteId
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsBySite(Guid siteId)
        {
            return this.GetsBySite(siteId, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsBySite(Guid siteId, params String[] includes)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.SiteId == siteId && x.Trigger.SensorItem.Sensor.Tank == null);
        }

        /// <summary>
        /// Gets all Alarms Open and Close by PondId
        /// </summary>
        /// <param name="pondId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsByPond(Guid pondId)
        {
            return GetsByPond(pondId, null);
        }

        /// <summary>
        /// Gets all Alarms Open and Close by PondId
        /// Include Entities
        /// </summary>
        /// <param name="pondId"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsByPond(Guid pondId, params String[] includes)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.PondId == pondId, includes);
        }

        /// <summary>
        /// Gets all Alarms Open and Close by TankId
        /// </summary>
        /// <param name="tankId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsByTank(Guid tankId)
        {
            return this.GetsByTank(tankId);
        }

        /// <summary>
        /// Gets all Alarms Open and Close by TankId
        /// Include Entities
        /// </summary>
        /// <param name="tankId"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsByTank(Guid tankId, params String[] includes)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.TankId == tankId, includes);
        }

        /// <summary>
        /// Gets all Alarms Open and Close by SensorId
        /// </summary>
        /// <param name="sensorId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsBySensor(Guid sensorId)
        {
            return this.GetsBySensor(sensorId, null);
        }

        /// <summary>
        /// Gets all Alarms Open and Close by SensorId
        /// Include Entities
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsBySensor(Guid sensorId, params String[] includes)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.Id == sensorId, includes);            
        }

        public Int32 GetTotalOpenByTankId(Guid tankId)
        {
            //var total = (from a in Context.Alarms
            //             join sie in Context.SensorItemEvents on a.SensorItemEventId equals sie.Id
            //             join si in Context.SensorItems on sie.SensorItemId equals si.Id
            //             join s in Context.Sensors on si.SensorId equals s.Id
            //             join t in Context.Tanks on s.TankId equals tankId
            //             select a).Count();

            //var query = (from a in Context.Alarms
            //             join sie in Context.SensorItemEvents on a.SensorItemEventId equals sie.Id
            //             join si in Context.SensorItems on sie.SensorItemId equals si.Id
            //             join s in Context.Sensors on si.SensorId equals s.Id
            //             join t in Context.Tanks on s.TankId equals tankId
            //             select a);

            var total = base.Find(x => x.Trigger.SensorItem.Sensor.TankId == tankId).Count();

            return total;
        }

        /// <summary>
        /// Get all by TriggerId
        /// </summary>
        /// <param name="triggerId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsByTrigger(Guid triggerId)
        {
            return base.Find(x => x.TriggerId == triggerId);
        }

        /// <summary>
        /// Get Alarm Open by TriggerId
        /// </summary>
        /// <param name="triggerId"></param>
        /// <returns></returns>
        public Alarm GetOpenByTrigger(Guid triggerId)
        {
            return base.Find(x => x.TriggerId == triggerId && x.EndDate == null).OrderByDescending(x => x.StartDate).SingleOrDefault();
        }

        /// <summary>
        /// Get Alarm Close by TriggerId
        /// </summary>
        /// <param name="triggerId"></param>
        /// <returns></returns>
        public IEnumerable<Alarm> GetsCloseByTrigger(Guid triggerId)
        {
            return base.Find(x => x.TriggerId == triggerId && x.EndDate != null).OrderByDescending(x => x.StartDate);
        }
    }
}
