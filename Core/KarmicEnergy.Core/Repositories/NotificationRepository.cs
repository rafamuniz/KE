using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class NotificationRepository : KERepositoryBase<Notification>, INotificationRepository
    {
        #region Constructor
        public NotificationRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor             
    }
}
