using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Web.Entities;
using KarmicEnergy.Web.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web.Http;

namespace KarmicEnergy.Web.ApiControllers
{
    public class SyncApiController : ApiControllerBase
    {
        #region Get

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
        [Route("sync/user/{siteId}/{lastSyncDate}", Name = "GetsUser")]
        public IHttpActionResult GetsUser(Guid siteId, DateTime lastSyncDate)
        {
            List<Guid> ids = new List<Guid>();
            List<ApplicationUser> entities = new List<ApplicationUser>();

            try
            {
                DateTime minDate = (DateTime)SqlDateTime.MinValue;
                var customers = KEUnitOfWork.CustomerRepository.GetsBySiteToSync(siteId, minDate).ToList();
                var customerUsers = KEUnitOfWork.CustomerUserRepository.GetsBySiteToSync(siteId, minDate).ToList();

                customers.ForEach(x => ids.Add(x.Id));
                customerUsers.ForEach(x => ids.Add(x.Id));

                if (ids != null && ids.Any())
                {
                    var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ApplicationContext.Create()));

                    foreach (var id in ids)
                    {
                        var user = userManager.FindById(id.ToString());
                        if (user.LastModifiedDate > lastSyncDate)
                        {
                            user.RoleNames = userManager.GetRoles(id.ToString());
                            entities.Add(user);
                        }
                    }
                }

                return Ok(entities);
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

        #endregion Get

        #region Post

        #region User

        [HttpPost]
        [Route("sync/user/post/{siteId}", Name = "PostsUser")]
        public IHttpActionResult PostsUser(Guid siteId, List<ApplicationUser> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ApplicationContext.Create()));

                    foreach (var e in entities)
                    {
                        var entity = userManager.FindByName(e.UserName);

                        if (entity == null) // ADD
                        {
                            e.Roles.Clear();
                            userManager.Create(e);

                            // Add Roles
                            foreach (var role in e.RoleNames)
                            {
                                userManager.AddToRole(e.Id, role);
                            }
                        }
                        else if (entity != null && e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            userManager.Update(entity);

                            // Roles
                            foreach (var role in entity.Roles)
                            {
                                userManager.RemoveFromRole(entity.Id, role.RoleId);
                            }
                            // Add Roles
                            foreach (var role in e.RoleNames)
                            {
                                userManager.AddToRole(entity.Id, role);
                            }
                        }
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

        [HttpPost]
        [Route("sync/address/post/{siteId}", Name = "PostsAddress")]
        public IHttpActionResult PostsAddress(Guid siteId, List<Address> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.AddressRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.AddressRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.AddressRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Address

        #region CustomerUser

        [HttpPost]
        [Route("sync/CustomerUser/post/{siteId}", Name = "PostsCustomerUser")]
        public IHttpActionResult PostsCustomerUser(Guid siteId, List<CustomerUser> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.CustomerUserRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.CustomerUserRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.CustomerUserRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion CustomerUser

        #region Contact

        [HttpPost]
        [Route("sync/Contact/post/{siteId}", Name = "PostsContact")]
        public IHttpActionResult PostsContact(Guid siteId, List<Contact> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.ContactRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.ContactRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.ContactRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Contact

        #region Pond
        [HttpPost]
        [Route("sync/pond/post/{siteId}", Name = "PostsPond")]
        public IHttpActionResult PostsPond(Guid siteId, List<Pond> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.PondRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.PondRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.PondRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Pond

        #region Tank
        [HttpPost]
        [Route("sync/Tank/post/{siteId}", Name = "PostsTank")]
        public IHttpActionResult PostsTank(Guid siteId, List<Tank> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.TankRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.TankRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.TankRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion Tank

        #region Sensor
        [HttpPost]
        [Route("sync/Sensor/post/{siteId}", Name = "PostsSensor")]
        public IHttpActionResult PostsSensor(Guid siteId, List<Sensor> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.SensorRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.SensorRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.SensorRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion Sensor

        #region SensorItem
        [HttpPost]
        [Route("sync/SensorItem/post/{siteId}", Name = "PostsSensorItem")]
        public IHttpActionResult PostsSensorItem(Guid siteId, List<SensorItem> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.SensorItemRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.SensorItemRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.SensorItemRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion SensorItem

        #region SensorItemEvent
        [HttpPost]
        [Route("sync/SensorItemEvent/post/{siteId}", Name = "PostsSensorItemEvent")]
        public IHttpActionResult PostsSensorItemEvent(Guid siteId, List<SensorItemEvent> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.SensorItemEventRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.SensorItemEventRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.SensorItemEventRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion SensorItemEvent

        #region Trigger
        [HttpPost]
        [Route("sync/Trigger/post/{siteId}", Name = "PostsTrigger")]
        public IHttpActionResult PostsTrigger(Guid siteId, List<Trigger> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.TriggerRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.TriggerRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.TriggerRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion Trigger

        #region TriggerContact
        [HttpPost]
        [Route("sync/TriggerContact/post/{siteId}", Name = "PostsTriggerContact")]
        public IHttpActionResult PostsTriggerContact(Guid siteId, List<TriggerContact> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.TriggerContactRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.TriggerContactRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.TriggerContactRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion TriggerContact

        #region Alarm
        [HttpPost]
        [Route("sync/Alarm/post/{siteId}", Name = "PostsAlarm")]
        public IHttpActionResult PostsAlarm(Guid siteId, List<Alarm> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.AlarmRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.AlarmRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.AlarmRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion Alarm

        #region AlarmHistory
        [HttpPost]
        [Route("sync/AlarmHistory/post/{siteId}", Name = "PostsAlarmHistory")]
        public IHttpActionResult PostsAlarmHistory(Guid siteId, List<AlarmHistory> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    using (var ke = KEUnitOfWork.Create(false))
                    {
                        foreach (var e in entities)
                        {
                            var entity = ke.AlarmHistoryRepository.Get(e.Id);
                            if (entity == null)
                            {
                                ke.AlarmHistoryRepository.Add(e);
                            }
                            else
                            {
                                entity.Update(e);
                                ke.AlarmHistoryRepository.UpdateWithoutDate(entity);
                            }
                        }

                        ke.Complete();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion AlarmHistory

        #endregion Post
    }
}