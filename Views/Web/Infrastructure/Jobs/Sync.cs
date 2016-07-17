using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;

namespace KarmicEnergy.Web.Jobs
{
    public class Sync
    {
        private KEUnitOfWork KEUnitOfWork;

        public void Execute()
        {
            try
            {
                KEUnitOfWork = KEUnitOfWork.Create();
                String siteConfig = ConfigurationManager.AppSettings["Site:Id"].ToString();

                if (!String.IsNullOrEmpty(siteConfig)) // Site
                {
                    #region Get

                    var lastDataSync = KEUnitOfWork.DataSyncRepository.GetAll().OrderByDescending(x => x.SyncDate).LastOrDefault();
                    List<DateTime> dates = new List<DateTime>();
                    DateTime lastDateTime = DateTime.MinValue;
                    String ip = ConfigurationManager.AppSettings["Master:Url"];

                    if (lastDataSync == null)
                        lastDateTime = DateTime.MinValue;

                    dates.Add(ActionType(siteConfig, ip, lastDateTime));
                    dates.Add(SensorType(siteConfig, ip, lastDateTime));
                    dates.Add(NotificationType(siteConfig, ip, lastDateTime));
                    dates.Add(OperatorType(siteConfig, ip, lastDateTime));
                    dates.Add(UnitType(siteConfig, ip, lastDateTime));
                    dates.Add(Unit(siteConfig, ip, lastDateTime));
                    dates.Add(StickConversion(siteConfig, ip, lastDateTime));
                    dates.Add(StickConversionValue(siteConfig, ip, lastDateTime));
                    dates.Add(Geometry(siteConfig, ip, lastDateTime));
                    dates.Add(LogType(siteConfig, ip, lastDateTime));
                    dates.Add(Severity(siteConfig, ip, lastDateTime));
                    dates.Add(Country(siteConfig, ip, lastDateTime));
                    dates.Add(City(siteConfig, ip, lastDateTime));

                    dates.Add(NotificationTemplate(siteConfig, ip, lastDateTime));
                    dates.Add(TankModel(siteConfig, ip, lastDateTime));
                    //dates.Add(Operator(siteConfig, ip, lastDateTime));
                    //dates.Add(Notification(siteConfig, ip, lastDateTime));
                    dates.Add(Item(siteConfig, ip, lastDateTime));

                    dates.Add(Address(siteConfig, ip, lastDateTime));
                    dates.Add(Customer(siteConfig, ip, lastDateTime));
                    dates.Add(Site(siteConfig, ip, lastDateTime));
                    dates.Add(Pond(siteConfig, ip, lastDateTime));
                    dates.Add(Tank(siteConfig, ip, lastDateTime));
                    dates.Add(Sensor(siteConfig, ip, lastDateTime));
                    dates.Add(SensorItem(siteConfig, ip, lastDateTime));
                    dates.Add(Trigger(siteConfig, ip, lastDateTime));
                    dates.Add(TriggerContact(siteConfig, ip, lastDateTime));

                    dates.Add(CustomerSetting(siteConfig, ip, lastDateTime));
                    dates.Add(CustomerUser(siteConfig, ip, lastDateTime));
                    dates.Add(CustomerUserSetting(siteConfig, ip, lastDateTime));
                    dates.Add(CustomerUserSite(siteConfig, ip, lastDateTime));

                    dates.Add(Group(siteConfig, ip, lastDateTime));
                    dates.Add(SensorGroup(siteConfig, ip, lastDateTime));

                    DataSync dataSync = new DataSync()
                    {
                        SiteId = Guid.Parse(siteConfig),
                        SyncDate = dates.Max<DateTime>()
                    };

                    //dates.Add(Log(siteConfig, ip, lastDateTime));

                    KEUnitOfWork.DataSyncRepository.Add(dataSync);
                    KEUnitOfWork.Complete();

                    #endregion Get
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DateTime ActionType(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/ActionType/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.ActionTypeRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime SensorType(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/SensorType/", siteId, lastSync.ToString("yyyy-MM-dd"));

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
                        KEUnitOfWork.SensorTypeRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime NotificationType(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/NotificationType/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.NotificationTypeRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime OperatorType(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/OperatorType/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.OperatorTypeRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Operator(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Operator/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.OperatorRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime UnitType(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/UnitType/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.UnitTypeRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Unit(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Unit/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.UnitRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime StickConversion(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/StickConversion/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.StickConversionRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime StickConversionValue(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/StickConversionValue/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.StickConversionValueRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Geometry(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Geometry/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.GeometryRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime LogType(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/LogType/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.LogTypeRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Log(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Log/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.LogRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime NotificationTemplate(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/NotificationTemplate/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.NotificationTemplateRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Severity(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Severity/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.SeverityRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime TankModel(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/TankModel/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.TankModelRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Country(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Country/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.CountryRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime City(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/City/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.CityRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Address(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Address/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.AddressRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Item(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Item/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.ItemRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Notification(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Notification/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.NotificationRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Alarm(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Alarm/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.AlarmRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime AlarmHistory(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/AlarmHistory/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.AlarmHistoryRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Contact(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Contact/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.ContactRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Customer(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Customer/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.CustomerRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime CustomerSetting(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/CustomerSetting/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.CustomerSettingRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime CustomerUser(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/CustomerUser/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.CustomerUserRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime CustomerUserSite(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/CustomerUserSite/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.CustomerUserSiteRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime CustomerUserSetting(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/CustomerUserSetting/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.CustomerUserSettingRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Trigger(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Trigger/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.TriggerRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime TriggerContact(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/TriggerContact/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.TriggerContactRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime User(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/User/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.UserRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Site(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Site/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.SiteRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Tank(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Tank/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.TankRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Pond(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Pond/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.PondRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Sensor(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Sensor/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.SensorRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime Group(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/Group/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.GroupRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime SensorGroup(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/SensorGroup/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.SensorGroupRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime SensorItem(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/SensorItem/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.SensorItemRepository.Update(entity);
                    }
                }

                KEUnitOfWork.Complete();
                return entities.Max(x => x.LastModifiedDate);
            }

            return lastSync;
        }

        private DateTime SensorItemEvent(String siteId, String ip, DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", ip, "sync/SensorItemEvent/", siteId, lastSync.ToString("yyyy-MM-dd"));
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
                        KEUnitOfWork.SensorItemEventRepository.Update(entity);
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
    }
}
