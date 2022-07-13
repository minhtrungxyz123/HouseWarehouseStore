using HouseWarehouseStore.Data.Entities;

namespace HouseWarehouseStore.Service
{
    public interface ICategoryService
    {
        Task<List<ProductCategory>> GetAll(bool showHidden = true);
    }
}