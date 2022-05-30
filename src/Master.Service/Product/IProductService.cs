using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();

        Task<ApiResult<Pagination<Product>>> Get(ProductSearchModel ctx);

        Task<Product> GetById(int? id);

        Task<RepositoryResponse> Create(ProductModel model);

        Task<RepositoryResponse> Update(int? id, ProductModel model);

        Task<int> Delete(int? id);

        Task<ApiResult<Product>> GetByIdAsyn(int? id);

        IList<Product> GetMvcListItems(bool showHidden = true);
    }
}