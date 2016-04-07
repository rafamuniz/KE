using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorItemEventRepository : Repository<SensorItemEvent, KEContext>, ISensorItemEventRepository
    {
        #region Constructor
        public SensorItemEventRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor       

        public SensorItemEvent GetTankWithWaterVolumeLastData(Guid tankId)
        {
            try
            {
                var lastEvent = Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.Tank.Id == tankId && x.SensorItem.ItemId == (int)ItemEnum.WaterVolume).OrderByDescending(d => d.EventDate).AsEnumerable().Last();
                //return base.Find(x => x.SensorItem.Sensor.TankId == tankId && x.SensorItem.Item.Id == (int)ItemEnum.WaterVolume).Last();
                return lastEvent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SensorItemEvent> GetTankWithWaterVolume(Guid tankId, Int32 quantity)
        {
            try
            {
                var events = Context.SensorItemEvents.Where(x => x.SensorItem.Sensor.Tank.Id == tankId && x.SensorItem.ItemId == (int)ItemEnum.WaterVolume && x.Value != null).OrderByDescending(d => d.EventDate).Take(quantity);
                return events.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
