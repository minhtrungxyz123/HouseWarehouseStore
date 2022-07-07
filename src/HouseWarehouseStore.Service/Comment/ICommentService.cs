using HouseWarehouseStore.Data.Entities;

namespace HouseWarehouseStore.Service
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAll();
    }
}