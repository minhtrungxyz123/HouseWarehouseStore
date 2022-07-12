using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface ICommentApiClient
    {
        Task<List<CommentModel>> GetAll();

        Task<List<CommentModel>> GetById(string id);

        public Task<bool> Create(CommentModel request);
    }
}