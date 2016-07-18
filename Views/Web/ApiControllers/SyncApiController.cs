using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                List<NotificationTemplate> notificationTemplates = new List<NotificationTemplate>();
                var entities = KEUnitOfWork.NotificationTemplateRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();

                foreach (var entity in entities)
                {
                    NotificationTemplate notificationTemplate = new NotificationTemplate()
                    {
                        Id = entity.Id
                    };

                    notificationTemplate.Update(entity);
                    notificationTemplates.Add(notificationTemplate);
                }

                return Ok(notificationTemplates);
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
                List<TankModel> tankModels = new List<TankModel>();
                var entities = KEUnitOfWork.TankModelRepository.Find(x => x.LastModifiedDate > lastSyncDate).ToList();

                foreach (var entity in entities)
                {
                    TankModel tankModel = new TankModel()
                    {
                        Id = entity.Id
                    };

                    tankModel.Update(entity);
                    tankModels.Add(tankModel);
                }
                return Ok(tankModels);
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

        [HttpGet]
        [Route("sync/Item/{siteId}/{lastSyncDate}", Name = "GetsItem")]
        public IHttpActionResult GetsItem(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.ItemRepository.GetsBySiteToSync(siteId, lastSyncDate);
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
        public IHttpActionResult GetsAddress(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.AddressRepository.GetsBySiteToSync(siteId, lastSyncDate);
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
                var entities = KEUnitOfWork.CustomerRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Customer

        #region CustomerSetting

        [HttpGet]
        [Route("sync/CustomerSetting/{siteId}/{lastSyncDate}", Name = "GetsCustomerSetting")]
        public IHttpActionResult GetsCustomerSetting(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.CustomerSettingRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion CustomerSetting

        #region CustomerUser

        [HttpGet]
        [Route("sync/CustomerUser/{siteId}/{lastSyncDate}", Name = "GetsCustomerUser")]
        public IHttpActionResult GetsCustomerUser(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.CustomerUserRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion CustomerUser               

        #region CustomerUserSetting

        [HttpGet]
        [Route("sync/CustomerUserSetting/{siteId}/{lastSyncDate}", Name = "GetsCustomerUserSetting")]
        public IHttpActionResult GetsCustomerUserSetting(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.CustomerUserSettingRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion CustomerUserSetting               

        #region CustomerUserSite

        [HttpGet]
        [Route("sync/CustomerUserSite/{siteId}/{lastSyncDate}", Name = "GetsCustomerUserSite")]
        public IHttpActionResult GetsCustomerUserSite(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.CustomerUserSiteRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion CustomerUserSite

        #region Site

        [HttpGet]
        [Route("sync/site/{siteId}/{lastSyncDate}", Name = "GetsSite")]
        public IHttpActionResult GetsSite(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.SiteRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Site

        #region Tank

        [HttpGet]
        [Route("sync/tank/{siteId}/{lastSyncDate}", Name = "GetsTank")]
        public IHttpActionResult GetsTank(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.TankRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Tank

        #region Pond

        [HttpGet]
        [Route("sync/pond/{siteId}/{lastSyncDate}", Name = "GetsPond")]
        public IHttpActionResult GetsPond(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.PondRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Pond               

        #region Sensor

        [HttpGet]
        [Route("sync/Sensor/{siteId}/{lastSyncDate}", Name = "GetsSensor")]
        public IHttpActionResult GetsSensor(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.SensorRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Sensor               

        #region SensorItem

        [HttpGet]
        [Route("sync/SensorItem/{siteId}/{lastSyncDate}", Name = "GetsSensorItem")]
        public IHttpActionResult GetsSensorItem(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.SensorItemRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion SensorItem               

        #region Trigger

        [HttpGet]
        [Route("sync/Trigger/{siteId}/{lastSyncDate}", Name = "GetsTrigger")]
        public IHttpActionResult GetsTrigger(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.TriggerRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Trigger               

        #region TriggerContact

        [HttpGet]
        [Route("sync/TriggerContact/{siteId}/{lastSyncDate}", Name = "GetsTriggerContact")]
        public IHttpActionResult GetsTriggerContact(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.TriggerContactRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion TriggerContact               

        #region Group

        [HttpGet]
        [Route("sync/Group/{siteId}/{lastSyncDate}", Name = "GetsGroup")]
        public IHttpActionResult GetsGroup(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.GroupRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Group

        #region GroupSensor

        [HttpGet]
        [Route("sync/SensorGroup/{siteId}/{lastSyncDate}", Name = "GetsSensorGroup")]
        public IHttpActionResult GetsSensorGroup(Guid siteId, DateTime lastSyncDate)
        {
            try
            {
                var entities = KEUnitOfWork.SensorGroupRepository.GetsBySiteToSync(siteId, lastSyncDate);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion GroupSensor
    }
}