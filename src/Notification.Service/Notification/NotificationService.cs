using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Notification.Service
{
    public class NotificationService : INotificationService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public NotificationService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region Method

        public async Task<int> Delete(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Notifications.FindAsync(id);

            _context.Notifications.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }
        public async Task<RepositoryResponse> Create(NotificationModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            HouseWarehouseStore.Data.Entities.Notification item = new HouseWarehouseStore.Data.Entities.Notification()
            {
                NotiId = Guid.NewGuid().ToString(),
                UserId = model.UserId,
                Url = model.Url,
                CreatedDate = model.CreatedDate,
                IsRead = model.IsRead,
                NotiBody = model.NotiBody,
                NotiHeader = model.NotiHeader
            };

            await _context.Notifications.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.NotiId
            };
        }

        public async Task<IEnumerable<HouseWarehouseStore.Data.Entities.Notification>> GetAll()
        {
            var entities = await _context.Notifications
                            .OrderByDescending(p => p.Url)
                            .ToListAsync();
            return entities;
        }

        public async Task<HouseWarehouseStore.Data.Entities.Notification> GetById(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Notifications
                            .OrderByDescending(p => p.Url)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.NotiId == id);

            return item;
        }

        #endregion Method
    }
}