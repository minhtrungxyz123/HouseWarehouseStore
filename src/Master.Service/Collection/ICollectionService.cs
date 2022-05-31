using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface ICollectionService
    {
        Task<IEnumerable<Collection>> GetAll();

        Task<ApiResult<Pagination<Collection>>> GetAllPaging(CollectionSearchContext ctx);

        Task<Collection> GetById(int? id);

        Task<RepositoryResponse> Create(CollectionModel model);

        Task<RepositoryResponse> Update(int? id, CollectionModel model);

        Task<int> Delete(int? id);

        Task<ApiResult<Collection>> GetByIdAsyn(int? id);

        IList<Collection> GetActive(bool showHidden = true);

        IList<Collection> GetHome(bool showHidden = true);

        IList<Collection> GetHot(bool showHidden = true);

        IList<Collection> GetStatusProduct(bool showHidden = true);
    }
}