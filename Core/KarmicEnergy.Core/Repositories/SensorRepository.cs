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

        public Boolean HasSensorTank(Guid tankId)
        {
            return base.Find(x => x.TankId == tankId).Any();
        }

        public Boolean HasSensorSite(Guid siteId)
        {
            return base.Find(x => x.SiteId == siteId && x.TankId == null).Any();
        }

        public List<Sensor> GetsByCustomer(Guid customerId)
        {
            return base.Find(x => x.Tank.Site.CustomerId == customerId && x.DeletedDate == null).ToList();
        }

        public List<Sensor> GetsByTank(Guid tankId)
        {
            return base.Find(x => x.TankId == tankId).ToList();
        }

        public List<Sensor> GetsByCustomerAndTank(Guid customerId, Guid tankId)
        {
            return base.Find(x => x.Tank.Site.CustomerId == customerId && x.TankId == tankId && x.DeletedDate == null).ToList();
        }

        /// <summary>
        /// Gets Sensor Site
        /// NOT Sensor Tank
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public List<Sensor> GetsBySite(Guid siteId)
        {
            return base.Find(x => x.SiteId == siteId && x.TankId == null && x.DeletedDate == null).ToList();
        }

        public List<Sensor> GetsByCustomerAndSite(Guid customerId, Guid siteId)
        {
            return base.Find(x => x.Tank.Site.CustomerId == customerId && x.SiteId == siteId && x.TankId == null && x.DeletedDate == null).ToList();
        }

        public List<Sensor> GetsBySiteAndSensorType(Guid siteId, SensorTypeEnum sensorType)
        {
            return base.Find(x => x.SensorTypeId == (Int16)sensorType && x.SiteId == siteId && x.TankId == null).ToList();
        }
    }
}
