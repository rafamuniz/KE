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
                //return Context.SensorData.Last(x => x.Sensor.Tank.Id == tankId && x.WaterVolume != null);
                return base.Find(x => x.SensorItem.Sensor.TankId == tankId && x.SensorItem.Item.Id == (int)ItemEnum.WaterVolume).Last();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
