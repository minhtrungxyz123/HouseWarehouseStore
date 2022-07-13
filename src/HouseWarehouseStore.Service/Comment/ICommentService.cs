using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAll();

        Task<List<CommentModel>> GetByIdAsyn(string id, int take);
    }
}