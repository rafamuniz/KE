using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services;
using KarmicEnergy.Core.Services.Interface;
using Quartz;

namespace KarmicEnergy.Web.Jobs
{
    public class DataSyncJob : IJob
    {
        #region Fields
        private readonly ILogService _logService;
        #endregion Fields

        #region Constructor
        public DataSyncJob()
        {

        }

        public DataSyncJob(ILogService logService)
        {
            this._logService = logService;
        }
        #endregion Constructor

        public void Execute(IJobExecutionContext context)
        {
            IKEUnitOfWork unitOfWork = KEUnitOfWork.Create(false);
            ILogService logService = new LogService(unitOfWork);
            SyncData syncData = new SyncData(logService);
            syncData.Execute();
        }
    }
}
