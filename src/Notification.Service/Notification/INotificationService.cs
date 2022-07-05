using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Notification.Service
{
    public interface INotificationService
    {
        Task<HouseWarehouseStore.Data.Entities.Notification> GetById(string id);

        Task<RepositoryResponse> Create(NotificationModel model);

        Task<IEnumerable<HouseWarehouseStore.Data.Entities.Notification>> GetAll();
    }
}