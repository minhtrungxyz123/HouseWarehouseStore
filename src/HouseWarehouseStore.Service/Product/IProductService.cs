using HouseWarehouseStore.Data.Entities;

namespace HouseWarehouseStore.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAll(bool showHidden = true);
    }
}