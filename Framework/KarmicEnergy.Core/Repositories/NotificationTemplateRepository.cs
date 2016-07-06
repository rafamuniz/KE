using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;

namespace KarmicEnergy.Core.Repositories
{
    public class NotificationTemplateRepository : KERepositoryBase<NotificationTemplate>, INotificationTemplateRepository
    {
        #region Constructor
        public NotificationTemplateRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor             
    }
}
