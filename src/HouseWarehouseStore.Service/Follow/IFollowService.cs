using HouseWarehouseStore.Data.Entities;

namespace HouseWarehouseStore.Service
{
    public interface IFollowService
    {
        Task<List<Follow>> GetAll();
    }
}