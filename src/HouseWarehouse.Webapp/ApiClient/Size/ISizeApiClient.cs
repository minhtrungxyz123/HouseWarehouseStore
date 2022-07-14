using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface ISizeApiClient
    {
        Task<List<SizeModel>> GetAll(string id);
    }
}
