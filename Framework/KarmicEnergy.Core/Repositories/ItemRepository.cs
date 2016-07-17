using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Core.Repositories
{
    public class ItemRepository : KERepositoryBase<Item>, IItemRepository
    {
        #region Constructor
        public ItemRepository(KEContext context)
            : base(context)
        {

        }
        #endregion Constructor     

        public List<Item> GetsBySensorTypeId(Int16 sensorTypeId)
        {
            return base.Find(x => x.SensorTypeId == sensorTypeId && x.DeletedDate == null).ToList();
        }

        public override IEnumerable<Item> GetsBySiteToSync(Guid siteId, DateTime lastSyncDate)
        {
            List<Item> items = new List<Item>();
            var entities = base.Find(x => x.LastModifiedDate > lastSyncDate);

            foreach (var entity in entities)
            {
                Item item = new Item()
                {
                    Id = entity.Id
                };

                item.Update(entity);
                items.Add(item);
            }

            return items;
        }
    }
}
