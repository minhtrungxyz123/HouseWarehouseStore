using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public interface ISizeService
    {
        Task<List<SizeModel>> GetAll(string id);
    }
}