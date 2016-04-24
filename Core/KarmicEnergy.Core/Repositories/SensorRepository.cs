using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorRepository : KERepositoryBase<Sensor>, ISensorRepository
    {
        #region Constructor
        public SensorRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor       

        public Boolean HasSensor(Guid tankId)
        {
            return base.Find(x => x.TankId == tankId).Any();
        }

        public List<Sensor> GetsByCustomerId(Guid customerId)
        {
            return base.Find(x => x.Tank.Site.CustomerId == customerId).ToList();
        }

        public List<Sensor> GetsByTankId(Guid tankId)
        {
            return base.Find(x => x.TankId == tankId).ToList();
        }

        public List<Sensor> GetsByTankIdAndCustomerId(Guid customerId, Guid tankId)
        {
            var sensors = base.Find(x => x.Tank.Site.CustomerId == customerId && x.TankId == tankId).ToList();

            //if (sensors.Any())
            //    sensors.ForEach(n =>
            //    {
            //        Context.Entry(n).Reference("Tank").Load();
            //    });

            return sensors;
        }
    }
}
