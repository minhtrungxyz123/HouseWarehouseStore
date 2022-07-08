using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface IFollowApiClient
    {
        Task<List<FollowModel>> GetAll();
    }
}