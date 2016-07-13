using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Web.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;

namespace KarmicEnergy.Web.ApiControllers
{
    public class SyncApiController : ApiControllerBase
    {
        #region Common

        [HttpGet]
        [Route("sync/actiontype/{siteId}/{lastSyncDate}", Name = "GetsActionType")]
        public IHttpActionResult GetsActionType(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.ActionTypeRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/sensortype/{siteId}/{lastSyncDate}", Name = "GetsSensorType")]
        public IHttpActionResult GetsSensorType(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                List<SensorType> sensorTypes = new List<SensorType>();
                var entities = KEUnitOfWork.SensorTypeRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();

                foreach (var entity in entities)
                {
                    SensorType sensorType = new SensorType()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        CreatedDate = entity.CreatedDate,
                        DeletedDate = entity.DeletedDate,
                        LastModifiedDate = entity.LastModifiedDate,
                        RowVersion = entity.RowVersion,
                        Status = entity.Status,
                    };

                    sensorTypes.Add(sensorType);
                }

                return Ok(sensorTypes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/notificationtype/{siteId}/{lastSyncDate}", Name = "GetsNotificationType")]
        public IHttpActionResult GetsNotificationType(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.NotificationTypeRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/operatortype/{siteId}/{lastSyncDate}", Name = "GetsOperatorType")]
        public IHttpActionResult GetsOperatorType(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.OperatorTypeRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/unittype/{siteId}/{lastSyncDate}", Name = "GetsUnitType")]
        public IHttpActionResult GetsUnitType(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.UnitTypeRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/unit/{siteId}/{lastSyncDate}", Name = "GetsUnit")]
        public IHttpActionResult GetsUnit(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.UnitRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/stickconversion/{siteId}/{lastSyncDate}", Name = "GetsStickConversion")]
        public IHttpActionResult GetsStickConversion(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                List<StickConversion> stickConversions = new List<StickConversion>();
                var entities = KEUnitOfWork.StickConversionRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();

                foreach (var entity in entities)
                {
                    StickConversion stickConversion = new StickConversion()
                    {
                        Id = entity.Id,
                        FromUnitId = entity.FromUnitId,
                        ToUnitId = entity.ToUnitId,
                        Name = entity.Name,
                        CreatedDate = entity.CreatedDate,
                        DeletedDate = entity.DeletedDate,
                        LastModifiedDate = entity.LastModifiedDate,
                        RowVersion = entity.RowVersion,
                        Status = entity.Status,
                    };

                    stickConversions.Add(stickConversion);
                }

                return Ok(stickConversions);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/stickconversionvalue/{siteId}/{lastSyncDate}", Name = "GetsStickConversionValue")]
        public IHttpActionResult GetsStickConversionValue(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                List<StickConversionValue> stickConversionValues = new List<StickConversionValue>();
                var entities = KEUnitOfWork.StickConversionValueRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();

                foreach (var entity in entities)
                {
                    StickConversionValue stickConversionValue = new StickConversionValue()
                    {
                        Id = entity.Id,
                        FromValue = entity.FromValue,
                        ToValue = entity.ToValue,
                        StickConversionId = entity.StickConversionId,
                        CreatedDate = entity.CreatedDate,
                        DeletedDate = entity.DeletedDate,
                        LastModifiedDate = entity.LastModifiedDate,
                        RowVersion = entity.RowVersion
                    };

                    stickConversionValues.Add(stickConversionValue);
                }

                return Ok(stickConversionValues);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/geometry/{siteId}/{lastSyncDate}", Name = "GetsGeometry")]
        public IHttpActionResult GetsGeometry(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.GeometryRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/logtype/{siteId}/{lastSyncDate}", Name = "GetsLogType")]
        public IHttpActionResult GetsLogType(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.LogTypeRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/log/{siteId}/{lastSyncDate}", Name = "GetsLog")]
        public IHttpActionResult GetsLog(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.LogRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/notificationtemplate/{siteId}/{lastSyncDate}", Name = "GetsNotificationTemplate")]
        public IHttpActionResult GetsNotificationTemplate(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.NotificationTemplateRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/severity/{siteId}/{lastSyncDate}", Name = "GetsSeverity")]
        public IHttpActionResult GetsSeverity(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.SeverityRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/tankmodel/{siteId}/{lastSyncDate}", Name = "GetsTankModel")]
        public IHttpActionResult GetsTankModel(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.TankModelRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/country/{siteId}/{lastSyncDate}", Name = "GetsCountry")]
        public IHttpActionResult GetsCountry(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.CountryRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/city/{siteId}/{lastSyncDate}", Name = "GetsCity")]
        public IHttpActionResult GetsCity(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.CityRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Common

        #region User

        [HttpGet]
        [Route("sync/users/{siteId}", Name = "SyncUsers")]
        public IHttpActionResult SyncUsers(String siteId, List<ApplicationUser> users)
        {
            try
            {
                foreach (var user in users)
                {
                    //UserManager.RemoveFromRolesAsync(user.Id, user.Roles.)
                    //user.Roles.Remove();
                    foreach (var role in user.Roles)
                    {
                        //  UserManager.IsInRoleAsync();

                        //foreach (var role in user.Roles)
                        //{

                        //}
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion User

        #region Address

        [HttpGet]
        [Route("sync/address/{siteId}/{lastSyncDate}", Name = "GetsAddress")]
        public IHttpActionResult GetsAddress(String siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.AddressRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Address

        #region Customer

        [HttpGet]
        [Route("sync/customer/{siteId}/{lastSyncDate}", Name = "GetsCustomer")]
        public IHttpActionResult GetsCustomer(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.SiteRepository.Find(x => x.Id == siteId && x.LastModifiedDate > lastSyncDate).SingleOrDefault().Customer;
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Customer

        #region Tank
        #endregion Tank

    }
}