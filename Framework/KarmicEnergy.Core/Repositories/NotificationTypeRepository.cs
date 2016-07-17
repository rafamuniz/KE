using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System.Collections.Generic;
using System.Linq;

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

        #region Functions

        public override void SaveSync(List<NotificationType> entities)
        {
            foreach (var e in entities)
            {
                var entity = this.Find(x => x.Name == e.Name).SingleOrDefault();
                if (entity == null)
                {
                    this.Add(e);
                }
                else
                {
                    entity.Update(e);
                    this.Update(entity);
                }
            }
        }

        #endregion Functions            
    }
}
