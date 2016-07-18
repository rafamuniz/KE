using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http;

namespace KarmicEnergy.Web.Jobs
{
    public class Sync
    {
        private KEUnitOfWork KEUnitOfWork;

        public String SiteId
        {
            get
            {
                return ConfigurationManager.AppSettings["Site:Id"].ToString();
            }
        }

        public String MasterUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Master:Url"].ToString();
            }
        }

        public void Execute()
        {
            try
            {
                KEUnitOfWork = KEUnitOfWork.Create();

                if (!String.IsNullOrEmpty(SiteId)) // Site
                {
                    Get();
                    Send();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Get

        private void Get()
        {
            DateTime startDate = DateTime.UtcNow;
            var lastDataSync = KEUnitOfWork.DataSyncRepository.Find(x => x.Action == "GET").OrderByDescending(x => x.SyncDate).LastOrDefault();
            List<DateTime> dates = new List<DateTime>();
            DateTime lastDateTime = DateTime.MinValue;

            if (lastDataSync != null)
                lastDateTime = lastDataSync.SyncDate;

            dates.Add(GetActionType(lastDateTime));
            dates.Add(GetSensorType(lastDateTime));
            dates.Add(GetNotificationType(lastDateTime));
            dates.Add(GetOperatorType(lastDateTime));
            dates.Add(GetUnitType(lastDateTime));
            dates.Add(GetUnit(lastDateTime));
            dates.Add(GetStickConversion(lastDateTime));
            dates.Add(GetStickConversionValue(lastDateTime));
            dates.Add(GetGeometry(lastDateTime));
            dates.Add(GetLogType(lastDateTime));
            dates.Add(GetSeverity(lastDateTime));
            dates.Add(GetCountry(lastDateTime));
            dates.Add(GetCity(lastDateTime));

            dates.Add(GetNotificationTemplate(lastDateTime));
            dates.Add(GetTankModel(lastDateTime));
            dates.Add(GetItem(lastDateTime));

            dates.Add(GetAddress(lastDateTime));
            dates.Add(GetCustomer(lastDateTime));
            dates.Add(GetSite(lastDateTime));
            dates.Add(GetPond(lastDateTime));
            dates.Add(GetTank(lastDateTime));
            dates.Add(GetSensor(lastDateTime));
            dates.Add(GetSensorItem(lastDateTime));
            dates.Add(GetTrigger(lastDateTime));
            dates.Add(GetTriggerContact(lastDateTime));

            dates.Add(GetCustomerSetting(lastDateTime));
            dates.Add(GetCustomerUser(lastDateTime));
            dates.Add(GetCustomerUserSetting(lastDateTime));
            dates.Add(GetCustomerUserSite(lastDateTime));

            dates.Add(GetGroup(lastDateTime));
            dates.Add(GetSensorGroup(lastDateTime));

            DataSync dataSync = new DataSync()
            {
                Action = "GET",
                StartDate = startDate,
                EndDate = DateTime.UtcNow,
                SiteId = Guid.Parse(SiteId),
                SyncDate = dates.Any() ? dates.Max<DateTime>() : (DateTime)SqlDateTime.MinValue
            };

            KEUnitOfWork.DataSyncRepository.Add(dataSync);
            KEUnitOfWork.Complete();
        }

        private DateTime GetActionType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/ActionType/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<ActionType> entities = Gets<ActionType>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.ActionTypeRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.ActionTypeRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.ActionTypeRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetSensorType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/SensorType/", SiteId, lastSync.ToString("yyyy-MM-dd"));

            List<SensorType> entities = Gets<SensorType>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.SensorTypeRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.SensorTypeRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.SensorTypeRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetNotificationType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/NotificationType/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<NotificationType> entities = Gets<NotificationType>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.NotificationTypeRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.NotificationTypeRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.NotificationTypeRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetOperatorType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/OperatorType/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<OperatorType> entities = Gets<OperatorType>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.OperatorTypeRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.OperatorTypeRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.OperatorTypeRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetOperator(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Operator/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Operator> entities = Gets<Operator>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.OperatorRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.OperatorRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.OperatorRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetUnitType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/UnitType/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<UnitType> entities = Gets<UnitType>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.UnitTypeRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.UnitTypeRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.UnitTypeRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetUnit(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Unit/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Unit> entities = Gets<Unit>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.UnitRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.UnitRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.UnitRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetStickConversion(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/StickConversion/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<StickConversion> entities = Gets<StickConversion>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.StickConversionRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.StickConversionRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.StickConversionRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetStickConversionValue(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/StickConversionValue/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<StickConversionValue> entities = Gets<StickConversionValue>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.StickConversionValueRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.StickConversionValueRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.StickConversionValueRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetGeometry(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Geometry/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Geometry> entities = Gets<Geometry>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.GeometryRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.GeometryRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.GeometryRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetLogType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/LogType/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<LogType> entities = Gets<LogType>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.LogTypeRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.LogTypeRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.LogTypeRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetLog(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Log/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Log> entities = Gets<Log>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.LogRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.LogRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.LogRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetNotificationTemplate(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/NotificationTemplate/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<NotificationTemplate> entities = Gets<NotificationTemplate>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.NotificationTemplateRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.NotificationTemplateRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.NotificationTemplateRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetSeverity(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Severity/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Severity> entities = Gets<Severity>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.SeverityRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.SeverityRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.SeverityRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetTankModel(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/TankModel/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<TankModel> entities = Gets<TankModel>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.TankModelRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.TankModelRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.TankModelRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetCountry(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Country/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Country> entities = Gets<Country>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.CountryRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.CountryRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.CountryRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetCity(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/City/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<City> entities = Gets<City>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.CityRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.CityRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.CityRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetAddress(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Address/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Address> entities = Gets<Address>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.AddressRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.AddressRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.AddressRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetItem(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Item/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Item> entities = Gets<Item>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.ItemRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.ItemRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.ItemRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetNotification(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Notification/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Notification> entities = Gets<Notification>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.NotificationRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.NotificationRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.NotificationRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetAlarm(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Alarm/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Alarm> entities = Gets<Alarm>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.AlarmRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.AlarmRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.AlarmRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetAlarmHistory(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/AlarmHistory/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<AlarmHistory> entities = Gets<AlarmHistory>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.AlarmHistoryRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.AlarmHistoryRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.AlarmHistoryRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetContact(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Contact/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Contact> entities = Gets<Contact>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.ContactRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.ContactRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.ContactRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetCustomer(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Customer/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Customer> entities = Gets<Customer>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.CustomerRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.CustomerRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.CustomerRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetCustomerSetting(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/CustomerSetting/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<CustomerSetting> entities = Gets<CustomerSetting>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.CustomerSettingRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.CustomerSettingRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.CustomerSettingRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetCustomerUser(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/CustomerUser/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<CustomerUser> entities = Gets<CustomerUser>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.CustomerUserRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.CustomerUserRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.CustomerUserRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetCustomerUserSite(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/CustomerUserSite/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<CustomerUserSite> entities = Gets<CustomerUserSite>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.CustomerUserSiteRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.CustomerUserSiteRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.CustomerUserSiteRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetCustomerUserSetting(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/CustomerUserSetting/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<CustomerUserSetting> entities = Gets<CustomerUserSetting>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.CustomerUserSettingRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.CustomerUserSettingRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.CustomerUserSettingRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetTrigger(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Trigger/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Trigger> entities = Gets<Trigger>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.TriggerRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.TriggerRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.TriggerRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetTriggerContact(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/TriggerContact/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<TriggerContact> entities = Gets<TriggerContact>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.TriggerContactRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.TriggerContactRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.TriggerContactRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetUser(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/User/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<User> entities = Gets<User>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.UserRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.UserRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.UserRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetSite(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Site/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Site> entities = Gets<Site>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.SiteRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.SiteRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.SiteRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetTank(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Tank/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Tank> entities = Gets<Tank>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.TankRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.TankRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.TankRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetPond(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Pond/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Pond> entities = Gets<Pond>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.PondRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.PondRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.PondRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetSensor(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Sensor/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Sensor> entities = Gets<Sensor>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.SensorRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.SensorRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.SensorRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetGroup(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Group/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Group> entities = Gets<Group>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.GroupRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.GroupRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.GroupRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetSensorGroup(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/SensorGroup/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<SensorGroup> entities = Gets<SensorGroup>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.SensorGroupRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.SensorGroupRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.SensorGroupRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetSensorItem(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/SensorItem/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<SensorItem> entities = Gets<SensorItem>(url);

            if (entities.Any())
            {


                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.SensorItemRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.SensorItemRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.SensorItemRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime GetSensorItemEvent(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/SensorItemEvent/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<SensorItemEvent> entities = Gets<SensorItemEvent>(url);

            if (entities.Any())
            {
                foreach (var e in entities)
                {
                    var entity = KEUnitOfWork.SensorItemEventRepository.Get(e.Id);
                    if (entity == null)
                    {
                        KEUnitOfWork.SensorItemEventRepository.Add(e);
                    }
                    else
                    {
                        entity.Update(e);
                        KEUnitOfWork.SensorItemEventRepository.UpdateWithoutDate(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private List<T> Gets<T>(String url)
        {
            List<T> entities = null;

            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(url).Result;
                entities = JsonConvert.DeserializeObject<List<T>>(response);
            }

            return entities;
        }
        #endregion Get

        #region Send

        private void Send()
        {
            DateTime startDate = DateTime.UtcNow;
            var lastDataSync = KEUnitOfWork.DataSyncRepository.Find(x => x.Action == "SEND").OrderByDescending(x => x.SyncDate).LastOrDefault();
            List<DateTime> dates = new List<DateTime>();
            DateTime lastDateTime = DateTime.MinValue;

            if (lastDataSync != null)
                lastDateTime = lastDataSync.SyncDate;

            //dates.Add(SendAddress(lastDateTime));
            //dates.Add(SendCustomer(lastDateTime));
            //dates.Add(SendSite(lastDateTime));
            //dates.Add(SendPond(lastDateTime));
            //dates.Add(SendTank(lastDateTime));
            //dates.Add(SendSensor(lastDateTime));
            //dates.Add(SendSensorItem(lastDateTime));
            //dates.Add(SendTrigger(lastDateTime));
            //dates.Add(SendTriggerContact(lastDateTime));

            //dates.Add(SendCustomerSetting(lastDateTime));
            //dates.Add(SendCustomerUser(lastDateTime));
            //dates.Add(SendCustomerUserSetting(lastDateTime));
            //dates.Add(SendCustomerUserSite(lastDateTime));

            //dates.Add(SendGroup(lastDateTime));
            //dates.Add(SendSensorGroup(lastDateTime));

            DataSync dataSync = new DataSync()
            {
                Action = "SEND",
                StartDate = startDate,
                EndDate = DateTime.UtcNow,
                SiteId = Guid.Parse(SiteId),
                SyncDate = dates.Any() ? dates.Max<DateTime>() : (DateTime)SqlDateTime.MinValue
            };

            KEUnitOfWork.DataSyncRepository.Add(dataSync);
            KEUnitOfWork.Complete();
        }

        #endregion Send
    }
}
