using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface ICommentService
    {
        Task<ApiResult<Pagination<CommentModel>>> GetAllPaging(CommentSearchContext ctx);

        Task<int> Delete(string id);
    }
}