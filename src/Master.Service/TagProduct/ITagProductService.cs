using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface ITagProductService
    {
        Task<IEnumerable<TagProduct>> GetAll();

        Task<ApiResult<Pagination<TagProductModel>>> GetAllPaging(TagProductSearchContext ctx);

        Task<TagProduct> GetById(string? id);

        Task<RepositoryResponse> Create(TagProductModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<TagProduct>> GetByIdAsyn(string? id);
    }
}