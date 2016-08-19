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

        public Boolean HasSensorSite(Guid siteId)
        {
            return base.Find(x => x.SiteId == siteId && x.DeletedDate == null).Any();
        }

        public Boolean HasSensorTank(Guid tankId)
        {
            return base.Find(x => x.TankId == tankId && x.DeletedDate == null).Any();
        }

        public Boolean HasSensorPond(Guid pondId)
        {
            return base.Find(x => x.PondId == pondId && x.DeletedDate == null).Any();
        }

        public IEnumerable<Sensor> GetsActive()
        {
            return base.Find(x => x.Status == "A" && x.DeletedDate == null);
        }

        public IEnumerable<Sensor> GetsByCustomer(Guid customerId)
        {
            List<Sensor> sensors = new List<Sensor>();
            var sensorsSite = base.Find(x => x.Site.CustomerId == customerId && x.DeletedDate == null);
            var sensorsPond = base.Find(x => x.Pond.Site.CustomerId == customerId && x.DeletedDate == null);
            var sensorsTank = base.Find(x => x.Tank.Site.CustomerId == customerId && x.DeletedDate == null);

            sensors.AddRange(sensorsSite);
            sensors.AddRange(sensorsPond);
            sensors.AddRange(sensorsTank);

            return sensors;
        }

        public IEnumerable<Sensor> GetsBySite(Guid siteId)
        {
            return base.Find(x => x.SiteId == siteId && x.DeletedDate == null);
        }

        public IEnumerable<Sensor> GetsByPond(Guid pondId)
        {
            return base.Find(x => x.PondId == pondId && x.DeletedDate == null);
        }

        public IEnumerable<Sensor> GetsByTank(Guid tankId)
        {
            return base.Find(x => x.TankId == tankId && x.DeletedDate == null);
        }

        /// <summary>
        /// Gets All Sensors that is installed in Site
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public IEnumerable<Sensor> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            return base.Find(x => x.Site.CustomerId == customerId && x.SiteId == siteId && x.TankId == null && x.DeletedDate == null);
        }

        /// <summary>
        /// Gets All Sensors that is installed in Tank
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="tankId"></param>
        /// <returns></returns>
        public IEnumerable<Sensor> GetsByCustomerAndTank(Guid customerId, Guid tankId)
        {
            return base.Find(x => x.Tank.Site.CustomerId == customerId && x.TankId == tankId && x.DeletedDate == null);
        }

        /// <summary>
        /// Gets All Sensors that is installed in Pond
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="pondId"></param>
        /// <returns></returns>
        public IEnumerable<Sensor> GetsByCustomerAndPond(Guid customerId, Guid pondId)
        {
            return base.Find(x => x.Pond.Site.CustomerId == customerId && x.PondId == pondId && x.DeletedDate == null);
        }

        public IEnumerable<Sensor> GetsBySiteAndSensorType(Guid siteId, SensorTypeEnum sensorType)
        {
            return base.Find(x => x.SensorTypeId == (Int16)sensorType && x.SiteId == siteId);
        }

        public override IEnumerable<Sensor> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<Sensor> entities = new List<Sensor>();
            List<Sensor> sensors = new List<Sensor>();

            var sites = base.Find(x => x.SiteId == siteId && x.LastModifiedDate > lastSyncDate);
            var ponds = base.Find(x => x.Pond.SiteId == siteId && x.LastModifiedDate > lastSyncDate);
            var tanks = base.Find(x => x.Tank.SiteId == siteId && x.LastModifiedDate > lastSyncDate);

            entities.AddRange(sites);
            entities.AddRange(ponds);
            entities.AddRange(tanks);

            foreach (var entity in entities)
            {
                Sensor sensor = new Sensor()
                {
                    Id = entity.Id
                };

                sensor.Update(entity);
                sensors.Add(sensor);
            }

            return sensors.AsEnumerable();
        }
    }
}
