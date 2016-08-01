using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarmicEnergy.Core.Services
{
    public abstract class KEServiceBase
    {
        #region Fields
        protected readonly IKEUnitOfWork _UnitOfWork;
        #endregion Fields

        #region Constructor
        public KEServiceBase(IKEUnitOfWork unitOfWork)
        {
            this._UnitOfWork = unitOfWork;
        }
             
        #endregion Constructor
    }
}
