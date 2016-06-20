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

        public List<Alarm> GetsActive()
        {
            return base.Find(x => x.EndDate == null).ToList();
        }

        public List<Alarm> GetsActiveByCustomer(Guid customerId)
        {
            List<Alarm> alarms = new List<Alarm>();

            var alarmsTank = base.Find(x => x.Trigger.SensorItem.Sensor.Tank.Site.CustomerId == customerId && x.EndDate == null).ToList();
            var alarmsSite = base.Find(x => x.Trigger.SensorItem.Sensor.Site.CustomerId == customerId && x.EndDate == null).ToList();

            alarms.AddRange(alarmsTank);
            alarms.AddRange(alarmsSite);

            var a = alarms.Distinct();

            return a.ToList();
        }

        public List<Alarm> GetsActiveBySite(Guid siteId)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.SiteId == siteId && x.Trigger.SensorItem.Sensor.Tank == null && x.EndDate == null).ToList();
        }

        public List<Alarm> GetsActiveByTank(Guid tankId)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.TankId == tankId && x.EndDate == null).ToList();
        }

        public List<Alarm> GetsBySite(Guid siteId)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.SiteId == siteId && x.Trigger.SensorItem.Sensor.Tank == null).ToList();
        }

        public List<Alarm> GetsByTank(Guid tankId)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.TankId == tankId).ToList();
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
        /// Get Alarm Active by TriggerId
        /// </summary>
        /// <param name="triggerId"></param>
        /// <returns></returns>
        public Alarm GetActiveByTrigger(Guid triggerId)
        {
            return base.Find(x => x.TriggerId == triggerId && x.EndDate == null).OrderByDescending(x => x.StartDate).SingleOrDefault();
        }
    }
}
