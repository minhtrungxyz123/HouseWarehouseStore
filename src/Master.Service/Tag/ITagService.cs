using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface ITagService
    {
        Task<IEnumerable<HouseWarehouseStore.Data.Entities.Tag>> GetAll();

        Task<ApiResult<Pagination<HouseWarehouseStore.Data.Entities.Tag>>> GetAllPaging(TagSearchContext ctx);

        Task<Tag> GetById(string? id);

        Task<RepositoryResponse> Create(TagModel model);

        Task<RepositoryResponse> Update(string? id, TagModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<HouseWarehouseStore.Data.Entities.Tag>> GetByIdAsyn(string? id);

        IList<HouseWarehouseStore.Data.Entities.Tag> GetActive(bool showHidden = true);
    }
}