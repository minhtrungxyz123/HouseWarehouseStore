using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface ICollectionService
    {
        Task<IEnumerable<Collection>> GetAll();

        Task<ApiResult<Pagination<Collection>>> GetAllPaging(CollectionSearchContext ctx);

        Task<Collection> GetById(string? id);

        Task<RepositoryResponse> Create(CollectionModel model);

        Task<RepositoryResponse> Update(string? id, CollectionModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<Collection>> GetByIdAsyn(string? id);

        IList<Collection> GetActive(bool showHidden = true);

        IList<Collection> GetHome(bool showHidden = true);

        IList<Collection> GetHot(bool showHidden = true);

        IList<Collection> GetStatusProduct(bool showHidden = true);
    }
}