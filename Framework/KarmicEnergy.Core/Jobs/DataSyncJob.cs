using KarmicEnergy.Core.Persistence;
using Quartz;
using System;
using System.Linq;
using System.Configuration;
using System.Data.Entity.Validation;
using System.IO;
using KarmicEnergy.Core.Entities;

namespace KarmicEnergy.Core.Jobs
{
    public class DataSyncJob : IJob
    {
        private readonly object locker = new object();

        public void Execute(IJobExecutionContext context)
        {
            lock (locker)
            {
                String siteConfig = ConfigurationManager.AppSettings["Site:Id"];

                if (String.IsNullOrEmpty(siteConfig)) // Office
                {
                    KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();

                    var sites = KEUnitOfWork.SiteRepository.GetAllActive();

                    foreach (var site in sites)
                    {
                        var lastSync = KEUnitOfWork.DataSyncRepository.Find(x => x.SiteId == site.Id).OrderByDescending(o => o.CreatedDate).Last();

                        Address(lastSync);
                    }
                }
                else // Site
                {
                }
            }
        }

        private void Address(DataSync lastSync)
        {
            KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();
            var addresses = KEUnitOfWork.AddressRepository.Find(x => x.RowVersionDate > lastSync.RowVersionDate);

            
        }
        
        private void DataSync(DataSync lastSync)
        {

        }
    }
}
