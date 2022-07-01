using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface IBannerApiClient
    {
        Task<List<BannerModel>> GetAll();
    }
}
