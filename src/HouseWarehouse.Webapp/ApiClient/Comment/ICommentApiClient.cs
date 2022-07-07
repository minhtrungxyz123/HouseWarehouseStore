using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface ICommentApiClient
    {
        Task<List<CommentModel>> GetAll();
    }
}