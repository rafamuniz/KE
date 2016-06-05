﻿using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Linq;
//using System.Data.Entity;

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

            return alarms;
        }

        public List<Alarm> GetsActiveBySite(Guid siteId)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.Tank.SiteId == siteId && x.EndDate == null).ToList();
        }

        public List<Alarm> GetsBySite(Guid siteId)
        {
            return base.Find(x => x.Trigger.SensorItem.Sensor.Tank.SiteId == siteId && x.EndDate == null).ToList();
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
    }
}
