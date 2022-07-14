using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface IProductSizeColorApiClient
    {
        Task<List<ProductSizeColorModel>> GetAll(string id);
    }
}
