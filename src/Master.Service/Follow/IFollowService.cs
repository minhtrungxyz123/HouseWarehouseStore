using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IFollowService
    {
        Task<ApiResult<Pagination<Follow>>> GetAllPaging(FollowSearchContext ctx);

        Task<Follow> GetById(string id);

        Task<RepositoryResponse> Create(FollowModel model);

        Task<RepositoryResponse> Update(string id, FollowModel model);

        Task<int> Delete(string id);

        Task<ApiResult<Follow>> GetByIdAsyn(string? id);
    }
}