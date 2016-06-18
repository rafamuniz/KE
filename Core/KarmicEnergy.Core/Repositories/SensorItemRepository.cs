﻿using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorItemRepository : KERepositoryBase<SensorItem>, ISensorItemRepository
    {
        #region Constructor
        public SensorItemRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor 

        public Boolean HasSensorItem(Guid tankId, ItemEnum item)
        {
            return base.Find(x => x.Sensor.TankId == tankId && x.ItemId == (Int32)item).Any();
        }

        public Boolean HasSiteSensorItem(Guid siteId, ItemEnum item)
        {
            return base.Find(x => x.Sensor.SiteId == siteId && x.Sensor.TankId == null && x.ItemId == (Int32)item).Any();
        }

        public List<SensorItem> GetsBySiteAndItem(Guid siteId, ItemEnum item)
        {
            return base.Find(x => x.Sensor.SiteId == siteId && x.Sensor.TankId == null && x.ItemId == (Int32)item).ToList();
        }

        public List<SensorItem> GetsBySensor(Guid sensorId)
        {
            return base.Find(x => x.SensorId == sensorId && x.DeletedDate == null).ToList();
        }
    }
}
