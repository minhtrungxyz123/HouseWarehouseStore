using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface ICategoryApiClient
    {
        Task<List<ProductCategoryModel>> GetAll();
    }
}