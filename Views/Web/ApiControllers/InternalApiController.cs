using System;
using System.Collections.Generic;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Entities;
using System.Web.Http;

namespace KarmicEnergy.Web.ApiControllers
{
    public class InternalApiController : ApiControllerBase
    {
        #region Tank
        [HttpGet]
        [Route("api/v1/json/customer/tank/gets/{customerId}", Name = "GetsTankByCustomerId")]
        public IHttpActionResult GetsTankByCustomerId(String customerId)
        {
            List<Tank> tanks = KEUnitOfWork.TankRepository.GetsByCustomerId(Guid.Parse(customerId));
            return Ok(tanks);
        }
        #endregion Tank

        #region SensorAlarm
        [HttpPost]
        [Route("api/v1/json/customer/sensoralarm/add", Name = "AddSensorAlarm")]
        public IHttpActionResult AddSensorAlarm(ApiControllers.Models.SensorAlarm model)
        {
            Trigger trigger = new Trigger()
            {
                Name = model.Description,
                Email = model.Email,
                SeverityId = model.SeverityId//,
                //SensorItemId = model.SensorId
            };

            KEUnitOfWork.TriggerRepository.Add(trigger);
            KEUnitOfWork.Complete();
            return Ok(trigger);
        }
        #endregion SensorAlarm
    }
}