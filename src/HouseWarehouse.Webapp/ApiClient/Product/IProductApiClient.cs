using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface IProductApiClient
    {
        Task<List<ProductModel>> GetAll();
    }
}