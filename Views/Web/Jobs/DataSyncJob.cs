using Quartz;

namespace KarmicEnergy.Web.Jobs
{
    public class DataSyncJob : IJob
    {
        private readonly object locker = new object();

        public void Execute(IJobExecutionContext context)
        {

        }
    }
}
