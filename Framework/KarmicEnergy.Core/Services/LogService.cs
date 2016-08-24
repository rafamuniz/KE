using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Core.Services.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Services
{
    public class LogService : KEServiceBase<Guid, Log>, ILogService
    {
        #region Constructor

        public LogService(IKEUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        #endregion Constructor

        #region Functions

        public override void Create(Log log)
        {
            if (log == null)
                throw new ArgumentNullException("log is required");

            this._unitOfWork.LogRepository.Add(log);
            this._unitOfWork.Complete();
        }

        public override Log Get(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            return this._unitOfWork.LogRepository.Get(id);
        }

        public override IEnumerable<Log> GetAll()
        {
            return this._unitOfWork.LogRepository.GetAll();
        }

        public override void Delete(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("id is required");

            var log = this._unitOfWork.LogRepository.Get(id);
            log.DeletedDate = DateTime.UtcNow;
            this._unitOfWork.LogRepository.Update(log);
            this._unitOfWork.Complete();
        }
              
        #endregion Functions
    }
}
