using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface INotificationApiClient
    {
        public Task<bool> Create(NotificationModel request);

        Task<List<NotificationModel>> GetNoti();

        Task<bool> Delete(string id);
    }
}