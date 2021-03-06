using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface ISizeService
    {
        Task<IEnumerable<Size>> GetAll();

        Task<ApiResult<Pagination<SizeModel>>> GetAllPaging(SizeSearchContext ctx);

        Task<Size> GetById(string? id);

        Task<RepositoryResponse> Create(SizeModel model);

        Task<RepositoryResponse> Update(string? id, SizeModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<Size>> GetByIdAsyn(string? id);

        IList<Size> GetActive();
    }
}