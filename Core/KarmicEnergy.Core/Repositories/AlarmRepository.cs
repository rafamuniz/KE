using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class AlarmRepository : Repository<Alarm, KEContext>, IAlarmRepository
    {
        #region Constructor
        public AlarmRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor    

        //public List<Alarm> GetsByTankId(Guid tankId)
        //{
        //    var lastEvent = Context.Alarms.Where(x => x.SensorItem.Sensor.Tank.Id == tankId && x.SensorItem.ItemId == (int)item).OrderByDescending(d => d.EventDate);
        //    if (!lastEvent.Any())
        //        return null;

        //    return lastEvent.AsEnumerable().Last();
        //}
    }
}
