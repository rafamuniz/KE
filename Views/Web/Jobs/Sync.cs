using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;

namespace KarmicEnergy.Web.Jobs
{
    public class Sync
    {
        public void Execute()
        {
            String siteConfig = ConfigurationManager.AppSettings["Site:Id"].ToString();

            if (String.IsNullOrEmpty(siteConfig)) // Office
            {
                KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();
                List<Site> sites = KEUnitOfWork.SiteRepository.GetAllActive().ToList();

                foreach (var site in sites)
                {
                    try
                    {
                        DateTime currentDate = DateTime.UtcNow;
                        List<DateTime?> syncDates = new List<DateTime?>();
                        List<DataSync> dataSyncs = KEUnitOfWork.DataSyncRepository.Find(x => x.SiteId == site.Id).ToList();
                        DataSync lastSync = dataSyncs.Any() ? dataSyncs.OrderByDescending(o => o.SyncDate).Last() : new DataSync() { SyncDate = DateTime.MinValue };

                        var responseAddress = Address(site, lastSync);
                        syncDates.Add(responseAddress.Value);

                        //DateTime syncDate = syncDates.Any() ? syncDates.OrderByDescending(o => o.Value).Max() : DateTime.MinValue;

                        //DataSync dataSync = new DataSync()
                        //{
                        //    SiteId = site.Id,
                        //    SyncDate = syncDates.Max(),
                        //};

                        //KEUnitOfWork.DataSyncRepository.Add(dataSync);
                        //KEUnitOfWork.Complete();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else // Site
            {
                // Address
                // Users
                // CustomerUsers
                // CustomerUserSettings
                // CustomerUserSites
                // Logs
                // Tank
                // Sensor
                // SensorItems
                // Triggers
                // TriggerContacts
                // SensorItemEvents
                // AlarmHistories


            }
        }

        private DateTime? Address(Site site, DataSync lastSync)
        {
            KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();
            List<Address> addresses = KEUnitOfWork.AddressRepository.Find(x => x.LastModifiedDate > lastSync.SyncDate).ToList();

            // WebAPI            
            String url = String.Format("http://{0}/{1}/{2}", site.IPAddress, "sync/addresses/", site.Id);

            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync<List<Address>>(url, addresses).Result;
                if (response.IsSuccessStatusCode)
                {
                    return addresses.Max(x => x.CreatedDate);
                }
            }
            return null;
        }
    }
}
