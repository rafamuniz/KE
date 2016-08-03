using Quartz;

namespace KarmicEnergy.Web.Jobs
{
    public class DataSyncJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            SyncData syncData = new SyncData();
            syncData.Execute();
        }
    }
}
