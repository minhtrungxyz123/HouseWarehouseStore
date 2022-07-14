using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public interface IProductSizeColorService
    {
        Task<List<ProductSizeColorModel>> GetAll(string id);
    }
}