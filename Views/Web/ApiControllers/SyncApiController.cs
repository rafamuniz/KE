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
        [Route("sync/actiontype/", Name = "GetsActionType")]
        public IHttpActionResult GetsActionType()
        {
            try
            {
                var entities = KEUnitOfWork.ActionTypeRepository.GetAll().ToList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sync/operatortype/{siteId}/{lastSyncDate}", Name = "GetsOperatorType")]
        public IHttpActionResult GetsOperatorType(String siteId, DateTime lastSyncDate)
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
        [Route("sync/sensortype/{siteId}/{lastSyncDate}", Name = "GetsSensorType")]
        public IHttpActionResult GetsSensorType(String siteId, DateTime lastSyncDate)
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
        [Route("sync/unittype/{siteId}/{lastSyncDate}", Name = "GetsUnitType")]
        public IHttpActionResult GetsUnitType(String siteId, DateTime lastSyncDate)
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
        [Route("sync/stickconversion/{siteId}/{lastSyncDate}", Name = "GetsStickConversion")]
        public IHttpActionResult GetsStickConversion(String siteId, DateTime lastSyncDate)
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
        [Route("sync/geometry/{siteId}/{lastSyncDate}", Name = "GetsGeometry")]
        public IHttpActionResult GetsGeometry(String siteId, DateTime lastSyncDate)
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
        [Route("sync/severity/{siteId}/{lastSyncDate}", Name = "GetsSeverity")]
        public IHttpActionResult GetsSeverity(String siteId, DateTime lastSyncDate)
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
        [Route("sync/city/{siteId}/{lastSyncDate}", Name = "GetsCity")]
        public IHttpActionResult GetsCity(String siteId, DateTime lastSyncDate)
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

        [HttpGet]
        [Route("sync/country/{siteId}/{lastSyncDate}", Name = "GetsCountry")]
        public IHttpActionResult GetsCountry(String siteId, DateTime lastSyncDate)
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
        [Route("sync/logtype/{siteId}/{lastSyncDate}", Name = "GetsLogType")]
        public IHttpActionResult GetsLogType(String siteId, DateTime lastSyncDate)
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
        [Route("sync/notificationtype/{siteId}/{lastSyncDate}", Name = "GetsNotificationType")]
        public IHttpActionResult GetsNotificationType(String siteId, DateTime lastSyncDate)
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

        //NotificationTemplates(lastSync);
        //Units(lastSync);
        //StickConversionValues(lastSync);

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
        [Route("sync/addresses/{siteId}", Name = "SyncAddressess")]
        public IHttpActionResult SyncAddressess(String siteId, List<Address> addresses)
        {
            try
            {
                KEUnitOfWork.AddressRepository.AddOrUpdateRange(addresses);
                KEUnitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Address

        #region Tank
        #endregion Tank

    }
}