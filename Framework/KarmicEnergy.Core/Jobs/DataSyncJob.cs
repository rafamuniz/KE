using Quartz;

namespace KarmicEnergy.Core.Jobs
{
    public class DataSyncJob : IJob
    {
        private readonly object locker = new object();

        public void Execute(IJobExecutionContext context)
        {

        }
    }
}
