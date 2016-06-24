using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class PondRepository : KERepositoryBase<Pond>, IPondRepository
    {
        #region Constructor
        public PondRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor        

        //public List<Tank> GetsByCustomerId(Guid customerId)
        //{
        //    return base.Find(x => x.Site.CustomerId == customerId && x.DeletedDate == null).ToList();
        //}

        //public List<Tank> GetsByCustomerIdAndSiteId(Guid customerId, Guid siteId)
        //{
        //    return base.Find(x => x.Site.CustomerId == customerId && x.SiteId == siteId && x.Status == "A" && x.DeletedDate == null).ToList();
        //}

        //public List<Models.TankModel> GetsWithLastMeasurement(Guid customerId)
        //{
        //    List<Models.TankModel> tankModels = new List<Models.TankModel>();
        //    var tanks = base.Find(x => x.Site.CustomerId == customerId).ToList();

        //    if (tanks.Any())
        //        tanks.ForEach(n =>
        //        {
        //            Models.TankModel tankModel = new Models.TankModel();
        //            tankModel.Id = n.Id;
        //            tankModel.Name = n.Name;

        //            var sensors = Context.Sensors.Where(s => s.TankId == n.Id);
        //            foreach (var s in sensors)
        //            {
        //                //var sensorData = Context.SensorData.Where(sd => sd.SensorId == s.Id).OrderByDescending(d => d.CreatedDate).First();
        //                //tankModel.WaterTemperature = sensorData.WaterTemperature;
        //                //tankModel.WeatherTemperature = sensorData.WeatherTemperature;
        //                //tankModel.LastMeasurementDate = sensorData.CreatedDate;

        //                //tankModel.NumAlarms = Context.Alarms.Where(sa => sa.SensorAlarm.SensorId == s.Id).Count();

        //                ////Context.Entry(n).Reference("Tank").Load();
        //                //tankModels.Add(tankModel);
        //            }
        //        });

        //    return tankModels;
        //}
    }
}
