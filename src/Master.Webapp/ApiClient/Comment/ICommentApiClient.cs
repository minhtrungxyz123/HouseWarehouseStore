using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface ICommentApiClient
    {
        Task<ApiResult<Pagination<CommentModel>>> Get(CommentSearchModel request);
        Task<bool> Delete(string id);
    }
}