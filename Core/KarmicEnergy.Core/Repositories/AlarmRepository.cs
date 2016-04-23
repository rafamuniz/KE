using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

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

        //public List<Alarm> GetsByTankId(Guid tankId)
        //{
        //    var lastEvent = Context.Alarms.Where(x => x.SensorItem.Sensor.Tank.Id == tankId && x.SensorItem.ItemId == (int)item).OrderByDescending(d => d.EventDate);
        //    if (!lastEvent.Any())
        //        return null;

        //    return lastEvent.AsEnumerable().Last();
        //}
    }
}
