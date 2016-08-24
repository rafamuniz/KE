using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using KarmicEnergy.Web.Entities;
using KarmicEnergy.Web.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http;

namespace KarmicEnergy.Web.Jobs
{
    public class SyncData
    {
        #region Fields
        private readonly ILogService _logService;
        #endregion Fields

        #region Constructor
        public SyncData(ILogService logService)
        {
            this._logService = logService;
        }
        #endregion Constructor

        #region Properties

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
        #endregion Properties

        #region Functions
        public void Execute()
        {
            try
            {
                if (!String.IsNullOrEmpty(SiteId)) // Site
                {
                    Get();
                    Send();
                }
            }
            catch (Exception ex)
            {
                Log log = new Log()
                {
                    SiteId = String.IsNullOrEmpty(SiteId) ? Guid.Empty : Guid.Parse(SiteId),
                    LogTypeId = (Int16)LogTypeEnum.Error,
                    Message = ex.Message
                };

                this._logService.Create(log);
            }
        }

        #region Get

        private void Get()
        {
            DateTime startDate = DateTime.UtcNow;
            var ke = KEUnitOfWork.Create(false);
            var lastDataSync = ke.DataSyncRepository.Find(x => x.Action == "GET").OrderByDescending(x => x.SyncDate).FirstOrDefault();
            List<DateTime> dates = new List<DateTime>();
            DateTime lastDateTime = DateTime.MinValue;

            if (lastDataSync != null)
                lastDateTime = lastDataSync.SyncDate;

            dates.Add(GetUser(lastDateTime));
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

            ke.DataSyncRepository.Add(dataSync);
            ke.Complete();
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

        private DateTime GetActionType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/ActionType/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<ActionType> entities = Gets<ActionType>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.ActionTypeRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.ActionTypeRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.ActionTypeRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetSensorType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/SensorType/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<SensorType> entities = Gets<SensorType>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.SensorTypeRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.SensorTypeRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.SensorTypeRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetNotificationType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/NotificationType/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<NotificationType> entities = Gets<NotificationType>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.NotificationTypeRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.NotificationTypeRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.NotificationTypeRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetOperatorType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/OperatorType/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<OperatorType> entities = Gets<OperatorType>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.OperatorTypeRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.OperatorTypeRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.OperatorTypeRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetOperator(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Operator/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Operator> entities = Gets<Operator>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.OperatorRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.OperatorRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.OperatorRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetUnitType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/UnitType/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<UnitType> entities = Gets<UnitType>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.UnitTypeRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.UnitTypeRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.UnitTypeRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetUnit(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Unit/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Unit> entities = Gets<Unit>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.UnitRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.UnitRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.UnitRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetStickConversion(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/StickConversion/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<StickConversion> entities = Gets<StickConversion>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.StickConversionRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.StickConversionRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.StickConversionRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetStickConversionValue(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/StickConversionValue/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<StickConversionValue> entities = Gets<StickConversionValue>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.StickConversionValueRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.StickConversionValueRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.StickConversionValueRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetGeometry(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Geometry/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Geometry> entities = Gets<Geometry>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.GeometryRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.GeometryRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.GeometryRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetLogType(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/LogType/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<LogType> entities = Gets<LogType>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.LogTypeRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.LogTypeRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.LogTypeRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetLog(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Log/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Log> entities = Gets<Log>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.LogRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.LogRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.LogRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetNotificationTemplate(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/NotificationTemplate/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<NotificationTemplate> entities = Gets<NotificationTemplate>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.NotificationTemplateRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.NotificationTemplateRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.NotificationTemplateRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetSeverity(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Severity/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Severity> entities = Gets<Severity>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.SeverityRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.SeverityRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.SeverityRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetTankModel(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/TankModel/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<TankModel> entities = Gets<TankModel>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.TankModelRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.TankModelRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.TankModelRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetCountry(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Country/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Country> entities = Gets<Country>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.CountryRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.CountryRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.CountryRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetCity(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/City/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<City> entities = Gets<City>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.CityRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.CityRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.CityRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetAddress(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Address/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Address> entities = Gets<Address>(url);

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
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.AddressRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetItem(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Item/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Item> entities = Gets<Item>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.ItemRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.ItemRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.ItemRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetNotification(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Notification/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Notification> entities = Gets<Notification>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.NotificationRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.NotificationRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.NotificationRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        //private DateTime GetAlarm(DateTime lastSync)
        //{
        //    String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Alarm/", SiteId, lastSync.ToString("yyyy-MM-dd"));
        //    List<Alarm> entities = Gets<Alarm>(url);

        //    if (entities.Any())
        //    {
        //        foreach (var e in entities)
        //        {
        //            var entity = KEUnitOfWork.AlarmRepository.Get(e.Id);
        //            if (entity == null)
        //            {
        //                KEUnitOfWork.AlarmRepository.Add(e);
        //            }
        //            else
        //            {
        //                entity.Update(e);
        //                KEUnitOfWork.AlarmRepository.UpdateWithoutDate(entity);
        //            }
        //        }

        //        KEUnitOfWork.Complete();
        //        return entities.Max(x => x.LastModifiedDate);
        //    }

        //    return lastSync;
        //}

        //private DateTime GetAlarmHistory(DateTime lastSync)
        //{
        //    String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/AlarmHistory/", SiteId, lastSync.ToString("yyyy-MM-dd"));
        //    List<AlarmHistory> entities = Gets<AlarmHistory>(url);

        //    if (entities.Any())
        //    {
        //        foreach (var e in entities)
        //        {
        //            var entity = KEUnitOfWork.AlarmHistoryRepository.Get(e.Id);
        //            if (entity == null)
        //            {
        //                KEUnitOfWork.AlarmHistoryRepository.Add(e);
        //            }
        //            else
        //            {
        //                entity.Update(e);
        //                KEUnitOfWork.AlarmHistoryRepository.UpdateWithoutDate(entity);
        //            }
        //        }

        //        KEUnitOfWork.Complete();
        //        return entities.Max(x => x.LastModifiedDate);
        //    }

        //    return lastSync;
        //}

        private DateTime GetContact(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Contact/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Contact> entities = Gets<Contact>(url);

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
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.ContactRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetCustomer(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Customer/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Customer> entities = Gets<Customer>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.CustomerRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.CustomerRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.CustomerRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetCustomerSetting(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/CustomerSetting/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<CustomerSetting> entities = Gets<CustomerSetting>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.CustomerSettingRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.CustomerSettingRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.CustomerSettingRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetCustomerUser(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/CustomerUser/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<CustomerUser> entities = Gets<CustomerUser>(url);

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
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.CustomerUserRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetCustomerUserSite(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/CustomerUserSite/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<CustomerUserSite> entities = Gets<CustomerUserSite>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.CustomerUserSiteRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.CustomerUserSiteRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.CustomerUserSiteRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetCustomerUserSetting(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/CustomerUserSetting/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<CustomerUserSetting> entities = Gets<CustomerUserSetting>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.CustomerUserSettingRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.CustomerUserSettingRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.CustomerUserSettingRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetTrigger(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Trigger/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Trigger> entities = Gets<Trigger>(url);

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
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.TriggerRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetTriggerContact(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/TriggerContact/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<TriggerContact> entities = Gets<TriggerContact>(url);

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
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.TriggerContactRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetUser(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/User/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<ApplicationUser> entities = Gets<ApplicationUser>(url);

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

            return lastSync;
        }

        private DateTime GetSite(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Site/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Site> entities = Gets<Site>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.SiteRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.SiteRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.SiteRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetTank(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Tank/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Tank> entities = Gets<Tank>(url);

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
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.TankRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetPond(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Pond/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Pond> entities = Gets<Pond>(url);

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
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.PondRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetSensor(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Sensor/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Sensor> entities = Gets<Sensor>(url);

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
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.SensorRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetGroup(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/Group/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<Group> entities = Gets<Group>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.GroupRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.GroupRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.GroupRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetSensorGroup(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/SensorGroup/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<SensorGroup> entities = Gets<SensorGroup>(url);

            if (entities != null && entities.Any())
            {
                using (var ke = KEUnitOfWork.Create(false))
                {
                    foreach (var e in entities)
                    {
                        var entity = ke.SensorGroupRepository.Get(e.Id);
                        if (entity == null)
                        {
                            ke.SensorGroupRepository.Add(e);
                        }
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.SensorGroupRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetSensorItem(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/SensorItem/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<SensorItem> entities = Gets<SensorItem>(url);

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
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.SensorItemRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        private DateTime GetSensorItemEvent(DateTime lastSync)
        {
            String url = String.Format("{0}/{1}/{2}/{3}", MasterUrl, "sync/SensorItemEvent/", SiteId, lastSync.ToString("yyyy-MM-dd"));
            List<SensorItemEvent> entities = Gets<SensorItemEvent>(url);

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
                        else if (e.LastModifiedDate > entity.LastModifiedDate)
                        {
                            entity.Update(e);
                            ke.SensorItemEventRepository.UpdateWithoutDate(entity);
                        }
                    }

                    ke.Complete();
                    return entities.Max(x => x.LastModifiedDate);
                }
            }

            return lastSync;
        }

        #endregion Get

        #region Send

        private void Send()
        {
            DateTime startDate = DateTime.UtcNow;
            var ke = KEUnitOfWork.Create(false);
            var lastDataSync = ke.DataSyncRepository.Find(x => x.Action == "SEND").OrderByDescending(x => x.SyncDate).FirstOrDefault();
            List<DateTime> dates = new List<DateTime>();
            DateTime lastDateTime = DateTime.MinValue;

            if (lastDataSync != null)
                lastDateTime = lastDataSync.SyncDate;

            dates.Add(SendUser(lastDateTime));
            dates.Add(SendAddress(lastDateTime));
            dates.Add(SendCustomerUser(lastDateTime));
            dates.Add(SendContact(lastDateTime));

            dates.Add(SendPond(lastDateTime));
            dates.Add(SendTank(lastDateTime));
            dates.Add(SendSensor(lastDateTime));
            dates.Add(SendSensorItem(lastDateTime));
            dates.Add(SendSensorItemEvent(lastDateTime));
            dates.Add(SendTrigger(lastDateTime));
            dates.Add(SendTriggerContact(lastDateTime));

            dates.Add(SendAlarm(lastDateTime));
            dates.Add(SendAlarmHistory(lastDateTime));

            //dates.Add(SendCustomerSetting(lastDateTime));
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

            ke.DataSyncRepository.Add(dataSync);
            ke.Complete();
        }

        private Boolean Post<T>(String url, List<T> entities)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync<List<T>>(url, entities).Result;
                if (response.IsSuccessStatusCode)
                    return true;
                else
                    throw new Exception(String.Format("URL: {0} - ERROR: {1}", url, response.ToString()));
            }
        }

        private DateTime SendUser(DateTime lastSync)
        {
            List<Guid> ids = new List<Guid>();
            List<ApplicationUser> entities = new List<ApplicationUser>();

            DateTime minDate = (DateTime)SqlDateTime.MinValue;

            using (var ke = KEUnitOfWork.Create(false))
            {
                var customers = ke.CustomerRepository.GetsBySiteToSync(Guid.Parse(SiteId), minDate).ToList();
                var customerUsers = ke.CustomerUserRepository.GetsBySiteToSync(Guid.Parse(SiteId), minDate).ToList();

                customers.ForEach(x => ids.Add(x.Id));
                customerUsers.ForEach(x => ids.Add(x.Id));
            }

            if (ids != null && ids.Any())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ApplicationContext.Create()));

                foreach (var id in ids)
                {
                    var user = userManager.FindById(id.ToString());
                    if (user.LastModifiedDate >= lastSync)
                    {
                        user.RoleNames = userManager.GetRoles(id.ToString());
                        entities.Add(user);
                    }
                }
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/User/post", SiteId);
                if (Post<ApplicationUser>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendAddress(DateTime lastSync)
        {
            List<Address> entities = new List<Address>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.AddressRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/Address/post", SiteId);
                if (Post<Address>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendCustomerUser(DateTime lastSync)
        {
            List<CustomerUser> entities = new List<CustomerUser>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.CustomerUserRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/CustomerUser/post", SiteId);
                if (Post<CustomerUser>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendContact(DateTime lastSync)
        {
            List<Contact> entities = new List<Contact>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.ContactRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/Contact/post", SiteId);
                if (Post<Contact>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }
            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendPond(DateTime lastSync)
        {
            List<Pond> entities = new List<Pond>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.PondRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/Pond/post", SiteId);
                if (Post<Pond>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendTank(DateTime lastSync)
        {
            List<Tank> entities = new List<Tank>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.TankRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/Tank/post", SiteId);
                if (Post<Tank>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendSensor(DateTime lastSync)
        {
            List<Sensor> entities = new List<Sensor>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.SensorRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/Sensor/post", SiteId);
                if (Post<Sensor>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendSensorItem(DateTime lastSync)
        {
            List<SensorItem> entities = new List<SensorItem>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.SensorItemRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/SensorItem/post", SiteId);
                if (Post<SensorItem>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendTrigger(DateTime lastSync)
        {
            List<Trigger> entities = new List<Trigger>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.TriggerRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/Trigger/post", SiteId);
                if (Post<Trigger>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendTriggerContact(DateTime lastSync)
        {
            List<TriggerContact> entities = new List<TriggerContact>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.TriggerContactRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/TriggerContact/post", SiteId);
                if (Post<TriggerContact>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendSensorItemEvent(DateTime lastSync)
        {
            List<SensorItemEvent> entities = new List<SensorItemEvent>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.SensorItemEventRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/SensorItemEvent/post", SiteId);
                if (Post<SensorItemEvent>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendAlarm(DateTime lastSync)
        {
            List<Alarm> entities = new List<Alarm>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.AlarmRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/Alarm/post", SiteId);
                if (Post<Alarm>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        private DateTime SendAlarmHistory(DateTime lastSync)
        {
            List<AlarmHistory> entities = new List<AlarmHistory>();

            using (var ke = KEUnitOfWork.Create(false))
            {
                entities = ke.AlarmHistoryRepository.Find(x => x.LastModifiedDate >= lastSync).ToList();
            }

            if (entities != null && entities.Any())
            {
                String url = String.Format("{0}/{1}/{2}", MasterUrl, "sync/AlarmHistory/post", SiteId);
                if (Post<AlarmHistory>(url, entities.ToList()))
                    return entities.Any() ? entities.Max(x => x.LastModifiedDate) : (DateTime)SqlDateTime.MinValue;
            }

            return (DateTime)SqlDateTime.MinValue;
        }

        #endregion Send

        #endregion Functions
    }
}
