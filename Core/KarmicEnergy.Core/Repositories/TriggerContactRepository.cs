using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KarmicEnergy.Core.Repositories
{
    public class TriggerContactRepository : KERepositoryBase<TriggerContact>, ITriggerContactRepository
    {
        #region Constructor
        public TriggerContactRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor               

        public List<TriggerContact> GetsByTrigger(Guid triggerId)
        {
            return base.Find(x => x.TriggerId == triggerId && x.Status == "A" && x.DeletedDate == null).ToList();            
        }
    }
}
