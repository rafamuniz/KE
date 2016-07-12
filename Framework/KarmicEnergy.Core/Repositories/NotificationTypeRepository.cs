using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class NotificationTypeRepository : KERepositoryBase<NotificationType>, INotificationTypeRepository
    {
        #region Constructor
        public NotificationTypeRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor              
    }
}
