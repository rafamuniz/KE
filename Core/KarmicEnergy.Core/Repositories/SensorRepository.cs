using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Munizoft.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class SensorRepository : Repository<Sensor, KEContext>, ISensorRepository
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

            if (sensors.Any())
                sensors.ForEach(n =>
                {
                    Context.Entry(n).Reference("Tank").Load();
                });

            return sensors;
        }
    }
}
